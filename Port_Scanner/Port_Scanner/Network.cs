using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Port_Scanner
{
    public static class Network
    {
        public  delegate void PortScanDelegate(string ip, int port);

        public static string GetLocalHost()
        {
            return Dns.GetHostName().ToString();
        }

        public static void Scan(string ip, int port)
        {
            StringBuilder result = new StringBuilder();
            TcpClient TcpScan = new TcpClient();
            try
            {
                TcpScan.Connect(ip, port);
                if (TcpScan.Connected)
                {
                    result.AppendLine("ip address: " + ip + " Port " + port + " Open");
                    TcpScan.Close();
                }
            }
            catch (SocketException e)
            {
                result.AppendLine("ip address: " + ip + " Port " + port + " Closed");
            }
            catch (Exception e)
            {
                throw e;
            }
            
            Console.Write(result);
        }
        

        public static void ScanRange(string ip, int startPort, int endPort)
        {

            StringBuilder connError = new StringBuilder();
            int totalPorts = endPort - startPort;
            int currentPort = startPort;
            PortScanDelegate singleScan = new PortScanDelegate(Scan);
            IAsyncResult resultStatus;
            
            for (int i = 0; i <= totalPorts; i++)
            {
                //portScanThread.Add(new Thread(delegate() { Scan(ip, currentPort); }));
                resultStatus = singleScan.BeginInvoke(ip, currentPort, null, null);
                if (resultStatus.IsCompleted)
                {
                    Console.Write(connError.ToString());
                }
                currentPort++;
            }
        }
    }
}
