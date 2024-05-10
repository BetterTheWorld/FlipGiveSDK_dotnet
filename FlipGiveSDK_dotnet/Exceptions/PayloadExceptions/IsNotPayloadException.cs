namespace FlipGiveSDK_dotnet.Exceptions.PayloadExceptions
{
    /// <summary>
    /// An exception to be thrown when an Token is not missing
    /// </summary>
    public class MissingPayloadException : AbstractPayloadException
    {
        /// <summary>
        /// Constructor with a message
        /// </summary>
        /// <param name="message">Message of the error</param>
        public MissingPayloadException(string message) : base(message) { }
    }
}
