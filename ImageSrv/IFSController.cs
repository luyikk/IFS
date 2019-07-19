using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Netx.Loggine;
using Netx.Service;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IFSServ.Interface;

namespace IFSServ
{
    public class IFSController : AsyncController,IFSService
    {
        public ILog Log { get; }
        public IFSOption Option { get; }
        public string BasePath => Path.GetFullPath(Option.Path);
        public IUserVerification Verification { get; }
        public bool LogIn { get; private set; }

        public IFSController(IUserVerification verification, IOptions<IFSOption> options, ILogger<IFSController> logger)
        {
            Log = new DefaultLog(logger);
            Option = options.Value;
           
            Verification = verification;

            if (!Directory.Exists(Option.Path))
            {
                Log.Info($"Create base path {Option.Path}");
                Directory.CreateDirectory(Option.Path);
            }
        }
        public Task<bool> LogOn(string username, string password)
        {
            LogIn = Verification.Verification(username, password);
            return Task.FromResult(LogIn);
        }


        public async Task<List<(string filename,string fullname,byte type,byte[] data)>> GetFs(string path)
        {
            checkLogIN();

            var ls=  Path.GetFullPath( path, BasePath);
            if (ls.IndexOf(BasePath) == 0)
            {              
                return await Actor<IFileActor>().GetFileList(BasePath,ls);                
            }
            else
            {
                throw new IOException("Permission denied");
            }
        }


        private void checkLogIN()
        {
            if (!LogIn)
                throw new Exception("Permission denied");
        }
      
    }
}
