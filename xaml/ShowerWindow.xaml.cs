using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace PodDesignPlugin
{
    public partial class ShowerWindow : Window
    {
        private readonly Dictionary<string, string> fileMappings = new Dictionary<string, string>
        {
            { "2600 x 1600_Position A_1000 x 450", "https://sobha-my.sharepoint.com/personal/krishnaprasad_veedu_pncarchitects_com/Documents/Files/PROJECT%20FILES/POD%20STANDARDIZATION/RFA%20files%20-%20Shared/Shower/S21A2/S21A2/S21A2-LH-RH.rfa" },
            { "2600 x 1600_Position B_1000 x 450", "https://sobha-my.sharepoint.com/personal/krishnaprasad_veedu_pncarchitects_com/Documents/Files/PROJECT%20FILES/POD%20STANDARDIZATION/RFA%20files%20-%20Shared/Shower/S21B2/S21B2/S21B2-LH-RH.rfa\r\n" },
            { "2600 x 1600_Position A_1000 x 260", "https://sobha-my.sharepoint.com/personal/krishnaprasad_veedu_pncarchitects_com/Documents/Files/PROJECT%20FILES/POD%20STANDARDIZATION/RFA%20files%20-%20Shared/Shower/S22A2/S22A2/S22A2-LH-RH.rfa\r\n" },
            { "2600 x 1600_Position B_1000 x 260", "https://sobha-my.sharepoint.com/personal/krishnaprasad_veedu_pncarchitects_com/Documents/Files/PROJECT%20FILES/POD%20STANDARDIZATION/RFA%20files%20-%20Shared/Shower/S22B2/S22B2/S22B2-LH-RH.rfa\r\n" },
            { "2850 x 1600_Position A_1000 x 450", "https://sobha-my.sharepoint.com/personal/krishnaprasad_veedu_pncarchitects_com/Documents/Files/PROJECT%20FILES/POD%20STANDARDIZATION/RFA%20files%20-%20Shared/Shower/S31A2/S31A2/S31A2-LH-RH.rfa\r\n" },
            { "2850 x 1600_Position B_1000 x 450", "https://sobha-my.sharepoint.com/personal/krishnaprasad_veedu_pncarchitects_com/Documents/Files/PROJECT%20FILES/POD%20STANDARDIZATION/RFA%20files%20-%20Shared/Shower/S31B2/S31B2/S31B2-LH-RH.rfa\r\n" },
            { "2850 x 1600_Position A_1000 x 260", "https://sobha-my.sharepoint.com/personal/krishnaprasad_veedu_pncarchitects_com/Documents/Files/PROJECT%20FILES/POD%20STANDARDIZATION/RFA%20files%20-%20Shared/Shower/S32A2/S32A2/S32A2-LH-RH.rfa\r\n" },
            { "2850 x 1600_Position B_1000 x 260", "https://sobha-my.sharepoint.com/personal/krishnaprasad_veedu_pncarchitects_com/Documents/Files/PROJECT%20FILES/POD%20STANDARDIZATION/RFA%20files%20-%20Shared/Shower/S32B2/S32B2/S32B2-LH-RH.rfa\r\n" },
            { "3000 x 1600_Position A_1000 x 450", "https://sobha-my.sharepoint.com/personal/krishnaprasad_veedu_pncarchitects_com/Documents/Files/PROJECT%20FILES/POD%20STANDARDIZATION/RFA%20files%20-%20Shared/Shower/S41A2/S41A2/S41A2-LH-RH.rfa\r\n" },
            { "3000 x 1600_Position B_1000 x 450", "https://sobha-my.sharepoint.com/personal/krishnaprasad_veedu_pncarchitects_com/Documents/Files/PROJECT%20FILES/POD%20STANDARDIZATION/RFA%20files%20-%20Shared/Shower/S41B2/S41B2/S41B2-LH-RH.rfa\r\n" },
            { "3000 x 1600_Position A_1000 x 260", "https://sobha-my.sharepoint.com/personal/krishnaprasad_veedu_pncarchitects_com/Documents/Files/PROJECT%20FILES/POD%20STANDARDIZATION/RFA%20files%20-%20Shared/Shower/S42A2/S42A2/S42A2-LH-RH.rfa\r\n" },
            { "3000 x 1600_Position B_1000 x 260", "https://sobha-my.sharepoint.com/personal/krishnaprasad_veedu_pncarchitects_com/Documents/Files/PROJECT%20FILES/POD%20STANDARDIZATION/RFA%20files%20-%20Shared/Shower/S42A2/S42A2/S42A2-LH-RH.rfa\r\n" }
        };

        public ShowerWindow()
        {
            InitializeComponent();
            DropdownType.SelectionChanged += CombinedSelectionChanged;
            DropdownDoorOpening.SelectionChanged += CombinedSelectionChanged;
            DropdownShaft.SelectionChanged += CombinedSelectionChanged;
        }

        private void CombinedSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectType = ((ComboBoxItem)DropdownType.SelectedItem)?.Content.ToString();
            string selectedDoor = ((ComboBoxItem)DropdownDoorOpening.SelectedItem)?.Content.ToString();
            string selectedShaft = ((ComboBoxItem)DropdownShaft.SelectedItem)?.Content.ToString();

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

            // Show correct door image

            if (selectType == "2600 x 1600" && selectedDoor == "Position A" && selectedShaft == "1000 x 450")
                S21A2.Visibility = Visibility.Visible;
            else if (selectType == "2600 x 1600" && selectedDoor == "Position B" && selectedShaft == "1000 x 450")
                S21B2.Visibility = Visibility.Visible;
            else if (selectType == "2600 x 1600" && selectedDoor == "Position A" && selectedShaft == "1000 x 260")
                S22A2.Visibility = Visibility.Visible;
            else if (selectType == "2600 x 1600" && selectedDoor == "Position B" && selectedShaft == "1000 x 260")
                S22B2.Visibility = Visibility.Visible;

            else if(selectType == "2850 x 1600" && selectedDoor == "Position A" && selectedShaft == "1000 x 450")
                S31A2.Visibility = Visibility.Visible;
            else if (selectType == "2850 x 1600" && selectedDoor == "Position B" && selectedShaft == "1000 x 450")
                S31B2.Visibility = Visibility.Visible;
            else if (selectType == "2850 x 1600" && selectedDoor == "Position A" && selectedShaft == "1000 x 260")
                S32A2.Visibility = Visibility.Visible;
            else if (selectType == "2850 x 1600" && selectedDoor == "Position B" && selectedShaft == "1000 x 260")
                S32B2.Visibility = Visibility.Visible;

            else if (selectType == "3000 x 1600" && selectedDoor == "Position A" && selectedShaft == "1000 x 450")
                S41A2.Visibility = Visibility.Visible;
            else if (selectType == "3000 x 1600" && selectedDoor == "Position B" && selectedShaft == "1000 x 450")
                S41B2.Visibility = Visibility.Visible;
            else if (selectType == "3000 x 1600" && selectedDoor == "Position A" && selectedShaft == "1000 x 260")
                S42A2.Visibility = Visibility.Visible;
            else if (selectType == "3000 x 1600" && selectedDoor == "Position B" && selectedShaft == "1000 x 260")
                S42B2.Visibility = Visibility.Visible;
        }


        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            string type = (DropdownType.SelectedItem as ComboBoxItem)?.Content.ToString();
            string doorOpening = (DropdownDoorOpening.SelectedItem as ComboBoxItem)?.Content.ToString();
            string shaft = (DropdownShaft.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(doorOpening) || string.IsNullOrEmpty(shaft))
            {
                this.Close(); // Close the ShowerWindow first

                // Use Dispatcher to delay showing TaskDialog AFTER window closes
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    TaskDialog.Show("Error", "Please select all dropdown values.");
                }));

                return;
            }

            string key = $"{type}_{doorOpening}_{shaft}";

            if (fileMappings.TryGetValue(key, out string fileUrl))
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = fileUrl,
                        UseShellExecute = true
                    });

                    this.Close(); //Close the window after download starts
                }
                catch (Exception ex)
                {
                    TaskDialog.Show("Error", $"Failed to open link: {ex.Message}");
                }
            }
            else
            {
                TaskDialog.Show("Error", "No file mapping found for this combination.");
            }
        }
    }
}
