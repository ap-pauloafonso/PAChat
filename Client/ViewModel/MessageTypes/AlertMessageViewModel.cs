using LibMessages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel.MessageTypes
{
    class AlertMessageViewModel: INotifyPropertyChanged
    {
        private string _Alert;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Alert
        {
            get
            {
                return _Alert;
            }
            set
            {
                _Alert = value;
                OnPropertyChange("Alert");
            }
        }


        public AlertMessageViewModel(AlertMessage alert)
        {
            Alert = "<"+alert.Client;
            if (alert.Online)
            {
                Alert += " Connected>"; 
            }
            else
            {
                Alert += " Disconnected>";
            }
        }

        public void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
