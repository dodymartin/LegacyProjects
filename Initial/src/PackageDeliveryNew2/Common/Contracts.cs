using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace PackageDeliveryNew2.Common
{
    public static class Contracts
    {
        [DebuggerStepThrough]
        public static void Require(bool precondition, string message = "")
        {
            if (!precondition)
                throw new ContractException(message);
        }
    }

    [Serializable]
    public class ContractException : Exception
    {
        public ContractException() { }

        public ContractException(string message)
            : base(message) { }

        public ContractException(string message, Exception innerException)
            : base(message, innerException) { }

        protected ContractException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
