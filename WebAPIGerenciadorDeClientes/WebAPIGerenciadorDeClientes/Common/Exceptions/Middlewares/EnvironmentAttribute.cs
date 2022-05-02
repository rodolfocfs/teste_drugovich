namespace WebAPIGerenciadorDeClientes.Common.Middlewares
{
    public class EnvironmentAttribute : Attribute
    {
        public string Allowed { get; set; }
        public string Forbidden { get; set; }

        public bool AllowedMode => string.IsNullOrEmpty(Forbidden?.Trim()) && !string.IsNullOrEmpty(Allowed?.Trim());
        public bool ForbiddenMode => string.IsNullOrEmpty(Allowed?.Trim()) && !string.IsNullOrEmpty(Forbidden?.Trim());

        public EnvironmentAttribute()
        {
        }

        public List<string> GetAllowedEnviroments() => string.IsNullOrEmpty(Allowed) ? new List<string>() : Allowed.Trim().Split(';').ToList();
        public List<string> GetForbiddenEnviroments() => string.IsNullOrEmpty(Forbidden) ? new List<string>() : Forbidden.Trim().Split(';').ToList();

        public bool IsAllowed(string env)
        {
            if (GetAllowedEnviroments().Any())
            {
                return GetAllowedEnviroments().Contains(env);
            }
            return true;
        }

        public bool IsForbidden(string env)
        {
            if (GetForbiddenEnviroments().Any())
            {
                return GetForbiddenEnviroments().Contains(env);
            }
            return false;
        }

    }
}
