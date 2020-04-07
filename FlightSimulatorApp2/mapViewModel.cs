using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp2
{
    class mapViewModel : INotifyPropertyChanged
    {
        private myAppModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        public mapViewModel(myAppModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public string VM_latitude_deg
        {
            get { return model.Latitude_deg; }
        }
        public string VM_longitude_deg
        {
            get { return model.Longitude_deg; }
        }
        public string Location
        {
            get { return VM_longitude_deg + "," + VM_latitude_deg; }
        }
    }
}
