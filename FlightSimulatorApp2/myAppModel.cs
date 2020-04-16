using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
        private double rudder;
        private double elevator;
        private double aileron;
        private double throttle;
        private Location location;
        private Mutex mutex = new Mutex();
        private double check;
        private bool isNumLatitude;
        private bool isNumLongitude;
        private double _latitude;
        private double _longitude;
        private string check1;
        private string errors;
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
            Stopwatch watch = new Stopwatch();
            while (!stop)
            {
                try
                {
                    //
                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                    Indicated_heading_deg = telnetClient.read();
                    Indicated_heading_deg = Indicated_heading_deg.Substring(0, 5);
                    mutex.ReleaseMutex();
                    //
                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/gps/indicated-vertical-speed\n");
                    Gps_indicated_vertical_speed = telnetClient.read();
                    Gps_indicated_vertical_speed = Gps_indicated_vertical_speed.Substring(0, 5);
                    mutex.ReleaseMutex();
                    //
                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    Gps_indicated_ground_speed_kt = telnetClient.read();
                    Gps_indicated_ground_speed_kt = Gps_indicated_ground_speed_kt.Substring(0, 5);
                    mutex.ReleaseMutex();
                    //
                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                    Airspeed_indicator_indicated_speed_kt = telnetClient.read();
                    Airspeed_indicator_indicated_speed_kt = Airspeed_indicator_indicated_speed_kt.Substring(0, 5);
                    mutex.ReleaseMutex();
                    //
                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/gps/indicated-altitude-ft\n");
                    Gps_indicated_altitude_ft = telnetClient.read();
                    Gps_indicated_altitude_ft = Gps_indicated_altitude_ft.Substring(0, 5);
                    mutex.ReleaseMutex();

                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                    Attitude_indicator_internal_roll_deg = telnetClient.read();
                    Attitude_indicator_internal_roll_deg = Attitude_indicator_internal_roll_deg.Substring(0, 5);
                    mutex.ReleaseMutex();
                    //
                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    Attitude_indicator_internal_pitch_deg = telnetClient.read();
                    Attitude_indicator_internal_pitch_deg = Attitude_indicator_internal_pitch_deg.Substring(0, 5);
                    mutex.ReleaseMutex();
                    //
                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                    Altimeter_indicated_altitude_ft = telnetClient.read();
                    Altimeter_indicated_altitude_ft = Altimeter_indicated_altitude_ft.Substring(0, 5);
                    mutex.ReleaseMutex();
                    //
                    mutex.WaitOne();
                    telnetClient.write("get /position/latitude-deg\n");
                    check1 = telnetClient.read();
                    isNumLatitude = double.TryParse(check1, out check);
                    if (isNumLatitude)
                    {
                        _latitude = double.Parse(check1);
                        if (_latitude <= 90 && _latitude >= -90)
                            Latitude_deg = check1;
                        else
                            Errors = Errors + "Out of bounds ";
                    }
                    mutex.ReleaseMutex();
                    //
                    mutex.WaitOne();
                    telnetClient.write("get /position/longitude-deg\n");
                    check1 = telnetClient.read();
                    isNumLongitude = double.TryParse(check1, out check);
                    if (isNumLongitude)
                    {
                        _longitude = double.Parse(check1);
                        if (_longitude <= 180 && _longitude >= -180)
                            Longitude_deg = check1;
                        else
                            Errors = Errors + "Out of bounds ";
                    }
                    mutex.ReleaseMutex();
                    //
                    mutex.WaitOne();
                    if (isNumLatitude && isNumLongitude)
                        Location = new Location(_latitude, _longitude);
                    mutex.ReleaseMutex();
                    Thread.Sleep(250);
                }
                catch (IOException)
                {
                    if (this.telnetClient.isConnect())
                        Errors = Errors + "Slowness was detected ";
                    else
                        Errors = Errors + "Server is down ";
                }
               catch(InvalidOperationException)
                    {
                        Errors = Errors + "Server is dissconnected";
                    }
                  
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
                NotifyPropertyChanged("location");
            }
        }
        public string Longitude_deg
        {
            get { return longitude_deg; }
            set
            {
                longitude_deg = value;
                NotifyPropertyChanged("location");
            }
        }
        public double Rudder
        {
            set
            {
                if (value != rudder)
                {
                    try
                    {
                        mutex.WaitOne();
                        rudder = value;
                        telnetClient.write("set /controls/flight/rudder " + rudder + "\n");
                        telnetClient.read();
                        mutex.ReleaseMutex();
                    }
                    catch (NullReferenceException)
                    {
                        Errors = Errors + "Server is dissconnected ";
                    }
                    catch (IOException)
                    {
                        Errors = Errors + "Server is down ";
                    }
                    
                }
            }
        }
        public double Elevator
        {
            set
            {
                if (value != elevator)
                {
                    try
                    {
                        mutex.WaitOne();
                        elevator = value;
                        telnetClient.write("set /controls/flight/elevator " + elevator + "\n");
                        telnetClient.read();
                        mutex.ReleaseMutex();
                    }
                    catch (NullReferenceException)
                    {
                        Errors = Errors + "Server is dissconnected ";
                    }
                    catch (IOException)
                    {
                        if (this.telnetClient.isConnect())
                            Errors = Errors + "Slowness was detected ";
                        else
                            Errors = Errors + "Server is down ";
                    }
                    
                }
            }
        }
        public double Aileron
        {
            set
            {
                if (value != aileron)
                {
                    try
                    {
                        mutex.WaitOne();
                        aileron = value;
                        telnetClient.write("set /controls/flight/aileron " + aileron + "\n");
                        telnetClient.read();
                        mutex.ReleaseMutex();
                    }
                    catch (NullReferenceException)
                    {
                        Errors = Errors + "Server is dissconnected ";
                    }
                    catch (IOException)
                    {
                        if (this.telnetClient.isConnect())
                            Errors = Errors + "Slowness was detected ";
                        else
                            Errors = Errors + "Server is down ";
                    }
                }
            }
        }
        public double Throttle
        {
            set
            {
                if (value != throttle)
                {
                    try
                    {
                        mutex.WaitOne();
                        throttle = value;
                        telnetClient.write("set /controls/engines/current-engine/throttle " + throttle + "\n");
                        telnetClient.read();
                        mutex.ReleaseMutex();
                    }
                    catch (NullReferenceException)
                    {
                        Errors = Errors + "Server is dissconnected ";
                    }
                    catch (IOException)
                    {
                        if (this.telnetClient.isConnect())
                            Errors = Errors + "Slowness was detected ";
                        else
                            Errors = Errors + "Server is down ";
                    }
                }
            }
        }
        public Location Location
        {
            get { return location; }
            set
            {
                location = value;
                NotifyPropertyChanged("location");
            }
        }
        public string Errors
        {
            get { return errors; }
            set
            {
                errors = value;
                NotifyPropertyChanged("errors");
            }
        }
    }
}
