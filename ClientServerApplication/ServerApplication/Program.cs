using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //setting the TCP listerner in port 51234
            int port = 51234;
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            TcpListener tcpServer = null;
            try
            {

                tcpServer = new TcpListener(ipAddress, port);

                //starting listerning client
                tcpServer.Start();

                //Buffer for reading data from client
                byte[] bytes = new byte[256];
                string data = null;
                string dataToUpper = null;


                //listening loop
                while (true)
                {
                    Console.WriteLine("Waiting for the Connecton");

                    //Call to accept request
                    TcpClient clientSystem = tcpServer.AcceptTcpClient();
                    Console.WriteLine("Connected !!!");


                    //getting a stream object for reading and writing 
                    NetworkStream dataStream = clientSystem.GetStream();

                    int i;
                    //loop to receive all the data send by the client
                    while ((i = dataStream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        //Translate data byte into ASCII value
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received Data : {0}", data);

                        //process the data to upper case
                        dataToUpper = data.ToUpper().ToString();
                        Console.WriteLine("The output is: ", dataToUpper);

                        byte[] respnseMsg = System.Text.Encoding.ASCII.GetBytes(dataToUpper);

                        //sending back the response 
                        dataStream.Write(respnseMsg, 0, respnseMsg.Length);
                        Console.WriteLine("Send {0}", dataToUpper);
                    }

                    //shutdown and end the connection
                    clientSystem.Close();
                }

            }
            catch (SocketException e)
            {
                Console.WriteLine("Socket Exception : {0} ", e);
            }
            finally
            {
                //stop listening for new client
                tcpServer.Stop();
            }
        }
    }
}
