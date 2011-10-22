using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Port_Scanner
{
    public class Program
    {
        public static List<string> args = new List<string>();

        public static void Main(string [] arguments)
        {
            for (int i = 0; i < arguments.Length; i++) 
            {
                args.Add(arguments[i]);
            }

            if (args.Count < 2 || args.Count > 3)
            {
                Console.WriteLine("Error incorrect syntax \r\nport_scanner [ip address] [start port] [end port] ");
            }
            else
            {
                RecieveUserInput(args);
            }
            return;
        }
        
        public static void RecieveUserInput(List<string> sArgs)
        {
            int start_port = 0;
            int end_port = 0;
            string ip = "";

            switch (sArgs.Count) 
            { 
                case 2:
                    if (Validation.IsNumber(sArgs[0]) && Validation.IsNumber(sArgs[1]) && sArgs[0] != "0" && sArgs[1] != "0")
                    {
                        start_port = Convert.ToInt32(sArgs[0]);
                        end_port = Convert.ToInt32(sArgs[1]);
                        //if ip argument is blank then localhost is the ip address
                        ip = "localhost";
                    }
                    break;
                case 3:
                    if (Validation.IsIpAddress(sArgs[0]) && (Validation.IsNumber(sArgs[1]) && Validation.IsNumber(sArgs[2]) && sArgs[1] != "0" && sArgs[2] != "0"))
                    {
                        ip = sArgs[0];
                        start_port = Convert.ToInt32(sArgs[1]);
                        end_port = Convert.ToInt32(sArgs[2]);
                    }
                    break;
                default:
                    Console.WriteLine("Error incorrect syntax \r\nport_scanner [ip address] [start port] [end port] ");
                    Validation.End_Program();
                    break;
            }

            Network.ScanRange(ip, start_port, end_port);
            Console.ReadLine();
        }
    }
}