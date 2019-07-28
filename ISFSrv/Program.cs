using System;
using Netx.Service.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using IFSServ.Interface;

namespace IFSServ
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new NetxServBuilder()
                .ConfigBase(p =>
                {
                    p.VerifyKey = "123123";
                    p.ServiceName = "IFServ";
                })
                .ConfigNetWork(p => p.Port = 1322)
                .RegisterDescriptors(p =>
                {
                    p.Configure<IFSOption>(option =>
                    {
                        option.Path = "./file";
                    });
                    p.AddSingleton<IUserVerification, UserVerification>();
                })
                .RegisterService(Assembly.GetExecutingAssembly())
                .Build();

            server.Start();


            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}
