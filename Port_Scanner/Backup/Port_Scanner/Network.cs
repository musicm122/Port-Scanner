using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Port_Scanner
{
    public static class Network
    {
        public static string GetLocalHost()
        {
            return Dns.GetHostName().ToString();
        }

        public static void Scan(string ip, int port)
        {
            string result = "";
            string error = "";
            TcpClient TcpScan = new TcpClient();
            try
            {
                TcpScan.Connect(ip, port);
                if (TcpScan.Connected)
                {
                    result = "ip address: " + ip + " Port " + port + " Open\r\n";
                }
                else
                {
                    result = "ip address: " + ip + " Port " + port + " Closed\r\n";
                }

                NetworkStream ClientStream = TcpScan.GetStream();
                ClientStream.Close();
                TcpScan.Close();
            }
            catch (SocketException e)
            {
                error = "ip address: " + ip + " Port " + port + " Closed:ERROR " + e.Message + "\r\n";
            }
            Console.Write(result);
        }

        public static void ScanRange(string ip, int startPort, int endPort)
        {
            string connError = "";
            int totalPorts = endPort - startPort + 1;
            int currentPort = startPort;
            Thread[] Threads = new Thread[totalPorts];
            for (int i = 0; i < totalPorts - 1; i++)
            {
                try
                {
                    //Console.WriteLine("thread# " + i);
                    Threads[i] = new Thread
                        (
                            delegate()
                            {
                                Scan(ip, currentPort);
                            }
                        );
                    Threads[i].Start();
                    //Console.WriteLine("thread# " + i + "Start");
                }
                catch (ThreadAbortException thAbort)
                {
                    connError += thAbort.Source.ToString() + ":" + thAbort.Message.ToString() + "\r\n";
                }
                catch (ThreadInterruptedException thInterrupt)
                {
                    connError += thInterrupt.Source.ToString() + ":" + thInterrupt.Message.ToString() + "\r\n";
                }

                currentPort++;
            }
        }
    }
}
