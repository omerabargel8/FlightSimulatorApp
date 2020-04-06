using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp2
{
    interface IAppModel : INotifyPropertyChanged 
    {
        void connect(string ip, int port);
        void disconnect();
        void start();
    }
}
