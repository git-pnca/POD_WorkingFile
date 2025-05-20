using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Autodesk.Revit.DB.SpecTypeId;
using System.Security.Cryptography;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.DB.Events;


namespace PodDesignPlugin
{
    /// <summary>
    /// Interaction logic for PODui.xaml
    /// </summary>
    public partial class PODui : Window
    {
        private void UpdatePodOptions()
        {
            string podType = ((ComboBoxItem)PodTypeComboBox.SelectedItem)?.Content.ToString();

            PodSizeComboBox.Items.Clear();
            DoorOpeningComboBox.Items.Clear();
            ShaftOpeningComboBox.Items.Clear();

            if (podType == "Shower Type")
            {
                PodSizeComboBox.Items.Add(new ComboBoxItem() { Content = "2600 x 1600" });
                PodSizeComboBox.Items.Add(new ComboBoxItem() { Content = "2850 x 1600" });
                PodSizeComboBox.Items.Add(new ComboBoxItem() { Content = "3000 x 1600" });
                DoorOpeningComboBox.Items.Add(new ComboBoxItem() { Content = "Position A" });
                DoorOpeningComboBox.Items.Add(new ComboBoxItem() { Content = "Position B" });
                ShaftOpeningComboBox.Items.Add(new ComboBoxItem() { Content = "1000 x 450" });
                ShaftOpeningComboBox.Items.Add(new ComboBoxItem() { Content = "1000 x 260" });
            }
            else if (podType == "Powder Type")
            {
                PodSizeComboBox.Items.Add(new ComboBoxItem() { Content = "1600 x 1450" });
                PodSizeComboBox.Items.Add(new ComboBoxItem() { Content = "1600 x 1350" });
                DoorOpeningComboBox.Items.Add(new ComboBoxItem() { Content = "Position A" });
                DoorOpeningComboBox.Items.Add(new ComboBoxItem() { Content = "Position B" });
                //ShaftOpeningComboBox.Items.Clear(); // Clear shaft options for Powder Type
            }
            else if (podType == "MaidSQ Type")
            {
                PodSizeComboBox.Items.Add(new ComboBoxItem() { Content = "1850 x 1650" });
                DoorOpeningComboBox.Items.Add(new ComboBoxItem() { Content = "Position A" });
                DoorOpeningComboBox.Items.Add(new ComboBoxItem() { Content = "Position B" });
                ShaftOpeningComboBox.Items.Clear(); // Clear shaft options for MaidSQ Type
            }
            else if (podType == "MaidL Type")
            {
                PodSizeComboBox.Items.Add(new ComboBoxItem() { Content = "2200 x 1500" });
                DoorOpeningComboBox.Items.Add(new ComboBoxItem() { Content = "Position A" });
                DoorOpeningComboBox.Items.Add(new ComboBoxItem() { Content = "Position B" });
                ShaftOpeningComboBox.Items.Clear(); // Clear shaft options for MaidL Type
            }

            PodSizeComboBox.SelectedIndex = -1;
            DoorOpeningComboBox.SelectedIndex = -1;
            ShaftOpeningComboBox.SelectedIndex = -1;

            CombinedSelectionChanged(null, null); // Update visibility based on new selections

        }
        private void UpdatePreviewImage()
        {
            PodTypeComboBox.SelectionChanged += (s, e) => { UpdatePodOptions(); UpdatePreviewImage(); };
            PodSizeComboBox.SelectionChanged += (s, e) => { UpdatePreviewImage(); };
            DoorOpeningComboBox.SelectionChanged += (s, e) => { UpdatePreviewImage(); };
            ShaftOpeningComboBox.SelectionChanged += (s, e) => { UpdatePreviewImage(); };

        }
        public PODui()
        {
            InitializeComponent();
            PodTypeComboBox.SelectionChanged += PodTypeComboBox_SelectionChanged;
            PodSizeComboBox.SelectionChanged += CombinedSelectionChanged;
            DoorOpeningComboBox.SelectionChanged += CombinedSelectionChanged;
            ShaftOpeningComboBox.SelectionChanged += CombinedSelectionChanged;
        }

