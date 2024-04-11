using MeasurementAnalyzer.Models;
using MeasurementAnalyzer.ViewModels;
using MeasurementAnalyzer.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MeasurementAnalyzer
{
    /// <summary>
    /// Interaction logic for RequestDocumentWindow.xaml
    /// </summary>
    public partial class RequestDocumentWindow : Window
    {
        public RequestDocumentWindow()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string json = File.ReadAllText(openFileDialog.FileName);
                    Measure? measure = JsonSerializer.Deserialize<Measure>(json);
                    if (measure != null)
                    {
                        MainWindow window = new MainWindow(measure);
                        this.Close();
                        window.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Json parsing error occured: " + ex.Message);
                }
            }
        }
    }
}
