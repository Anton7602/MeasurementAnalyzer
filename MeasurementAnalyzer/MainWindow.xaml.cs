using DevExpress.Charts.Designer.Native;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using MeasurementAnalyzer.Models;
using MeasurementAnalyzer.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace MeasurementAnalyzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        public MainWindow(Measure measure)
        {
            InitializeComponent();
            InitializeViewModel(measure);
        }

        private void InitializeViewModel(Measure measure)
        {
            GraphViewModel viewModel = new GraphViewModel();
            viewModel.OriginalData = measure.Distances.ToObservableCollection();
            viewModel.Data = FilterDataMedianFiltering(FilterOutSpikes(measure.Distances, 15), 25).ToObservableCollection();
            graphView.DataContext = viewModel;
        }

        //Smoothing dataset using Moving average algorythm (Obsolete)
        private static List<MeasurementDistance> FilterDataMovingAverage(List<MeasurementDistance> data, int windowSize)
        {
            List<MeasurementDistance> smoothedData = new List<MeasurementDistance>();
            for (int i = 0; i < data.Count; i++)
            {
                int windowStartIndex = Math.Max(0, i - windowSize + 1);
                int windowEndIndex = Math.Min(i + 1, data.Count());

                decimal sum = 0;
                int count = 0;

                for (int j = windowStartIndex; j < windowEndIndex; j++)
                {
                    sum += data[j].Speed;
                    count++;
                }

                MeasurementDistance newValue = new MeasurementDistance();
                newValue.Speed = sum / count;
                newValue.Distance = (i == 0) ? data[0].Distance : smoothedData[i - 1].Distance + newValue.Speed;
                smoothedData.Add(newValue);
            }
            return smoothedData;
        }

        //Smoothing using Median Filtering
        public static List<MeasurementDistance> FilterDataMedianFiltering(List<MeasurementDistance> data, int windowSize)
        {
            if (windowSize % 2 == 0)
            {
                throw new ArgumentException("Window size must be an odd number.");
            }

            int halfWindowSize = windowSize / 2;
            List<MeasurementDistance> smoothedData = new List<MeasurementDistance>();

            for (int i = 0; i < data.Count(); i++)
            {
                List<decimal> window = new List<decimal>();

                for (int j = 0; j < windowSize; j++)
                {
                    int index = i - halfWindowSize + j;

                    if (index < 0 || index >= data.Count)
                    {
                        // If the index is out of bounds, use the original value as it is
                        window.Add(data[i].Speed);
                    }
                    else
                    {
                        window.Add(data[index].Speed);
                    }
                }

                window.Sort();

                MeasurementDistance newValue = new MeasurementDistance();
                newValue.Speed = window[halfWindowSize];
                newValue.Distance = (i == 0) ? data[0].Distance : smoothedData[i - 1].Distance + newValue.Speed;
                smoothedData.Add(newValue);
            }

            return smoothedData;
        }

        //Removes significant falldowns to ignore incorrect values
        public static List<MeasurementDistance> FilterOutSpikes(List<MeasurementDistance> data, int windowSize)
        {
            for (int i = windowSize + 1; i < data.Count(); i++)
            {
                if (i == 270)
                {
                    int u = 2;
                }
                decimal averageValueInWindow = 0;
                for (int j = i - windowSize; j < i; j++)
                {
                    averageValueInWindow += Math.Abs(data[j].Speed - data[j - 1].Speed);
                }
                averageValueInWindow /= windowSize;
                int currentEvaluatedIndex = i - windowSize / 2;
                if (Math.Abs(data[currentEvaluatedIndex].Speed - data[currentEvaluatedIndex - 1].Speed) > averageValueInWindow * 6 && data[currentEvaluatedIndex].Speed < data[currentEvaluatedIndex - 1].Speed)
                {
                    data.RemoveAt(i - windowSize / 2);
                    i--;
                }
            }
            return data;
        }

        private void btnExportFilteredDataSet_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string json = JsonSerializer.Serialize((graphView.DataContext as GraphViewModel).Data);
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "FilteredData",
                DefaultExt = ".json"
            };
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                File.WriteAllText(dlg.FileName, json);
            }
        }

        private void btnLoadNewDataSet_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";

            if (openFileDialog.ShowDialog() == true)
            {
                string json = File.ReadAllText(openFileDialog.FileName);
                Measure? measure = JsonSerializer.Deserialize<Measure>(json);
                if (measure != null)
                {
                    InitializeViewModel(measure);
                }
            }
        }
    }
}