        private void PodTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePodOptions();
            CombinedSelectionChanged(sender, e);
        }

        private void CombinedSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string podType = ((ComboBoxItem)PodTypeComboBox.SelectedItem)?.Content.ToString();
            string podSize = ((ComboBoxItem)PodSizeComboBox.SelectedItem)?.Content.ToString();
            string doorSize = ((ComboBoxItem)DoorOpeningComboBox.SelectedItem)?.Content.ToString();
            string shaftSize = ((ComboBoxItem)ShaftOpeningComboBox.SelectedItem)?.Content.ToString();

            // Hide all images first
            S21A2.Visibility = Visibility.Collapsed;
            S21B2.Visibility = Visibility.Collapsed;
            S22A2.Visibility = Visibility.Collapsed;
            S22B2.Visibility = Visibility.Collapsed;

            S31A2.Visibility = Visibility.Collapsed;
            S31B2.Visibility = Visibility.Collapsed;
            S32A2.Visibility = Visibility.Collapsed;
            S32B2.Visibility = Visibility.Collapsed;

            S41A2.Visibility = Visibility.Collapsed;
            S41B2.Visibility = Visibility.Collapsed;
            S42A2.Visibility = Visibility.Collapsed;
            P1A2.Visibility = Visibility.Collapsed;
            P1B2.Visibility = Visibility.Collapsed;
            P2A2.Visibility = Visibility.Collapsed;
            P2B2.Visibility = Visibility.Collapsed;
            M1A.Visibility = Visibility.Collapsed;
            M1B.Visibility = Visibility.Collapsed;
            M3A.Visibility = Visibility.Collapsed;
            M3B.Visibility = Visibility.Collapsed;



            if (string.IsNullOrEmpty(podType) || string.IsNullOrEmpty(podSize) || string.IsNullOrEmpty(doorSize))
            {
                PlaceholderText.Visibility = Visibility.Visible; // Show placeholder
                return;
            }
            else
            {
                PlaceholderText.Visibility = Visibility.Collapsed; // Hide placeholder
            }

            string imageKey = "";
            string url = null;

            if (podType.Contains("Shower Type"))
            {

                if (podSize == "2600 x 1600" && doorSize == "Position A" && shaftSize == "1000 x 450")
                {
                    imageKey = "S21A2";

                }

                else if (podSize == "2600 x 1600" && doorSize == "Position B" && shaftSize == "1000 x 450")
                {
                    imageKey = "S21B2";

                }

                else if (podSize == "2600 x 1600" && doorSize == "Position A" && shaftSize == "1000 x 260")
                {
                    imageKey = "S22A2";

                }
                else if (podSize == "2600 x 1600" && doorSize == "Position B" && shaftSize == "1000 x 260")
                {
                    imageKey = "S22B2";

                }
                else if (podSize == "2850 x 1600" && doorSize == "Position A" && shaftSize == "1000 x 450")
                {
                    imageKey = "S31A2";
                }


                else if (podSize == "2850 x 1600" && doorSize == "Position B" && shaftSize == "1000 x 450")
                {
                    imageKey = "S31B2";
                }
                else if (podSize == "2850 x 1600" && doorSize == "Position A" && shaftSize == "1000 x 260")
                {
                    imageKey = "S32A2";


                }
                else if (podSize == "2850 x 1600" && doorSize == "Position B" && shaftSize == "1000 x 260")
                {
                    imageKey = "S32B2";

                }

                else if (podSize == "3000 x 1600" && doorSize == "Position A" && shaftSize == "1000 x 450")
                {
                    imageKey = "S41A2";

                }
                else if (podSize == "3000 x 1600" && doorSize == "Position B" && shaftSize == "1000 x 450")
                {
                    imageKey = "S41B2";

                }
                else if (podSize == "3000 x 1600" && doorSize == "Position A" && shaftSize == "1000 x 260")
                {
                    imageKey = "S42A2";
                }

                else if (podSize == "3000 x 1600" && doorSize == "Position B" && shaftSize == "1000 x 260")
                {
                    imageKey = "S42B2";

                }
            }



