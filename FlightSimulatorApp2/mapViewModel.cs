using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp2
{
   public class mapViewModel : INotifyPropertyChanged
    {
        private IAppModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        public mapViewModel(IAppModel model)
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
       
        public Location VM_location1
        {
            get
            {
                Console.WriteLine("@@@@@@@@@@@" + model.Location1);
                return model.Location1;
            }
        }
        /**
         * public string Location
        {
            get
            {
                Console.WriteLine("@@@@@@@@@@@ VM_longitude_deg + ", " + VM_latitude_deg");
                return VM_longitude_deg + "," + VM_latitude_deg;
            }
        }
    */
    }
}
