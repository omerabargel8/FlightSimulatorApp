﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    interface ITelnetClient
    {
        //this method connect to server with the arguments ip, port
        void connect(string ip, int port);
        //send to server the command received
        void write(string command);
        //reads data from server
        string read();
        //disconnect from server
        void disconnect();
        //checks if the server is still connected
        bool isConnect();
    }
}
