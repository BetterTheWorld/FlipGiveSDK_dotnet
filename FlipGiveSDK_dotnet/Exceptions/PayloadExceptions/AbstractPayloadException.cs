using System;

namespace FlipGiveSDK_dotnet.Exceptions.PayloadExceptions
{
    /// <summary>
    /// An exception to be thrown when an Token is not validated
    /// </summary>
    public abstract class AbstractPayloadException : ApplicationException
    {
        /// <summary>
        /// Constructor with a message
        /// </summary>
        /// <param name="message">Message of the error</param>
        public AbstractPayloadException(string message) : base(message) { }
    }
}
