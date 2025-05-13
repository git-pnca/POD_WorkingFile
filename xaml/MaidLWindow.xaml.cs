using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using Autodesk.Revit.UI;
using System;

namespace PodDesignPlugin
{
    public partial class MaidLWindow : Window
    {
        private string downloadUrl = null;

        public MaidLWindow()
        {
            InitializeComponent();
            DropdownDoorOpening.SelectionChanged += SelectionChangedHandler;
        }

        private void SelectionChangedHandler(object sender, SelectionChangedEventArgs e)
        {
            string selectedDoor = ((ComboBoxItem)DropdownDoorOpening.SelectedItem)?.Content.ToString();

            M3A.Visibility = Visibility.Collapsed;
            M3B.Visibility = Visibility.Collapsed;

            string imageKey = GetImageName(selectedDoor);

            switch (imageKey)
            {
                case "M3A":
                    M3A.Visibility = Visibility.Visible;
                    break;
                case "M3B":
                    M3B.Visibility = Visibility.Visible;
                    break;
            }

            downloadUrl = GetDownloadUrl(imageKey);
        }

        private string GetImageName(string doorPosition)
        {
            if (doorPosition == "Position A") return "M3A";
            if (doorPosition == "Position B") return "M3B";

            return null;
        }

        private string GetDownloadUrl(string imageKey)
        {
            switch (imageKey)
            {
                case "M3A": return "https://sobha-my.sharepoint.com/personal/krishnaprasad_veedu_pncarchitects_com/Documents/Files/PROJECT%20FILES/POD%20STANDARDIZATION/RFA%20files%20-%20Shared/Maid/M3A/M3A/M3A-LH-RH.rfa\r\n";
                case "M3B": return "https://sobha-my.sharepoint.com/personal/krishnaprasad_veedu_pncarchitects_com/Documents/Files/PROJECT%20FILES/POD%20STANDARDIZATION/RFA%20files%20-%20Shared/Maid/M3B/M3B/M3B-LH-RH.rfa\r\n";
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
