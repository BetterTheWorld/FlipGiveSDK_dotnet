namespace FlipGiveSDK_dotnet.Exceptions.PayloadExceptions
{
    /// <summary>
    /// An exception to be thrown when an Token is not meeting minimum data
    /// </summary>
    public class MinimumPayloadException : AbstractPayloadException
    {
        /// <summary>
        /// Constructor with a message
        /// </summary>
        /// <param name="message">Message of the error</param>
        public MinimumPayloadException(string message) : base(message) { }
    }
}
