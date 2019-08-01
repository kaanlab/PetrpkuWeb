using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace PetrpkuWeb.Client
{
    public interface IAppVersionService
    {
        string Version { get; }
    }

    /// <summary>
    /// Show version number from csproj
    /// </summary>
    public class AppVersionService : IAppVersionService
    {
        public string Version => SetVersion();
        private string SetVersion()
        {
            var attribute = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            if (attribute.InformationalVersion != null)
                return attribute.InformationalVersion;
            return "undefine";
        }
    }
}
