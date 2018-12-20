using System;
using System.Runtime.Serialization;

namespace PizzaStoreApp.DataAccess
{
    [Serializable]
    internal class DatabaseBadException : Exception
    {
        public DatabaseBadException()
        {
        }

        public DatabaseBadException(string message) : base(message)
        {
        }

        public DatabaseBadException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DatabaseBadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}