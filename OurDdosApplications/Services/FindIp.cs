using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace KillRussia2WithDdos
{
    public class FindIp
    {
        public static IEnumerable<string> FindIpAddresses(string url)
        {
            try
            {
                return GetIpAddreses(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }

        }
        private static IEnumerable<string> GetIpAddreses(string hostname)
        {
            if (!hostname.Contains(".ru"))
            {
                return new string[] {hostname};
            }
            List<string> list = new();
            foreach (var item in Dns.GetHostEntry(hostname).AddressList)
            {
                list.Add(item.ToString());
            }
            return list;
        }
    }
}
