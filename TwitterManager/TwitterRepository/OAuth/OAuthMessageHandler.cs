using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace TwitterHelper.OAuth
{
    /// <summary>
    /// Basic DelegatingHandler that creates an OAuth authorization header based on the OAuthBase
    /// class downloaded from http://oauth.net
    /// </summary>
    public class OAuthMessageHandler : DelegatingHandler
    {
        // Obtain these values by creating a Twitter app at http://dev.twitter.com/
        private static string _consumerKey = "lVAwXKGYmyt2xL8pdOyxLanMF";
        private static string _consumerSecret = "vMLRQy8vmyHK3XqAOxFyF5GdNYMakpkYJ9Bcw5c7BUVNFqcuRW";
        private static string _token = "568833316-ff3UAKTlYGv8GBFWc7saxgBKD777dFE6GvUjcRjd";
        private static string _tokenSecret = "GTOJNy8jrqKziSnxNjB4hAOHrmdk6RMgOUCorcAacAWw3";

        private OAuthBase _oAuthBase = new OAuthBase();

        public OAuthMessageHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Compute OAuth header 
            string normalizedUri;
            string normalizedParameters;
            string authHeader;

            string signature = _oAuthBase.GenerateSignature(
                request.RequestUri,
                _consumerKey,
                _consumerSecret,
                _token,
                _tokenSecret,
                request.Method.Method,
                _oAuthBase.GenerateTimeStamp(),
                _oAuthBase.GenerateNonce(),
                out normalizedUri,
                out normalizedParameters,
                out authHeader);

            request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", authHeader);
            return base.SendAsync(request, cancellationToken);
        }
    }
}