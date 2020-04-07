using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp2
{
    class MyTelnetClient : ITelnetClient {
        private TcpClient socket;
        private NetworkStream stream;
        private string blaa;

        public void connect(string ip, int port) {
            try
            {
                socket = new TcpClient(ip, port);
                stream = socket.GetStream();
                blaa = "";
                Console.WriteLine("Connection established");
            } catch (SocketException e)
            {
                Console.WriteLine("ERROR: {0}", e);
            }
        }
        public void write(string command) 
        {
            try
            {
                Byte[] message = System.Text.Encoding.ASCII.GetBytes(command);
                stream.Write(message, 0, message.Length);
            } catch (Exception e)
            {
                Console.WriteLine("ERROR: {0}", e);
            }

        }
        public string read() {
            /**
            //initializing data string
            string data = "";
            try
            {
                byte[] write = Encoding.ASCII.GetBytes(command);
                //write to server 
                socket.GetStream().Write(write, 0, write.Length);
                byte[] read = new byte[1024];
                //reads server response so server won't be with unnecessary information
                //that another thread by mistake can read instead of necessary information.
                socket.GetStream().Read(read, 0, 1024);

                data = Encoding.ASCII.GetString(read, 0, read.Length);
                //Console.WriteLine("server response of reading data is: {0}\n",Double.Parse(data));
                return data;
            }
            catch (Exception e)
            {
     
                disconnect();
                return data;
            }
            */
            string received_data = "";
            try
            {
                //mutex.WaitOne();
                //byte[] write = Encoding.ASCII.GetBytes(command);
                //write to server 
                //client.GetStream().Write(write, 0, write.Length);
                byte[] read = new byte[1024];
                //reads server response so server won't be with unnecessary information
                //that another thread by mistake can read instead of necessary information.
                socket.GetStream().Read(read, 0, 1024);

                received_data = Encoding.ASCII.GetString(read, 0, read.Length);

                //mutex.ReleaseMutex();
                //Console.WriteLine("server response of reading data is: {0}\n",Double.Parse(data));
                return received_data;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: {0}", e);
                return "ERROR";
            }
        }
        public void disconnect() 
        {
            socket.Close();
            Console.WriteLine("Disconnecting...");
        }
    }
}
