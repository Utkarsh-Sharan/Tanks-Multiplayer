using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace Game.Network
{
    public static class AuthenticationWrapper
    {
        public static AuthState AuthState { get; private set; } = AuthState.NotAuthenticated;
        
        public static async Task<AuthState> DoAuth(int maxRetries = 5)
        {
            if (AuthState == AuthState.Authenticated)
                return AuthState;

            if(AuthState == AuthState.Authenticating)
            {
                await Authenticating();
                return AuthState;
            }

            await SignInAnonymouslyAsync(maxRetries);

            return AuthState;
        }

        private static async Task<AuthState> Authenticating()
        {
            while (AuthState == AuthState.Authenticating || AuthState == AuthState.NotAuthenticated)
                await Task.Delay(200);

            return AuthState;
        }

        private static async Task SignInAnonymouslyAsync(int maxRetries)
        {
            AuthState = AuthState.Authenticating;

            int retries = 0;
            while (AuthState == AuthState.Authenticating && retries < maxRetries)
            {
                try
                {
                    await AuthenticationService.Instance.SignInAnonymouslyAsync();

                    if (AuthenticationService.Instance.IsSignedIn && AuthenticationService.Instance.IsAuthorized)
                    {
                        AuthState = AuthState.Authenticated;
                        break;
                    }
                }
                catch(AuthenticationException authEx)
                {
                    Debug.Log(authEx);
                    AuthState = AuthState.Error;
                }
                catch(RequestFailedException reqFailEx)
                {
                    Debug.Log(reqFailEx);
                    AuthState = AuthState.Error;
                }

                ++retries;
                await Task.Delay(1000);
            }

            if(AuthState != AuthState.Authenticated)    //we ran out of authentication tries without any errors.
            {
                Debug.LogWarning($"Player did not sign in after {retries} retries!");
                AuthState = AuthState.TimeOut;
            }
        }
    }

    public enum AuthState
    {
        NotAuthenticated,
        Authenticating,
        Authenticated,
        Error,
        TimeOut
    }
}