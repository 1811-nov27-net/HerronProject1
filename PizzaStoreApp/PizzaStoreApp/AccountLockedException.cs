using System;
using System.Runtime.Serialization;

namespace PizzaStoreApp
{
    [Serializable]
    internal class AccountLockedException : Exception
    {
        public AccountLockedException()
        {
        }

        public AccountLockedException(string message) : base(message)
        {
        }

        public AccountLockedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountLockedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}