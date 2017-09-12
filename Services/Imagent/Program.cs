using System.ServiceProcess;

namespace Kalendar.Zero.Imagent
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            log4net.Config.XmlConfigurator.Configure();

            var servicesToRun = new ServiceBase[]
            {
                new Aproxy(), 
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}