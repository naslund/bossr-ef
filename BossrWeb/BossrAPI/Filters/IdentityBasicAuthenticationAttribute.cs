using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using BossrData;

namespace BossrAPI.Filters
{
    public class IdentityBasicAuthenticationAttribute : BasicAuthenticationAttribute
    {
        protected override async Task<IPrincipal> AuthenticateAsync(string userName, string password,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var context = new BossrContext())
            {
                if (context.Accounts.SingleOrDefault(x => x.Username == userName && x.Password == password) == null)
                    return null;
            }

            // Create a ClaimsIdentity with all the claims for this user.
            var nameClaim = new Claim(ClaimTypes.Name, userName);
            var claims = new List<Claim> {nameClaim};

            // important to set the identity this way, otherwise IsAuthenticated will be false
            // see: http://leastprivilege.com/2012/09/24/claimsidentity-isauthenticated-and-authenticationtype-in-net-4-5/
            var identity = new ClaimsIdentity(claims, AuthenticationTypes.Basic);

            var principal = new ClaimsPrincipal(identity);
            return principal;
        }
    }
}