using IFS;
using IFSServ;
using IFSServ.Interface;
using Microsoft.Extensions.DependencyInjection;
using Netx.Actor;
using Netx.Service.Builder;
using System;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace Unit.serv
{
    public class UnitFileInfoGet
    {
       
        private readonly ITestOutputHelper output;
        public UnitFileInfoGet(ITestOutputHelper tempOutput)
        {
            output = tempOutput;  
        }


        [Fact]
        public async void TestServer()
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
                       option.Path = "./";
                   });
                   p.AddSingleton<IUserVerification, UserVerification>();
               })
               .RegisterService(typeof(IFSController).Assembly)
               .Build();

            server.Start();

            var client = new IFSClient("127.0.0.1", 1322, "123123");
            var service = client.GetFSService();
            await service.LogOn("username", "password");
            var dir = await service.GetFs("./");            

            Assert.True(dir.Count > 0);
            server.Stop();
        }

        [Fact]
        public async void TestGetFileInfo()
        {
            var files = await (new IFSActorController()).GetFileList("./", "./");
            Assert.NotNull(files);
            output.WriteLine($"file length:{files.Count}");            
        }

    }
}
