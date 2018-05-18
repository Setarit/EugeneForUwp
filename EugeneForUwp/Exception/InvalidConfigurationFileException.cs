namespace EugeneForUwp.Exception
{
    class InvalidConfigurationFileException:EugeneException
    {
        public InvalidConfigurationFileException()
        {
        }

        public InvalidConfigurationFileException(string message) : base(message)
        {
        }

        public InvalidConfigurationFileException(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
}
