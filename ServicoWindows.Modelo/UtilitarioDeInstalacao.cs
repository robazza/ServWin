using System.Configuration.Install;
using System.Reflection;

namespace ServicoWindows.Modelo
{
    //ServiceInstallerUtility
    class UtilitarioDeInstalacao
    {
        private static readonly string exePath =
          Assembly.GetExecutingAssembly().Location;
        public static bool Instalar()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(new[] { "/LogFile=", exePath });
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool Desinstalar()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(new[] { "/LogFile=", "/u", exePath });
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
