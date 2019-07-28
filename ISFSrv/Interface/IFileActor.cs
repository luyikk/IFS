using Netx;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IFSServ
{
    [Build]
    public interface IFileActor
    {
        [TAG(1)]
        Task<List<(string filename, string fullname, byte type, byte[] data)>> GetFileList(string basepath, string path);

        [TAG(2)]
        Task<bool> Del(string file);
    }
}
