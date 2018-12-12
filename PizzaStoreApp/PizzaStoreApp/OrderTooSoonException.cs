using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStoreAppLibrary
{
    [Serializable()]
    public class OrderTooSoonException : System.Exception
    {
        public OrderTooSoonException() : base() { }
        public OrderTooSoonException(string message) : base(message) { }
        public OrderTooSoonException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected OrderTooSoonException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
