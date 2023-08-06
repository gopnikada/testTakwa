using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App1
{
    public class CsvDataModel
    {
        public string IKNumber { get; set; }
        public string Name { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public string Street { get; set; }
        public string IKNumber2 { get; set; }
        public CsvDataModel(string iKNumber, string name, string pLZ, string ort, string street, string iKNumber2)
        {
            IKNumber = iKNumber;
            Name = name;
            PLZ = pLZ;
            Ort = ort;
            Street = street;
            IKNumber2 = iKNumber2;
        }
    }


    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {

        private ObservableCollection<CsvDataModel> data;

        public string SearchTextNumber { get; private set; } = string.Empty;
        public string SearchTextName { get; private set; } = string.Empty;
        public string SearchTextOrt { get; private set; } = string.Empty;

        public MainWindow()
        {
            this.InitializeComponent();
            LoadDataFromCsv();
        }
        private async void LoadDataFromCsv()
        {
            // Replace "data.csv" with the path to your CSV file
            var csvFilePath = "C:\\Users\\User\\source\\repos\\App1\\App1\\kostentraeger.csv";
            var lines = File.ReadLines(csvFilePath);
            data = new ObservableCollection<CsvDataModel>();

            // Skip the first line (headers) and parse the remaining lines
            foreach (var line in lines)
            {
                var values = line.Split(';');
                data.Add(new CsvDataModel
                (
                     values[1],
                    values[0],
                    values[7],
                    values[8],
                    values[9],
                     values[17]
                ));
            }
        }
        private void OnSearchBoxNumber(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            SearchTextNumber = searchBoxNumber.Text.Trim();
            DataGridSetFiltered(data);
        }

        private void OnSearchBoxName(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            SearchTextName = searchBoxName.Text.Trim();
            DataGridSetFiltered(data);
        }

        private void OnSearchBoxOrt(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            SearchTextOrt = searchBoxOrt.Text.Trim();
            DataGridSetFiltered(data);
        }

        private void DataGridSetFiltered(ObservableCollection<CsvDataModel> data)
        {
            var filteredData = new ObservableCollection<CsvDataModel>(data.Where(item =>
               item.IKNumber.Contains(SearchTextNumber) &&
                item.Name.Contains(SearchTextName) &&
           item.Ort.Contains(SearchTextOrt)
           ));
            dataGrid.ItemsSource = filteredData;

        }



    }

}
