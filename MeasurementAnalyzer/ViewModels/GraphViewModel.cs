using MeasurementAnalyzer.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MeasurementAnalyzer.ViewModels
{
    public partial class GraphViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<MeasurementDistance> _data;

        public ObservableCollection<MeasurementDistance> Data
        {
            get { return _data; }
            set
            {
                if (_data != value)
                {
                    _data = value;
                    OnPropertyChanged(nameof(Data));
                }
            }
        }

        private ObservableCollection<MeasurementDistance> _originalData;

        public ObservableCollection<MeasurementDistance> OriginalData
        {
            get { return _originalData; }
            set
            {
                if (_originalData != value)
                {
                    _originalData = value;
                    OnPropertyChanged(nameof(OriginalData));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
