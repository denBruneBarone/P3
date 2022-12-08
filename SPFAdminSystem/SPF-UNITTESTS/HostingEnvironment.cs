using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
namespace myHostingEnvironment
{
    internal class HostingEnvironment : IHostingEnvironment, Microsoft.Extensions.Hosting.IHostingEnvironment, IWebHostEnvironment
    {
        public string EnvironmentName { get; set; } = Microsoft.Extensions.Hosting.Environments.Production;

        public string ApplicationName { get; set; }

        public string WebRootPath { get; set; }

        public IFileProvider WebRootFileProvider { get; set; }

        public string ContentRootPath { get; set; }

        public IFileProvider ContentRootFileProvider { get; set; }
    }

}