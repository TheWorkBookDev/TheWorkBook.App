﻿using IdentityModel.Client;
using IdentityModel.OidcClient;

namespace TheWorkBook;

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
            // If we were just checking if the user was authenticated, then we don't want to start an auth flow.
            if (request.RequestUri.ToString().Contains("authcheck"))
            {
                // Here, we allow the Unauthorized response to return.This is because
                // we don't want to start an auth flow if the user isn't authenticated.
                return response;
            }

            string refreshToken = await SecureStorage.GetAsync(OidcConsts.RefreshTokenKeyName);
            if (!string.IsNullOrWhiteSpace(refreshToken))
            {
                var refreshResult = await OidcClient.RefreshTokenAsync(refreshToken);

                await SecureStorage.SetAsync(OidcConsts.AccessTokenKeyName, refreshResult.AccessToken);
                await SecureStorage.SetAsync(OidcConsts.RefreshTokenKeyName, refreshResult.RefreshToken);
                await SecureStorage.SetAsync(OidcConsts.IdentityTokenKeyName, refreshResult.IdentityToken);

                request.SetBearerToken(refreshResult.AccessToken);

                return await base.SendAsync(request, cancellationToken);
            }
            else
            {
                var result = await OidcClient.LoginAsync(new LoginRequest());
                request.SetBearerToken(result.AccessToken);

                await SecureStorage.SetAsync(OidcConsts.AccessTokenKeyName, result.AccessToken);
                await SecureStorage.SetAsync(OidcConsts.RefreshTokenKeyName, result.RefreshToken);
                await SecureStorage.SetAsync(OidcConsts.IdentityTokenKeyName, result.IdentityToken);

                return await base.SendAsync(request, cancellationToken);
            }
        }

        return response;
    }
}