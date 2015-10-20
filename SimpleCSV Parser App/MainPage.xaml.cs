using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SimpleCSV_Parser_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void btnParse_Click(object sender, RoutedEventArgs e)
        {
            CSVParser csvParser = new CSVParser();
            csvParser.RawText = this.txtInput.Text;

            var parsedData = csvParser.Parse();

            ShowData(parsedData);
        }

        private void ShowData(List<Dictionary<string, string>> parsedData)
        {

            StringBuilder output = new StringBuilder();

            var i = 0;

            foreach (var record in parsedData)
            {
                output.AppendLine("Record " + i);
                foreach (var field in record)
                {
                    output.AppendLine(field.Key + ": " + field.Value);
                }
                output.AppendLine("----------------");

                i++;
            }

            this.tbResultsConsole.Text = output.ToString();

        }
    }
}
