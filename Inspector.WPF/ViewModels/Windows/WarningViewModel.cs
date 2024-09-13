using Inspector.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Inspector.ViewModels.Windows
{
    public partial class WarningViewModel<T> : INotifyPropertyChanged
    {
        private List<T> dbCollection { get; set; }
        public List<T> DBCollection
        {
            get { return dbCollection; }
            set
            {
                dbCollection = value;
                OnPropertyChanged(nameof(DBCollection));
            }
        }
        private List<SertificatesWpf> dbCollection1 { get; set; }
        public List<SertificatesWpf> DBCollection1
        {
            get { return dbCollection1; }
            set
            {
                dbCollection1 = value;
                OnPropertyChanged(nameof(DBCollection1));
            }
        }

        private string warningMessage1;
        public string WarningMessage1
        {
            get { return warningMessage1; }
            set
            {
                warningMessage1 = value;
                OnPropertyChanged(nameof(WarningMessage1));
            }
        }
        private string warningMessage2;
        public string WarningMessage2
        {
            get { return warningMessage2; }
            set
            {
                warningMessage2 = value;
                OnPropertyChanged(nameof(WarningMessage2));
            }
        }

        public ICommand CloseCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
