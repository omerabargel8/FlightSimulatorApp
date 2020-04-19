using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    public interface IAppModel : INotifyPropertyChanged 
    {
        //ptoperties
        void connect(string ip, int port);
        void disconnect();
        void start();
        string Heading_deg { get; set; }
        string Vertical_speed { get; set; }
        string Ground_speed { get; set; }
        string Airspeed { get; set; }
        string Indicated_altitude { get; set; }
        string Internal_roll { get; set; }
        string Internal_pitch { get; set; }
        string Altimeter_altitude { get; set; }
        string Latitude_deg { get; set; }
        string Longitude_deg { get; set; }
        double Rudder { set; }
        double Elevator { set; }
        double Aileron { set; }
        double Throttle { set; }
        Location Location { get; set; }
        string Errors { get; set; }
        string Status { get; set; }
    }
}
