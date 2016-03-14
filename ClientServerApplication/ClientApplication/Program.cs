using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            string server = "127.0.0.1";
            var port = 51234;

            Console.WriteLine("Enter the text to Send to the Server for changing it to upper case: ");
            string message = Console.ReadLine();

            //initializaing the tcpclient;
            TcpClient myClient = new TcpClient(server, port);
            //Translate the passing message into ASCII and store it in byte
            byte[] msgData = System.Text.Encoding.ASCII.GetBytes(message);

            //geting client stream to read and write
            NetworkStream clientStream = myClient.GetStream();

            //sending the message to TCPServer
            clientStream.Write(msgData, 0, msgData.Length);
            Console.WriteLine("Sent Message: {0}", message);

            //Receiving TCPServer Response

            //buffer to store the response
            msgData = new byte[256];

            //string to store the response ASCII representation
            string responseMsg = string.Empty;

            //Read the first byte of tcpresponse
            int byteMsg = clientStream.Read(msgData, 0, msgData.Length);
            responseMsg = System.Text.Encoding.ASCII.GetString(msgData, 0, byteMsg);
            Console.WriteLine("Received : {0}", responseMsg);

            clientStream.Close();

            myClient.Close();
        }
    }
}
