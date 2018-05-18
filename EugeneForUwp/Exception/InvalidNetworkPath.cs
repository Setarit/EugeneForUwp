namespace EugeneForUwp.Exception
{
    class InvalidNetworkPath:EugeneException
    {
        public InvalidNetworkPath()
        {
        }

        public InvalidNetworkPath(string message) : base(message)
        {
        }

        public InvalidNetworkPath(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
}
