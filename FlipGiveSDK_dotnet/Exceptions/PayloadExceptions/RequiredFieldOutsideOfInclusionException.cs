namespace FlipGiveSDK_dotnet.Exceptions.PayloadExceptions
{
    /// <summary>
    /// An exception to be thrown when an Token is containing some fileds that are not in the inclusion list
    /// </summary>
    public class RequiredFieldOutsideOfInclusionException : AbstractPayloadException
    {
        /// <summary>
        /// Constructor with a message
        /// </summary>
        /// <param name="message">Message of the error</param>
        public RequiredFieldOutsideOfInclusionException(string message) : base(message) { }
    }
}
