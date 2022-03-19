using IdentityModel.Client;
using IdentityModel.OidcClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorkBook
{
    public class AccessTokenHttpMessageHandler : DelegatingHandler
    {
        protected OidcClient OidcClient { get; }

        public AccessTokenHttpMessageHandler(OidcClient oidcClient) : base(new HttpClientHandler())
        {
            OidcClient = oidcClient;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string accessToken = await SecureStorage.GetAsync(OidcConsts.AccessTokenKeyName);

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                request.SetBearerToken(accessToken);
                request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                string refreshToken = await SecureStorage.GetAsync(OidcConsts.RefreshTokenKeyName);
                if (!string.IsNullOrWhiteSpace(refreshToken))
                {
                    var refreshResult = await OidcClient.RefreshTokenAsync(refreshToken);

                    await SecureStorage.SetAsync(OidcConsts.AccessTokenKeyName, refreshResult.AccessToken);
                    await SecureStorage.SetAsync(OidcConsts.RefreshTokenKeyName, refreshResult.RefreshToken);

                    request.SetBearerToken(refreshResult.AccessToken);

                    return await base.SendAsync(request, cancellationToken);
                }
                else
                {
                    var result = await OidcClient.LoginAsync(new LoginRequest());
                    request.SetBearerToken(result.AccessToken);

                    await SecureStorage.SetAsync(OidcConsts.AccessTokenKeyName, result.AccessToken);
                    await SecureStorage.SetAsync(OidcConsts.RefreshTokenKeyName, result.RefreshToken);

                    request.SetBearerToken(result.AccessToken);

                    return await base.SendAsync(request, cancellationToken);
                }
            }

            return response;
        }
    }
}
