namespace FlipGiveSDK_dotnet.Exceptions.PayloadExceptions
{
    /// <summary>
    /// An exception to be thrown when an Token is missing some required fields
    /// </summary>
    public class RequiredFieldInPayloadMissingException : AbstractPayloadException
    {
        /// <summary>
        /// Constructor with a message
        /// </summary>
        /// <param name="message">Message of the error</param>
        public RequiredFieldInPayloadMissingException(string message) : base(message) { }
    }
}
