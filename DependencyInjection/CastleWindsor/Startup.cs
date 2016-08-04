using Castle.Windsor;
using DependencyInjection.CastleWindsor;

namespace CampAPI.Castle
{
    public class WebApiInstaller
    {
        public void Main()
        {
            var container = new WindsorContainer();

            container.Install(new Installer("DefaultConnection"));
        }

    }
}