namespace EugeneForUwp.Exception
{
    class InvalidRemoteVersion:EugeneException
    {
        public InvalidRemoteVersion()
        {
        }

        public InvalidRemoteVersion(string message) : base(message)
        {
        }

        public InvalidRemoteVersion(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
}
