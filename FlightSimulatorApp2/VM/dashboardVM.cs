using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp2
{
    public class dashboardVM : INotifyPropertyChanged
    {
        private IAppModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        //constructor
        public dashboardVM(IAppModel model)
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
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
  
        //properties
        public string VM_heading_deg
        {
            get { return model.Heading_deg; }
        }
        public string VM_vertical_speed
        {
            get { return model.Vertical_speed; }
        }
        public string VM_ground_speed
        {
            get { return model.Ground_speed; }
        }
        public string VM_airspeed
        {
            get { return model.Airspeed; }
        }
        public string VM_indicated_altitude
        {
            get { return model.Indicated_altitude; }
        }
        public string VM_internal_roll
        {
            get { return model.Internal_roll; }
        }
        public string VM_internal_pitch
        {
            get { return model.Internal_pitch; }
        }
        public string VM_altimeter_altitude
        {
            get { return model.Altimeter_altitude; ; }
        }

    }
}
