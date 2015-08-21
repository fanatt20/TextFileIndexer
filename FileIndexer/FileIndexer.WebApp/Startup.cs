using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FileIndexer.WebApp.Startup))]
namespace FileIndexer.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
