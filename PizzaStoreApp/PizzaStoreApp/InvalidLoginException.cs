using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStoreAppLibrary
{
    [Serializable()]
    public class InvalidLoginException : System.Exception
    {
        public InvalidLoginException() : base() { }
        public InvalidLoginException(string message) : base(message) { }
        public InvalidLoginException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected InvalidLoginException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
