using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp2
{
    class MyTelnetClient : ITelnetClient {
        private TcpClient socket;
        private NetworkStream stream;

        //this method connect to server with the arguments ip, port
        public void connect(string ip, int port) {
            try
            {
                socket = new TcpClient(ip, port);
                stream = socket.GetStream();
                //define receive and send timeout
                socket.ReceiveTimeout = 10000;
                socket.SendTimeout = 10000;
                Console.WriteLine("Connection established");
            } catch (SocketException e)
            {
                Console.WriteLine("ERROR: {0}", e);
            }
        }
        //send to server the command received
        public void write(string command) 
        {
            try
            {
                Byte[] message = System.Text.Encoding.ASCII.GetBytes(command);
                stream.Write(message, 0, message.Length);
            }
            catch (IOException)
            {
                throw new IOException();
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException();
            }
            

        }
        //reads data from server
        public string read() {
            string received_data = "";
            try
            {
                byte[] read = new byte[1024];
                socket.GetStream().Read(read, 0, 1024);
                received_data = Encoding.ASCII.GetString(read, 0, read.Length);
                return received_data;
            }
            catch (Exception)
            {
                throw new IOException();
            }
        }
        //disconnect from server
        public void disconnect() 
        {
            socket.Close();
            Console.WriteLine("Disconnecting...");
        }
        //checks if the server is still connected
        public bool isConnect()
        {
            return this.socket.Connected && socket != null;
        }
    }
}
