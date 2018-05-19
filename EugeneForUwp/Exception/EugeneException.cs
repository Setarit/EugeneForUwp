namespace EugeneForUwp.Exception
{
    public class EugeneException : System.Exception
    {
        public EugeneException()
        {
        }

        public EugeneException(string message) : base(message)
        {
        }

        public EugeneException(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
}
