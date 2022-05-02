using System.Reflection;

namespace WebAPIGerenciadorDeClientes.Common.Scope
{
    public class ScopeInformation
    {
        public ScopeInformation()
        {
            HostScopeInfo = new Dictionary<string, string>
            {
                { "MachineName", Environment.MachineName },
                { "EntryPoint", Assembly.GetEntryAssembly().GetName().Name }
            };
        }

        public Dictionary<string, string> HostScopeInfo { get; }
    }
}
