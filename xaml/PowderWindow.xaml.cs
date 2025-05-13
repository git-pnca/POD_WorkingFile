using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using Autodesk.Revit.UI;
using System;

namespace PodDesignPlugin
{
    public partial class PowderWindow : Window
    {
        private string downloadUrl = null;

        public PowderWindow()
        {
            InitializeComponent();
            DropdownSize.SelectionChanged += SelectionChangedHandler;
            DropdownDoorOpening.SelectionChanged += SelectionChangedHandler;
        }

        private void SelectionChangedHandler(object sender, SelectionChangedEventArgs e)
        {
            string selectedSize = ((ComboBoxItem)DropdownSize.SelectedItem)?.Content.ToString();
            string selectedDoor = ((ComboBoxItem)DropdownDoorOpening.SelectedItem)?.Content.ToString();

            P1A2.Visibility = Visibility.Collapsed;
            P1B2.Visibility = Visibility.Collapsed;
            P2A2.Visibility = Visibility.Collapsed;
            P2B2.Visibility = Visibility.Collapsed;

            string imageKey = GetImageName(selectedSize, selectedDoor);

            switch (imageKey)
            {
                case "P1A2":
                    P1A2.Visibility = Visibility.Visible;
                    break;
                case "P1B2":
                    P1B2.Visibility = Visibility.Visible;
                    break;
                case "P2A2":
                    P2A2.Visibility = Visibility.Visible;
                    break;
                case "P2B2":
                    P2B2.Visibility = Visibility.Visible;
                    break;
            }

            downloadUrl = GetDownloadUrl(imageKey);
        }

        private string GetImageName(string size, string doorPosition)
        {
            if (size == "1600 x 1450" && doorPosition == "Position A") return "P1A2";
            if (size == "1600 x 1450" && doorPosition == "Position B") return "P1B2";
            if (size == "1800 x 1350" && doorPosition == "Position A") return "P2A2";
            if (size == "1800 x 1350" && doorPosition == "Position B") return "P2B2";
            return null;
        }

        private string GetDownloadUrl(string imageKey)
        {
            switch (imageKey)
            {
                case "P1A2": return "https://sobha-my.sharepoint.com/personal/krishnaprasad_veedu_pncarchitects_com/Documents/Files/PROJECT%20FILES/POD%20STANDARDIZATION/RFA%20files%20-%20Shared/Powder/P1A2/P1A2/P1A2-LH-RH.rfa\r\n";
                case "P1B2": return "https://sobha-my.sharepoint.com/personal/krishnaprasad_veedu_pncarchitects_com/Documents/Files/PROJECT%20FILES/POD%20STANDARDIZATION/RFA%20files%20-%20Shared/Powder/P1B2/P1B2/P1B2-LH-RH.rfa\r\n";
                case "P2A2": return "https://sobha-my.sharepoint.com/personal/krishnaprasad_veedu_pncarchitects_com/Documents/Files/PROJECT%20FILES/POD%20STANDARDIZATION/RFA%20files%20-%20Shared/Powder/P2A2/P2A2/P2A2-LH-RH.rfa\r\n";
                case "P2B2": return "https://sobha-my.sharepoint.com/personal/krishnaprasad_veedu_pncarchitects_com/Documents/Files/PROJECT%20FILES/POD%20STANDARDIZATION/RFA%20files%20-%20Shared/Powder/P2B2/P2B2/P2B2-LH-RH.rfa\r\n";
                default: return null;
            }
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(downloadUrl))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = downloadUrl,
                    UseShellExecute = true
                });
                this.Close(); //Close the window after download starts
            }
            else
            {
                this.Close(); // Close the Window first

                // Use Dispatcher to delay showing TaskDialog AFTER window closes
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    TaskDialog.Show("Error", "Please select all dropdown values.");
                }));

                return;
            }
        }
    }
}
