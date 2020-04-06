using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp2
{
    class MyTelnetClient : ITelnetClient {
        public void connect(string ip, int port) {
            Console.WriteLine("nnn");
        }
        public void write(string command) { }
        public string read() {
            return "";
        }
        public void disconnect() { }
    }
}
