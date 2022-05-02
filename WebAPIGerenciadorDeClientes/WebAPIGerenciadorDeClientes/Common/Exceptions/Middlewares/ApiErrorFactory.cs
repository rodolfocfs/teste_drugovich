using System.Data;
using static System.Net.HttpStatusCode;
using System.Security.Authentication;

namespace WebAPIGerenciadorDeClientes.Common.Exceptions.Middleware
{
    public static class ApiErrorFactory
    {
        public static string message = "Um erro interno no sistema ocorreu. Consulte o administrador do sistema.";
        public static string unauthorizedMessage = "Acesso não autorizado.";
        public static ApiError New(Exception exception) =>
            exception switch
            {
                UnauthorizedAccessException e => new ApiError
                {
                    Message = unauthorizedMessage,
                    Status = Unauthorized
                },
                InvalidInputException e => new ApiError
                {
                    Details = e.Details,
                    Message = e.Message,
                    Status = InternalServerError
                },
                InvalidCredentialException e => new ApiError
                {
                    Message = e.Message,
                    Status = InternalServerError
                },              
                DataException e => new ApiError
                {
                    Message = message,
                    Status = BadRequest
                },              
                AggregateException e => new ApiError
                {
                    Message = message,
                    Status = BadRequest
                },              
                NotSupportedException e => new ApiError
                {
                    Message = message,
                    Status = InternalServerError
                },
                Exception e => new ApiError
                {
                    Message = message,
                    Status = InternalServerError
                },
                _ => new ApiError()
            };
    }
}
