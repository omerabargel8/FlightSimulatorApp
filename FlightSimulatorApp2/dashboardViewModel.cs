using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp2
{
    class dashboardViewModel : INotifyPropertyChanged
    {
        private myAppModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        public dashboardViewModel(myAppModel model)
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
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
  
        public string VM_indicated_heading_deg
        {
            get { return model.Indicated_heading_deg; }
        }
        public string VM_gps_indicated_vertical_speed
        {
            get { return model.Gps_indicated_vertical_speed; }
        }
        public string VM_gps_indicated_ground_speed_kt
        {
            get { return model.Gps_indicated_ground_speed_kt; }
        }
        public string VM_airspeed_indicator_indicated_speed_kt
        {
            get { return model.Airspeed_indicator_indicated_speed_kt; }
        }
        public string VM_gps_indicated_altitude_ft
        {
            get { return model.Gps_indicated_altitude_ft; }
        }
        public string VM_attitude_indicator_internal_roll_deg
        {
            get { return model.Attitude_indicator_internal_roll_deg; }
        }
        public string VM_attitude_indicator_internal_pitch_deg
        {
            get { return model.Attitude_indicator_internal_pitch_deg; }
        }
        public string VM_altimeter_indicated_altitude_ft
        {
            get { return model.Altimeter_indicated_altitude_ft; ; }
        }

    }
}
