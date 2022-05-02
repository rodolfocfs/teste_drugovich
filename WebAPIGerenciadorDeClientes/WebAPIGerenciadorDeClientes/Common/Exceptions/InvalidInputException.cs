namespace WebAPIGerenciadorDeClientes.Common.Exceptions
{
    public class InvalidInputException : Exception
    {
        public IDictionary<string, object> Details { get; }

        public InvalidInputException(string message) : this(message, new Dictionary<string, object>())
        {
        }

        public InvalidInputException(string message, IDictionary<string, object> details) : base(message)
        {
            Details = details;
        }

        public InvalidInputException(string message, IDictionary<string, string> details) : base(message)
        {
            Details = details.ToDictionary(x => x.Key, x => (object)x.Value);
        }

        public InvalidInputException(string message, Exception inner) :
            this(message, new Dictionary<string, object>(), inner)
        {
        }

        public InvalidInputException(string message, IDictionary<string, object> details, Exception inner)
            : base(message, inner)
        {
            Details = details;
        }
    }
}