            else if (podType.Contains("Powder Type"))
            {
                if (string.IsNullOrEmpty(podSize) || string.IsNullOrEmpty(doorSize))
                {
                    PlaceholderText.Visibility = Visibility.Visible; // Show placeholder
                    return;
                }
                if (podSize == "1600 x 1450" && doorSize == "Position A")
                {
                    imageKey = "P1A2";
                   
                }
                else if (podSize == "1600 x 1450" && doorSize == "Position B")
                {
                    imageKey = "P1B2";
                   
                }

                else if (podSize == "1600 x 1350" && doorSize == "Position A")
                {
                    imageKey = "P2A2";
                }
                else if (podSize == "1600 x 1350" && doorSize == "Position B")
                {

                    imageKey = "P2B2";
                }
            }

            else if (podType.Contains("MaidSQ Type"))
            {
                if (string.IsNullOrEmpty(podSize) || string.IsNullOrEmpty(doorSize))
                {
                    PlaceholderText.Visibility = Visibility.Visible; // Show placeholder
                    return;
                }
                if (podSize == "1850 x 1650" && doorSize == "Position A")
                {
                    imageKey = "M1A";
                }
                else if (podSize == "1850 x 1650" && doorSize == "Position B")
                {
                    imageKey = "M1B";
                }
            }

            else if (podType.Contains("MaidL Type"))
            {
                if (string.IsNullOrEmpty(podSize) || string.IsNullOrEmpty(doorSize))
                {
                    PlaceholderText.Visibility = Visibility.Visible; // Show placeholder
                    return;
                }
                if (podSize == "2200 x 1500" && doorSize == "Position A")
                {
                    imageKey = "M3A";
                }
                else if (podSize == "2200 x 1500" && doorSize == "Position B")
                {
                    imageKey = "M3B";
                }
            }


