using Microsoft.Extensions.DependencyInjection;
using System;
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
        public async void TestGetFileInfo()
        {
            var files= await ( new ImageSrv.FileActorController()).GetFileList("./");
            Assert.NotNull(files);
            output.WriteLine($"file length:{files.Count}");
            
        }
    }
}
