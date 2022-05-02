namespace WebAPIGerenciadorDeClientes.Common.Exceptions
{

    public class ApiExceptionOptions
    {
        public Func<Exception, ApiError> AddResponseDetails { get; set; }
        public Func<Exception, LogLevel> DetermineLogLevel { get; set; }
    }
}