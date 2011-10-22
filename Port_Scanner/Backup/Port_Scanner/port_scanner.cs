/*Author: Terrance Smith
 *Description:
 *A simple port scanner;
 *takes 3 args [start:port] [end:port] [ipaddress]
 *compiled with Mono and .NET. 
*/
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Evil_Smitty;


namespace port_scanner
{
	public class port_scanner
    {
		public static void scan_start(string [] sArgs)
        {
            int start_port = 0;
			int end_port = 0;
			string ip = "";
            string[] sArr = new string[sArgs.Length];

            for (int i = 0; i < sArgs.Length; i++)
            {
                //assign args to array
                sArr[i] = sArgs[i];
			}

            if(sArr.Length == 2)
            {
                if(Validation.IsNumber(sArr[0]) && Validation.IsNumber(sArr[1]) && sArr[0]!="0" && sArr[1]!="0")
                {
    				start_port = Convert.ToInt32(sArr[0]);
	    			end_port = Convert.ToInt32(sArr[1]);
				
					//if ip arg blank then local host
					ip = "localhost";
				}
            }
            else if(sArr.Length == 3)
            {
				if (Validation.IsIpAddress(sArgs[0]) && (Validation.IsNumber(sArr[1]) && Validation.IsNumber(sArr[2]) && sArr[1]!="0" && sArr[2]!="0"))
                {
                    //Console.WriteLine(Dns.GetHostEntry(sArr[0]).ToString());
                    ip = sArr[0];
                    start_port = Convert.ToInt32(sArr[1]);
                    end_port = Convert.ToInt32(sArr[2]);
                }
			}
            else
            {
                Console.WriteLine("Error incorrect syntax \r\nport_scanner [ip address] [start port] [end port] ");
				Validation.End_Program();
				return;
			}
            
            Network.ScanRange(ip.ToString(), start_port, end_port);
            
		}

        
        public static void Main(string[] args)
        {
			//string [] args = {"1","10","10.232.14.1"};
            if(args.Length < 2 || args.Length > 3)
            {
                Console.WriteLine("Error incorrect syntax \r\nport_scanner [ip address] [start port] [end port] ");
			}
            else
            {
                scan_start(args);
			}
			
            //Validation.End_Program();
			return;
		}
	}
}

//csc out: port_scanner.exe port_scanner.cs, validation.cs 