//Author: Terrance Smith
/* Utility class I am using in conjunction with network based projects
 * I.E regex ip validation, getting Range, preforming tcp connections
 * 
*/
using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Linq;

namespace Evil_Smitty 
{

    public static class Validation
    {
		public static bool IsNumber(string TheString)
        {
			Regex pattern = new Regex(@"^\d+$"); 
			return pattern.IsMatch(TheString);
		}
		
        public static bool ContainsNumber(string TheString)
        {
			Regex pattern = new Regex(@".*([\d]+).*"); 
			return pattern.IsMatch(TheString);
		}
		
        public static bool IsIpAddress(string TheString)
        {
			Regex pattern = new Regex(@"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$");
			return pattern.IsMatch(TheString);
		}
		
        public static void End_Program()
        {
			Console.Write("Press Any Key to Continue.....");
			Console.ReadKey(true);
			return;
		}
	}
	public static class Network
    {
		public static string GetLocalHost()
        {
			string localhost = "";
			try
            {
				localhost = Dns.GetHostName().ToString();
			}
            catch(SocketException e) 
            {
				Console.WriteLine("SocketException caught!!!");
				Console.WriteLine("Source : " + e.Source);
				Console.WriteLine("Message : " + e.Message);
				localhost =  "Cannot determine localhost";
			}	
			return localhost;
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
            for (int i = 0; i<totalPorts-1; i++)
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
