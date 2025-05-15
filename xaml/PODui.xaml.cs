using System;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
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


namespace PodDesignPlugin
{
    /// <summary>
    /// Interaction logic for PODui.xaml
    /// </summary>
    public partial class PODui : Window
    {
        public PODui()
        {
            InitializeComponent();
            PodTypeComboBox.SelectionChanged += CombinedSelectionChanged;
            PodSizeComboBox.SelectionChanged += CombinedSelectionChanged;
            DoorOpeningComboBox.SelectionChanged += CombinedSelectionChanged;
            ShaftOpeningComboBox.SelectionChanged += CombinedSelectionChanged;
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
            S42B2.Visibility = Visibility.Collapsed;


            if (string.IsNullOrEmpty(podType) || string.IsNullOrEmpty(podSize) || string.IsNullOrEmpty(doorSize) || string.IsNullOrEmpty(shaftSize))
            {
                PlaceholderText.Visibility = Visibility.Visible; // Show placeholder
                return;
            }
            PlaceholderText.Visibility = Visibility.Collapsed; // Hide placeholder

            if(podType == "Shower Type" && podSize == "2600 x 1600" && doorSize == "Position A" && shaftSize == "1000 x 450")
                S21A2.Visibility = Visibility.Visible;
            else if (podType == "Shower Type" && podSize == "2600 x 1600" && doorSize == "Position B" && shaftSize == "1000 x 450")
                S21B2.Visibility = Visibility.Visible;
            else if (podType == "Shower Type" && podSize == "2600 x 1600" && doorSize == "Position A" && shaftSize == "1000 x 260")
                S22A2.Visibility = Visibility.Visible;
            else if (podType == "Shower Type" && podSize == "2600 x 1600" && doorSize == "Position B" && shaftSize == "1000 x 260")
                S22B2.Visibility = Visibility.Visible;
            else if (podType == "Shower Type" && podSize == "2850 x 1600" && doorSize == "Position A" && shaftSize =="1000 x 450")
                S31A2.Visibility = Visibility.Visible;
            else if (podType == "Shower Type" && podSize == "2850 x 1600" && doorSize == "Position B" && shaftSize == "1000 x 450")
                S31B2.Visibility = Visibility.Visible;
            else if (podType == "Shower Type" && podSize == "2850 x 1600" && doorSize == "Position A" && shaftSize == "1000 x 260")
                S32A2.Visibility = Visibility.Visible;
            else if (podType == "Shower Type" && podSize == "2850 x 1600" && doorSize == "Position B" && shaftSize == "1000 x 260")
                S32B2.Visibility = Visibility.Visible;
            else if (podType == "Shower Type" && podSize == "3000 x 1600" && doorSize == "Position A" && shaftSize == "1000 x 450")
                S41A2.Visibility = Visibility.Visible;
            else if (podType == "Shower Type" && podSize == "3000 x 1600" && doorSize == "Position B" && shaftSize == "1000 x 450")
                S41B2.Visibility = Visibility.Visible;
            else if (podType == "Shower Type" && podSize == "3000 x 1600" && doorSize == "Position A" && shaftSize == "1000 x 260")
                S42A2.Visibility = Visibility.Visible;
            else if (podType == "Shower Type" && podSize == "3000 x 1600" && doorSize == "Position B" && shaftSize == "1000 x 260")
                S42B2.Visibility = Visibility.Visible;

            
        }

    
        
        public async void Download_Click(object sender, RoutedEventArgs e)
        {
            string podType = ((ComboBoxItem)PodTypeComboBox.SelectedItem)?.Content.ToString();
            string podSize = ((ComboBoxItem)PodSizeComboBox.SelectedItem)?.Content.ToString();
            string doorSize = ((ComboBoxItem)DoorOpeningComboBox.SelectedItem)?.Content.ToString();
            string shaftSize = ((ComboBoxItem)ShaftOpeningComboBox.SelectedItem)?.Content.ToString();

            if (string.IsNullOrEmpty(podType) || string.IsNullOrEmpty(podSize) || string.IsNullOrEmpty(doorSize) || string.IsNullOrEmpty(shaftSize))
            {
                MessageBox.Show("Please select all options before downloading.");
                return;
            }

            var creds = new Credentials();
            string token = await creds.Authenticate();

            MessageBox.Show("Token: " + token.Substring(0, 10) + "...");
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

        }
        

    }
}
