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
        private string indicated_heading_deg;
        private string gps_indicated_vertical_speed;
        private string gps_indicated_ground_speed_kt;
        private string airspeed_indicator_indicated_speed_kt;
        private string gps_indicated_altitude_ft;
        private string attitude_indicator_internal_roll_deg;
        private string attitude_indicator_internal_pitch_deg;
        private string altimeter_indicated_altitude_ft;
        private string latitude_deg;
        private string longitude_deg;
        public myAppModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            stop = false;
        }
        public void connect (string ip, int port)
        {
            this.telnetClient.connect(ip, port);
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
                    telnetClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                    Indicated_heading_deg = telnetClient.read();
                    //
                    telnetClient.write("get /instrumentation/gps/indicated-vertical-speed\n");
                    Gps_indicated_vertical_speed = telnetClient.read();

                    telnetClient.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    Gps_indicated_ground_speed_kt = telnetClient.read();

                    telnetClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                    Airspeed_indicator_indicated_speed_kt = telnetClient.read();

                    telnetClient.write("get /instrumentation/gps/indicated-altitude-ft\n");
                    Gps_indicated_altitude_ft = telnetClient.read();

                    telnetClient.write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                    Attitude_indicator_internal_roll_deg = telnetClient.read();

                    telnetClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    Attitude_indicator_internal_pitch_deg = telnetClient.read();

                    telnetClient.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                    Altimeter_indicated_altitude_ft = telnetClient.read();
    
                    telnetClient.write("get /position/latitude-deg\n");
                    Latitude_deg = telnetClient.read();

                    telnetClient.write("get /position/longitude-deg\n");
                    Longitude_deg = telnetClient.read();
                    Thread.Sleep(250);
                }
            }).Start();
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public string Indicated_heading_deg
        {
            get { return indicated_heading_deg; }
            set
            {
                indicated_heading_deg = value;
                NotifyPropertyChanged("indicated_heading_deg");
            }
        }
        public string Gps_indicated_vertical_speed
        {
            get { return gps_indicated_vertical_speed; }
            set
            {
                gps_indicated_vertical_speed = value;
                NotifyPropertyChanged("gps_indicated_vertical_speed");
            }
        }
        public string Gps_indicated_ground_speed_kt
        {
            get { return gps_indicated_ground_speed_kt; }
            set
            {
                gps_indicated_ground_speed_kt = value;
                NotifyPropertyChanged("gps_indicated_ground_speed_kt");
            }
        }
        public string Airspeed_indicator_indicated_speed_kt
        {
            get { return airspeed_indicator_indicated_speed_kt; }
            set
            {
                airspeed_indicator_indicated_speed_kt = value;
                NotifyPropertyChanged("airspeed_indicator_indicated_speed_kt");
            }
        }
        public string Gps_indicated_altitude_ft
        {
            get { return gps_indicated_altitude_ft; }
            set
            {
                gps_indicated_altitude_ft = value;
                NotifyPropertyChanged("gps_indicated_altitude_ft");
            }
        }
        public string Attitude_indicator_internal_roll_deg
        {
            get { return attitude_indicator_internal_roll_deg; }
            set
            {
                attitude_indicator_internal_roll_deg = value;
                NotifyPropertyChanged("attitude_indicator_internal_roll_deg");
            }
        }
        public string Attitude_indicator_internal_pitch_deg
        {
            get { return attitude_indicator_internal_pitch_deg; }
            set
            {
                attitude_indicator_internal_pitch_deg = value;
                NotifyPropertyChanged("attitude_indicator_internal_pitch_deg");
            }
        }
        public string Altimeter_indicated_altitude_ft
        {
            get { return altimeter_indicated_altitude_ft; }
            set
            {
                altimeter_indicated_altitude_ft = value;
                NotifyPropertyChanged("altimeter_indicated_altitude_ft");
            }
        }
        public string Latitude_deg
        {
            get { return latitude_deg; }
            set
            {
                latitude_deg = value;
                NotifyPropertyChanged("latitude_deg");
            }
        }
        public string Longitude_deg
        {
            get { return longitude_deg; }
            set
            {
                longitude_deg = value;
                NotifyPropertyChanged("longitude_deg");
            }
        }
        public void setRudder(double xPos)
        {
            telnetClient.write("set /controls/flight/rudder " + xPos + "\n");
        }
        public void setElevator(double yPos)
        {
            telnetClient.write("set /controls/flight/rudder "+yPos+"\n");
        }
    }
}