            if (!string.IsNullOrEmpty(imageKey))
            {
                var image = (Image)this.FindName(imageKey);
                if (image != null)
                {
                    image.Visibility = Visibility.Visible;
                }
            }
            else
            {
                PlaceholderText.Visibility = Visibility.Visible; // Show placeholder
            }
        }


        private string GetSelectedUrl()
        {
            string type = ((ComboBoxItem)PodTypeComboBox.SelectedItem)?.Content.ToString();
            string size = ((ComboBoxItem)PodSizeComboBox.SelectedItem)?.Content.ToString();
            string door = ((ComboBoxItem)DoorOpeningComboBox.SelectedItem)?.Content.ToString();
            string shaft = ((ComboBoxItem)ShaftOpeningComboBox.SelectedItem)?.Content.ToString();

            if (type == "Shower Type" && size == "2600 x 1600" && door == "Position A" && shaft == "1000 x 450")
            {
                return "https://docs.b360.autodesk.com/projects/0afcb078-8d81-4947-aa51-883eb24d077e/folders/urn:adsk.wipprod:fs.folder:co.5rqsx5fIQwSlBuWFTMldKA/detail";
            }
            else if (type == "Shower Type" && size == "2600 x 1600" && door == "Position B" && shaft == "1000 x 450")
            {
                return "https://docs.b360.autodesk.com/projects/0afcb078-8d81-4947-aa51-883eb24d077e/folders/urn:adsk.wipprod:fs.folder:co.5rqsx5fIQwSlBuWFTMldKA/detail";
            }
            else if (type == "Shower Type" && size == "2600 x 1600" && door == "Position A" && shaft == "1000 x 260")
            {
                return "https://docs.b360.autodesk.com/projects/0afcb078-8d81-4947-aa51-883eb24d077e/folders/urn:adsk.wipprod:fs.folder:co.5rqsx5fIQwSlBuWFTMldKA/detail";
            }
            else if (type == "Shower Type" && size == "2600 x 1600" && door == "Position B" && shaft == "1000 x 260")
            {
                return "https://docs.b360.autodesk.com/projects/0afcb078-8d81-4947-aa51-883eb24d077e/folders/urn:adsk.wipprod:fs.folder:co.5rqsx5fIQwSlBuWFTMldKA/detail";
            }
            else if (type == "Shower Type" && size == "2850 x 1600" && door == "Position A" && shaft == "1000 x 450")
            {
                return "https://music.youtube.com/";
            }
            else if (type == "Shower Type" && size == "2850 x 1600" && door == "Position B" && shaft == "1000 x 450")
            {
                return "https://docs.b360.autodesk.com/projects/0afcb078-8d81-4947-aa51-883eb24d077e/folders/urn:adsk.wipprod:fs.folder:co.5rqsx5fIQwSlBuWFTMldKA/detail";
            }
            else if (type == "Shower Type" && size == "2850 x 1600" && door == "Position A" && shaft == "1000 x 260")
            {
                return "https://docs.b360.autodesk.com/projects/0afcb078-8d81-4947-aa51-883eb24d077e/folders/urn:adsk.wipprod:fs.folder:co.5rqsx5fIQwSlBuWFTMldKA/detail";
            }
            else if (type == "Shower Type" && size == "2850 x 1600" && door == "Position B" && shaft == "1000 x 260")
            {
                return "https://docs.b360.autodesk.com/projects/0afcb078-8d81-4947-aa51-883eb24d077e/folders/urn:adsk.wipprod:fs.folder:co.5rqsx5fIQwSlBuWFTMldKA/detail";
            }
            else if (type == "Shower Type" && size == "3000 x 1600" && door == "Position A" && shaft == "1000 x 450")
            {
                return "https://docs.b360.autodesk.com/projects/0afcb078-8d81-4947-aa51-883eb24d077e/folders/urn";
            }
            else if(type == "Shower Type" && size == "3000 x 1600" && door == "Position B" && shaft == "1000 x 450")
            {
                return "https://docs.b360.autodesk.com/projects/0afcb078-8d81-4947-aa51-883eb24d077e/folders/urn:adsk.wipprod:fs.folder:co.5rqsx5fIQwSlBuWFTMldKA/detail";
            }
            else if (type == "Shower Type" && size == "3000 x 1600" && door == "Position A" && shaft == "1000 x 260")
            {
                return "https://docs.b360.autodesk.com/projects/0afcb078-8d81-4947-aa51-883eb24d077e/folders/urn:adsk.wipprod:fs.folder:co.5rqsx5fIQwSlBuWFTMldKA/detail";
            }
            else if (type == "Shower Type" && size == "3000 x 1600" && door == "Position B" && shaft == "1000 x 260")
            {
                return "https://docs.b360.autodesk.com/projects/0afcb078-8d81-4947-aa51-883eb24d077e/folders/urn:adsk.wipprod:fs.folder:co.5rqsx5fIQwSlBuWFTMldKA/detail";
            }
            else if(type == "Powder Type" && size == "1600 x 1450" && door == "Position A")
            {
                return "https://docs.b360.autodesk.com/projects/0afcb078-8d81-4947-aa51-883eb24d077e/folders/urn:adsk.wipprod:fs.folder:co.5rqsx5fIQwSlBuWFTMldKA/detail";
            }
            else if (type == "Powder Type" && size == "1600 x 1450" && door == "Position B")
            {
                return "https://docs.b360.autodesk.com/projects/0afcb078-8d81-4947-aa51-883eb24d077e/folders/urn:adsk.wipprod:fs.folder:co.5rqsx5fIQwSlBuWFTMldKA/detail";
            }
            else if (type == "Powder Type" && size == "1600 x 1350" && door == "Position A")
            {
                return "https://docs.b360.autodesk.com/projects/0afcb078-8d81-4947-aa51-883eb24d077e/folders/urn:adsk.wipprod:fs.folder:co.5rqsx5fIQwSlBuWFTMldKA/detail";
            }
            else if (type == "Powder Type" && size == "1600 x 1350" && door == "Position B")
            {
                return "https://docs.b360.autodesk.com/projects/0afcb078-8d81-4947-aa51-883eb24d077e/folders/urn:adsk.wipprod:fs.folder:co.5rqsx5fIQwSlBuWFTMldKA/detail";
            }
            else if (type == "MaidSQ Type" && size == "1850 x 1650" && door == "Position A")
            {
                return "https://docs.b360.autodesk.com/projects/0afcb078-8d81-4947-aa51-883eb24d077e/folders/urn:adsk.wipprod:fs.folder:co.5rqsx5fIQwSlBuWFTMldKA/detail";
            }
            else if (type == "MaidSQ Type" && size == "1850 x 1650" && door == "Position B")
            {
                return "https://docs.b360.autodesk.com/projects/0afcb078-8d81-4947-aa51-883eb24d077e/folders/urn:adsk.wipprod:fs.folder:co.5rqsx5fIQwSlBuWFTMldKA/detail";
            }
            else if (type == "MaidL Type" && size == "2200 x 1500" && door == "Position A")
            {
                return "https://docs.b360.autodesk.com/projects/0afcb078-8d81-4947-aa51-883eb24d077e/folders/urn:adsk.wipprod:fs.folder:co.5rqsx5fIQwSlBuWFTMldKA/detail";
            }
            else if (type == "MaidL Type" && size == "2200 x 1500" && door == "Position B")
            {
                return "https://docs.b360.autodesk.com/projects/0afcb078-8d81-4947-aa51-883eb24d077e/folders/urn:adsk.wipprod:fs.folder:co.-EHjJFgpTVK_Bbu4ZOsjfw/detail";
            }
            return null; // No valid combination found
        }
        
        private static void OpenUrl(string url)
        {
           

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        public void Download_Click(object sender, RoutedEventArgs e)
        {
            string podType = ((ComboBoxItem)PodTypeComboBox.SelectedItem)?.Content.ToString();
            string podSize = ((ComboBoxItem)PodSizeComboBox.SelectedItem)?.Content.ToString();
            string doorSize = ((ComboBoxItem)DoorOpeningComboBox.SelectedItem)?.Content.ToString();
            string shaftSize = ((ComboBoxItem)ShaftOpeningComboBox.SelectedItem)?.Content.ToString();

            if (string.IsNullOrEmpty(podType) || string.IsNullOrEmpty(podSize) || string.IsNullOrEmpty(doorSize))
            {
                MessageBox.Show("Please select all options before downloading.");
                return;

            }

            var url = GetSelectedUrl();
            if (url == null)
            {
                MessageBox.Show("Please select a valid combination first.");
                return;
            }

            OpenUrl(url);
        }
       
        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            PodTypeComboBox.SelectedIndex = -1;
            PodSizeComboBox.SelectedIndex = -1;
            DoorOpeningComboBox.SelectedIndex = -1;
            ShaftOpeningComboBox.SelectedIndex = -1;

            PlaceholderText.Visibility = Visibility.Visible; // Show placeholder
            
            S21A2.Visibility = Visibility.Collapsed;
            S21B2.Visibility = Visibility.Collapsed;
            S22A2.Visibility = Visibility.Collapsed;
            S22B2.Visibility = Visibility.Collapsed;
            S31A2.Visibility = Visibility.Collapsed;
            S31B2.Visibility = Visibility.Collapsed;
            S32A2.Visibility = Visibility.Collapsed;
            S32B2.Visibility = Visibility.Collapsed;
            S41A2.Visibility = Visibility.Collapsed;
            S41B2.Visibility = Visibility.Collapsed;
            S42A2.Visibility = Visibility.Collapsed;
            S42B2.Visibility = Visibility.Collapsed;
            P1A2.Visibility = Visibility.Collapsed;
            P1B2.Visibility = Visibility.Collapsed;
            P2A2.Visibility = Visibility.Collapsed;
            P2B2.Visibility = Visibility.Collapsed;
            M1A.Visibility = Visibility.Collapsed;
            M1B.Visibility = Visibility.Collapsed;
            M3A.Visibility = Visibility.Collapsed;
            M3B.Visibility = Visibility.Collapsed;

        }
        

    }
}
