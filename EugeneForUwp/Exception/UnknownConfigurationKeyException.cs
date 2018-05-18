namespace EugeneForUwp.Exception
{
    class UnknownConfigurationKeyException:EugeneException
    {
        public UnknownConfigurationKeyException()
        {
        }

        public UnknownConfigurationKeyException(string message) : base(message)
        {
        }

        public UnknownConfigurationKeyException(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
}
