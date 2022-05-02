using System.Net;

namespace WebAPIGerenciadorDeClientes.Common.Exceptions
{
    public class ApiError
    {
        public static string DefaultErrorMessage = "Erro no processamento da requisição.";

        public string Id { get; } = Guid.NewGuid().ToString();
        public HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;
        public string Title { get; set; } = "API Error";
        public string Message { get; set; } = DefaultErrorMessage;
        public IDictionary<string, object> Details { get; set; } = new Dictionary<string, object>();
    }
}