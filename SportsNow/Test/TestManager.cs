using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Es.Udc.DotNet.SportsNow.Test
{
    public class TestManager
    {

        public static IUnityContainer ConfigureUnityContainer(string sectionName)
        {
            IUnityContainer container = new UnityContainer();

            UnityConfigurationSection section =
                (UnityConfigurationSection)ConfigurationManager.GetSection(sectionName);

            section.Configure(container, section.Containers.Default.Name);

            return container;
        }


        public static void ClearUnityContainer(IUnityContainer container)
        {

            container.Dispose();
        }

    }
}
