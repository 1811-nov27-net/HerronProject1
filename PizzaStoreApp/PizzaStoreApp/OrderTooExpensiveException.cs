using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStoreAppLibrary
{
    [Serializable()]
    public class OrderTooExpensiveException : System.Exception
    {
        public OrderTooExpensiveException() : base() { }
        public OrderTooExpensiveException(string message) : base(message) { }
        public OrderTooExpensiveException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected OrderTooExpensiveException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
