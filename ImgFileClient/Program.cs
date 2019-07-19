using Netx.Client;
using System;
using System.Threading.Tasks;
using IFS;

namespace ImgFileClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new IFSClient("127.0.0.1", 1322, "123123");

            var service= client.GetFSService();

            if (await service.LogOn("username", "password"))
            {
                try
                {
                    var dir = await service.GetFs("././BaiduYun");
       

                }
                catch (Exception er)
                {
                    Console.WriteLine(er.Message);
                }
            }
            else
            {
                Console.WriteLine("username or password error!");
            }

            Console.ReadLine();
        }
    }
}
