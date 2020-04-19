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
        private string heading_deg;
        private string vertical_speed;
        private string ground_speed;
        private string airspeed;
        private string indicated_altitude;
        private string internal_roll;
        private string internal_pitch;
        private string altimeter_altitude;
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
        private string status;
        
        //constructor
        public myAppModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            stop = false;
        }
        //connect to server using telnetClient
        public void connect (string ip, int port)
        {
            try
            {
                this.telnetClient.connect(ip, port);
                Status = "Connected";
                Errors = "";
            } 
            catch
            {
                Errors = "--Unable to connect to server--";
                throw new Exception();
            }
        }
        //disconnect from server using telnetClient
        public void disconnect()
        {
            stop = true;
            telnetClient.disconnect();
            Status = "Disconnected";
        }
        //this method runs loop, gets and updates the values of all properties
        public void start()
        {
            new Thread(delegate ()
            {
            while (!stop)
            {
                try
                {
                    //
                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                    Heading_deg = telnetClient.read();
                    Heading_deg = Heading_deg.Substring(0, 5);
                    mutex.ReleaseMutex();
                    //
                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/gps/indicated-vertical-speed\n");
                    Vertical_speed = telnetClient.read();
                    Vertical_speed = Vertical_speed.Substring(0, 5);
                    mutex.ReleaseMutex();
                    //
                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    Ground_speed = telnetClient.read();
                    Ground_speed = Ground_speed.Substring(0, 5);
                    mutex.ReleaseMutex();
                    //
                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                    Airspeed = telnetClient.read();
                    Airspeed = Airspeed.Substring(0, 5);
                    mutex.ReleaseMutex();
                    //
                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/gps/indicated-altitude-ft\n");
                    Indicated_altitude = telnetClient.read();
                    Indicated_altitude = Indicated_altitude.Substring(0, 5);
                    mutex.ReleaseMutex();

                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                    Internal_roll = telnetClient.read();
                    Internal_roll = Internal_roll.Substring(0, 5);
                    mutex.ReleaseMutex();
                    //
                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    Internal_pitch = telnetClient.read();
                    Internal_pitch = Internal_pitch.Substring(0, 5);
                    mutex.ReleaseMutex();
                    //
                    mutex.WaitOne();
                    telnetClient.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                    Altimeter_altitude = telnetClient.read();
                    Altimeter_altitude = Altimeter_altitude.Substring(0, 5);
                    mutex.ReleaseMutex();
                    //
                    mutex.WaitOne();
                    telnetClient.write("get /position/latitude-deg\n");
                    check1 = telnetClient.read();
                    //try to convert string to doule
                    isNumLatitude = double.TryParse(check1, out check);
                    if (isNumLatitude)
                    {
                        _latitude = double.Parse(check1);
                        //checks bounds of map
                        if (_latitude <= 90 && _latitude >= -90)
                            Latitude_deg = check1;
                        else
                            Errors = Errors + "--Out of bound--";
                    }
                    mutex.ReleaseMutex();
                    //
                    mutex.WaitOne();
                    telnetClient.write("get /position/longitude-deg\n");
                    check1 = telnetClient.read();
                    //try to convert string to doule
                    isNumLongitude = double.TryParse(check1, out check);
                    if (isNumLongitude)
                    {
                        _longitude = double.Parse(check1);
                            //checks bounds of map
                            if (_longitude <= 180 && _longitude >= -180)
                            Longitude_deg = check1;
                        else
                            Errors = Errors + "--Out of bound--";
                    }
                    mutex.ReleaseMutex();
                    //
                    mutex.WaitOne();
                    //update location only if both longitude and latitude are double numbers
                    if (isNumLatitude && isNumLongitude)
                        Location = new Location(_latitude, _longitude);
                    mutex.ReleaseMutex();
                    Thread.Sleep(250);
                }
                catch (IOException)
                {
                    if (this.telnetClient.isConnect())
                        Errors = Errors + "--Slowness was detected--";
                    else
                        Errors = "-Server is down-";
                }
               catch(InvalidOperationException)
                    {
                        Errors = "--Server is dissconnected--";
                    }
                  
                }
            }).Start();
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }


        //properties
        public string Heading_deg
        {
            get { return heading_deg; }
            set
            {
                heading_deg = value;
                NotifyPropertyChanged("heading_deg");
            }
        }
        public string Vertical_speed
        {
            get { return vertical_speed; }
            set
            {
                vertical_speed = value;
                NotifyPropertyChanged("vertical_speed");
            }
        }
        public string Ground_speed
        {
            get { return ground_speed; }
            set
            {
                ground_speed = value;
                NotifyPropertyChanged("ground_speed");
            }
        }
        public string Airspeed
        {
            get { return airspeed; }
            set
            {
                airspeed = value;
                NotifyPropertyChanged("airspeed");
            }
        }
        public string Indicated_altitude
        {
            get { return indicated_altitude; }
            set
            {
                indicated_altitude = value;
                NotifyPropertyChanged("indicated_altitude");
            }
        }
        public string Internal_roll
        {
            get { return internal_roll; }
            set
            {
                internal_roll = value;
                NotifyPropertyChanged("internal_roll");
            }
        }
        public string Internal_pitch
        {
            get { return internal_pitch; }
            set
            {
                internal_pitch = value;
                NotifyPropertyChanged("internal_pitch");
            }
        }
        public string Altimeter_altitude
        {
            get { return altimeter_altitude; }
            set
            {
                altimeter_altitude = value;
                NotifyPropertyChanged("altimeter_altitude");
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
                        Errors = "--Server is dissconnected--";
                    }
                    catch (IOException)
                    {
                        if (this.telnetClient.isConnect())
                            Errors = Errors + "--Slowness was detected--";
                        else
                            Errors = "--Server is down--";
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
                        Errors = "--Server is dissconnected--";
                    }
                    catch (IOException)
                    {
                        if (this.telnetClient.isConnect())
                            Errors = Errors + "--Slowness was detected--";
                        else
                            Errors = "--Server is down--";
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
                        Errors = "--Server is dissconnected--";
                    }
                    catch (IOException)
                    {
                        if (this.telnetClient.isConnect())
                            Errors = Errors + "Slowness was detected ";
                        else
                            Errors = "--Server is down--";
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
                        Errors = "--Server is dissconnected--";
                    }
                    catch (IOException)
                    {
                        if (this.telnetClient.isConnect())
                            Errors = Errors + "--Slowness was detected--";
                        else
                            Errors = "--Server is down--";
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
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                NotifyPropertyChanged("status");
            }
        }
    }
}
