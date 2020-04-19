using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
   public class mapVM : INotifyPropertyChanged
    {
        private IAppModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        //constructor
        public mapVM(IAppModel model)
        {
            this.model = model;
            //creates this delegate method for bunding values from the model to view
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
        //properties
        public string VM_latitude_deg
        {
            get { return model.Latitude_deg; }
        }
        public string VM_longitude_deg
        {
            get { return model.Longitude_deg; }
        }
       
        public Location VM_location
        {
            get
            {
                return model.Location;
            }
        }

    }
}
