using FlipGiveSDK_dotnet.Models;
using Jose;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FlipGiveSDK_dotnet.Helpers
{
    /// <summary>
    /// JWE helper class using jose-jwt
    /// </summary>
    public class JweHelper
    {
        /// <summary>
        /// jwk created based on the secret provided
        /// </summary>
        private readonly Jwk _jwk;

        public JweHelper(string secret)
        {
            _jwk = new Jwk(Encoding.UTF8.GetBytes(secret));
        }

        /// <summary>
        /// This method is decrypting a token and deserialize to a Payload object
        /// </summary>
        /// <param name="token">The token to be decrypted</param>
        /// <returns>A Payload object</returns>
        public Payload? Decrypt(string token)
        {
            var plaintext = JWT.Decrypt(token, _jwk);

            return JsonSerializer.Deserialize<Payload>(plaintext);
        }

        /// <summary>
        /// This method is encrypting a Payload into a token
        /// </summary>
        /// <param name="payload">The Payload to be encrypted</param>
        /// <returns>A valid token</returns>
        public string Encrypt(Payload payload)
        {
            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
            };
            var jsonString = JsonSerializer.Serialize(payload, options);

            var preSharedKey = new byte[] { 0xA6, 0x4D, 0x0F, 0x12, 0x02, 0xBC, 0x14, 0xEA, 0x2E, 0xCC, 0x91, 0x8B };

            var extraHeaders = new Dictionary<string, object>
            {
                { "iv", preSharedKey }
            };
            string token = Jose.JWT.Encode(jsonString, _jwk, JweAlgorithm.DIR, JweEncryption.A128GCM, extraHeaders: extraHeaders);

            return token;
        }

        /// <summary>
        /// This method is encrypting an object into a token
        /// </summary>
        /// <param name="payload">The object to be encrypted</param>
        /// <returns>A valid token</returns>
        public string Encrypt(object payload)
        {
            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = true
            };
            var jsonString = JsonSerializer.Serialize(payload, options);

            string token = Jose.JWT.Encode(jsonString, _jwk, JweAlgorithm.DIR, JweEncryption.A128GCM);

            return token;
        }
    }
}
