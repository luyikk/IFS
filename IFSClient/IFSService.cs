using Netx;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFSServ
{
    [Build]
    public interface IFSService
    {
        [TAG(1000)]
        Task<bool> LogOn(string username, string password);

        [TAG(1001)]
        Task<List<(string filename, string fullname, byte type, byte[] data)>> GetFs(string path);

    }
}
