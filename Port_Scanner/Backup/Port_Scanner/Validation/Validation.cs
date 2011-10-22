using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Port_Scanner
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
}
