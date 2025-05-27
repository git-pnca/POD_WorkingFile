using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace PodDesignPlugin
{
    /// <summary>
    /// Interaction logic for ViewsCreation.xaml
    /// </summary>
    public partial class ViewsCreation : Window
    {
        private UIDocument _uidoc;
        private Document _doc;
        private List<Level> _levels;

        public ViewsCreation(UIDocument uidoc)
        {
            InitializeComponent();
            _uidoc = uidoc;
            _doc = uidoc.Document;

            LoadLevels();
            InitializeDropdowns();
        }

        private void LoadLevels()
        {
            FilteredElementCollector collector = new FilteredElementCollector(_doc);
            _levels = collector.OfClass(typeof(Level)).Cast<Level>().ToList();

            levelListBox.ItemsSource = _levels;
            levelListBox.DisplayMemberPath = "Name";
        }

        private void InitializeDropdowns()
        {
            disciplineComboBox.ItemsSource = new List<string>
            {
                "Architecture", "Structure", "Mechanical", "Electrical", "Plumbing", "Coordination"
            };

            scaleComboBox.ItemsSource = new List<string>
            {
                "1:1", "1:2", "1:5", "1:10", "1:20", "1:25", "1:50", "1:100",
                "1:200", "1:500", "1:1000", "1:2000", "1:5000"
            };

            subDisciplineComboBox.IsEditable = true;
            subDisciplineComboBox.IsReadOnly = false;
        }

        private void DisciplineComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            subDisciplineComboBox.Text = string.Empty;
            subDisciplineComboBox.ToolTip = "Optional: Leave blank if not applicable";
        }

        private void ScaleComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Optional: implement logic if needed
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            string discipline = disciplineComboBox.SelectedItem as string;
            string subDiscipline = subDisciplineComboBox.Text.Trim();
            string scale = scaleComboBox.SelectedItem as string;

            if (string.IsNullOrEmpty(discipline))
            {
                MessageBox.Show("Please select a Discipline.", "Missing Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(scale))
            {
                MessageBox.Show("Please select a Scale.", "Missing Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (rbLevelWise.IsChecked == true)
            { 
                CreateViewsByLevel(discipline, subDiscipline, scale);
            }
            else if (rbSelectedLevels.IsChecked == true)
            {
                Level selectedLevel = levelListBox.SelectedItem as Level;
                if (selectedLevel == null)
                {
                    MessageBox.Show("Please select a Level.", "Missing Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                CreateViewForLevel(selectedLevel, discipline, subDiscipline, scale);
            }

            this.DialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void CreateViewsByLevel(string discipline, string subDiscipline, string scale)
        {
            using (Transaction tx = new Transaction(_doc, "Create Views by Level"))
            {
                tx.Start();
                foreach (Level level in _levels)
                {
                    CreateViewForLevel(level, discipline, subDiscipline, scale);
                }
                tx.Commit();
            }
        }

        private void CreateViewForLevel(Level level, string discipline, string subDiscipline, string scale)
        {
            ViewFamilyType floorPlanType = new FilteredElementCollector(_doc)
                .OfClass(typeof(ViewFamilyType))
                .Cast<ViewFamilyType>()
                .FirstOrDefault(vft => vft.ViewFamily == ViewFamily.FloorPlan);

            if (floorPlanType == null)
            {
                MessageBox.Show("No Floor Plan ViewFamilyType found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ViewPlan view = ViewPlan.Create(_doc, floorPlanType.Id, level.Id);
            view.Name = level.Name + " - " + discipline + (string.IsNullOrEmpty(subDiscipline) ? "" : " - " + subDiscipline);
        }
    }
}
