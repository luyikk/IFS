using IFSServ;
using Netx.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFS
{
    public class IFSClient
    {
        public NetxSClient Client { get; }
        public INetxSClientBuilder INetxtBuilder { get; }
        private IFSService service { get; }
        public IFSClient(string host, int port, string key)
        {
            INetxtBuilder = new NetxSClientBuilder()
             .ConfigConnection(p => //配置服务器IP
             {
                 p.Host = host;
                 p.Port = port;
                 p.VerifyKey = key;
                 p.ServiceName = "IFServ";
             })
             .ConfigSessionStore(() => new Netx.Client.Session.SessionFile());

            Client = INetxtBuilder.Build();

          
        }
        
        public IFSService GetFSService()=> Client.Get<IFSService>();     

    }
}
