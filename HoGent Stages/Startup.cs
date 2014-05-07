using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HoGent_Stages.Startup))]

namespace HoGent_Stages
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
