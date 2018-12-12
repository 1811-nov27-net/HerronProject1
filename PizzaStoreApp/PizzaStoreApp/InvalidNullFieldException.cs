using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStoreAppLibrary
{
    [Serializable()]
    public class InvalidNullFieldException : System.Exception
    {
        public InvalidNullFieldException() : base() { }
        public InvalidNullFieldException(string message) : base(message) { }
        public InvalidNullFieldException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected InvalidNullFieldException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
