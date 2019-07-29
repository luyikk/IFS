using Netx.Actor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IFSServ
{
    [ActorOption(maxQueueCount: 1500, ideltime: 3000)]
    public class IFSActorController : ActorController, IFileActor
    {
        
        [Netx.Actor.Open(OpenAccess.Internal)]
        public Task<List<(string filename, string fullname, byte type, byte[] data)>> GetFileList(string basepath,string path)
        {
            var files = from p in GetFileInfos(path)
                        select
                        (
                            p.Name,
                            Path.GetRelativePath(basepath, p.FullName),
                            p is FileInfo ? (byte)0 : (byte)1,
                            p is FileInfo ? GetMD5HashFromFile(p.FullName) : null
                        );


            return Task.FromResult(files.ToList());
        }

        private FileSystemInfo[] GetFileInfos(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            if (!directoryInfo.Exists)
            {
                throw new IOException($"not find path:{path}");
            }
            else
            {                              
                return directoryInfo.GetFileSystemInfos();
            }
        }

        [Netx.Actor.Open(OpenAccess.Internal)]
        public Task<bool> Del(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }



        public byte[] GetMD5HashFromFile(string fileName)
        {
            using (FileStream file = new FileStream(fileName, System.IO.FileMode.Open,FileAccess.Read))
            {
                MD5 md5 = new MD5CryptoServiceProvider();

                return md5.ComputeHash(file);   
            }
        }


    }
}
