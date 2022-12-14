using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace SPFAdminSystem.Authentication
{
    // This class provides custom authentication state information by
    // extending the AuthenticationStateProvider class
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        // ProtectedSessionStorage is used to manage user sessions
        private readonly ProtectedSessionStorage _sessionStorage;

        // This ClaimsPrincipal represents an anonymous user
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        // returns the current authentication state
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                // Get the user session from the session storage
                var userSessionStorageResult = await _sessionStorage.GetAsync<UserSession>("UserSession");

                var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;

                // If there is no user session, return an AuthenticationState with the anonymous user
                if (userSession == null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));

                // Otherwise, create a ClaimsPrincipal with the user's claims and return an AuthenticationState with it
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.UserName),
                    new Claim(ClaimTypes.GivenName, userSession.GivenName),
                    new Claim(ClaimTypes.Role, userSession.Role)
                }, "CustomAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch (InvalidOperationException)
            {
                // raised everytime the ProtectedSessionStorage.GetAsync method is called before a UserSession is set in the sessionStorage
                // handled by returning an anonymous user
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }
        // informs all currently rendered components that a user has changed his authstate
        public async Task UpdateAuthenticationState(UserSession userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession != null)
            {
                await _sessionStorage.SetAsync("UserSession", userSession);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.UserName),
                    new Claim(ClaimTypes.GivenName, userSession.GivenName),
                    new Claim(ClaimTypes.Role, userSession.Role)
                }));
            }
            else
            {
                await _sessionStorage.DeleteAsync("UserSession");
                claimsPrincipal = _anonymous;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
