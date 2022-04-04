using System;
using System.Runtime.Serialization;

namespace Banks.Tools
{
    public class BanksExceptions : Exception
    {
        public BanksExceptions()
        {
        }

        public BanksExceptions(string message)
            : base(message)
        {
        }

        public BanksExceptions(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected BanksExceptions(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}