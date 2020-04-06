using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorApp2
{
    class myAppModel : IAppModel
    {
        ITelnetClient telnetClient;
        volatile Boolean stop;
        public event PropertyChangedEventHandler PropertyChanged;
        private double indicated_heading_deg;
        private double gps_indicated_vertical_speed;
        private double gps_indicated_ground_speed_kt;
        private double airspeed_indicator_indicated_speed_kt;
        private double gps_indicated_altitude_ft;
        private double attitude_indicator_internal_roll_deg;
        private double attitude_indicator_internal_pitch_deg;
        private double altimeter_indicated_altitude_ft;
        public myAppModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            stop = false;
        }
        public void connect (string ip, int port)
        {

        }
        public void disconnect()
        {
            stop = true;
            telnetClient.disconnect();
        }
        public void start()
        {
            new Thread(delegate ()
            {
                while (!stop)
                {
                    //
                    telnetClient.write("get indicated-heading-deg");
                    Indicated_heading_deg = Double.Parse(telnetClient.read());
                    //
                    telnetClient.write("get gps_indicated-vertical-speed");
                    Gps_indicated_vertical_speed = Double.Parse(telnetClient.read());
                    //
                    telnetClient.write("get gps_indicated-ground-speed-kt");
                    gps_indicated_ground_speed_kt = Double.Parse(telnetClient.read());
                    //
                    telnetClient.write("get airspeed-indicator_indicated-speed-kt");
                    Airspeed_indicator_indicated_speed_kt = Double.Parse(telnetClient.read()); 
                    //
                    telnetClient.write("get gps_indicated-altitude-ft");
                    Gps_indicated_altitude_ft = Double.Parse(telnetClient.read());
                    //
                    telnetClient.write("get attitude-indicator_internal-roll-deg");
                    Attitude_indicator_internal_roll_deg = Double.Parse(telnetClient.read());
                    //
                    telnetClient.write("get attitude-indicator_internal-pitch-deg");
                    Attitude_indicator_internal_pitch_deg = Double.Parse(telnetClient.read());
                    //
                    telnetClient.write("get altimeter_indicated-altitude-ft");
                    Altimeter_indicated_altitude_ft = Double.Parse(telnetClient.read());
                    Thread.Sleep(250);
                }
            }).Start();
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public double Indicated_heading_deg
        {
            get { return indicated_heading_deg; }
            set
            {
                indicated_heading_deg = value;
                NotifyPropertyChanged("indicated_heading_deg");
            }
        }
        public double Gps_indicated_vertical_speed
        {
            get { return gps_indicated_vertical_speed; }
            set
            {
                gps_indicated_vertical_speed = value;
                NotifyPropertyChanged("gps_indicated_vertical_speed");
            }
        }
        public double Gps_indicated_ground_speed_kt
        {
            get { return gps_indicated_ground_speed_kt; }
            set
            {
                gps_indicated_ground_speed_kt = value;
                NotifyPropertyChanged("gps_indicated_ground_speed_kt");
            }
        }
        public double Airspeed_indicator_indicated_speed_kt
        {
            get { return airspeed_indicator_indicated_speed_kt; }
            set
            {
                airspeed_indicator_indicated_speed_kt = value;
                NotifyPropertyChanged("airspeed_indicator_indicated_speed_kt");
            }
        }
        public double Gps_indicated_altitude_ft
        {
            get { return gps_indicated_altitude_ft; }
            set
            {
                gps_indicated_altitude_ft = value;
                NotifyPropertyChanged("gps_indicated_altitude_ft");
            }
        }
        public double Attitude_indicator_internal_roll_deg
        {
            get { return attitude_indicator_internal_roll_deg; }
            set
            {
                attitude_indicator_internal_roll_deg = value;
                NotifyPropertyChanged("attitude_indicator_internal_roll_deg");
            }
        }
        public double Attitude_indicator_internal_pitch_deg
        {
            get { return attitude_indicator_internal_pitch_deg; }
            set
            {
                attitude_indicator_internal_pitch_deg = value;
                NotifyPropertyChanged("attitude_indicator_internal_pitch_deg");
            }
        }
        public double Altimeter_indicated_altitude_ft
        {
            get { return altimeter_indicated_altitude_ft; }
            set
            {
                altimeter_indicated_altitude_ft = value;
                NotifyPropertyChanged("altimeter_indicated_altitude_ft");
            }
        }

    }
}
