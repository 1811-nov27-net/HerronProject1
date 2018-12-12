using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStoreAppLibrary
{
    [Serializable()]
    public class OrderTooLargeException : System.Exception
    {
        public OrderTooLargeException() : base() { }
        public OrderTooLargeException(string message) : base(message) { }
        public OrderTooLargeException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected OrderTooLargeException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
