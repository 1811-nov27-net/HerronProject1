using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStoreAppLibrary
{
    [Serializable()]
    public class InvalidOrderException : System.Exception
    {
        public InvalidOrderException() : base() { }
        public InvalidOrderException(string message) : base(message) { }
        public InvalidOrderException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected InvalidOrderException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
