using System;

namespace FlipGiveSDK_dotnet.Exceptions
{
    /// <summary>
    /// An exception to be thrown when an invalid token is provided
    /// </summary>
    public class InvalidTokenException : ApplicationException
    {
        /// <summary>
        /// Constructor with a message
        /// </summary>
        /// <param name="message">Message of the error</param>
        public InvalidTokenException(string message) : base(message) { }
    }
}
