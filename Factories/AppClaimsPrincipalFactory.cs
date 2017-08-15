using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using BankAccounts.Models;

namespace BankAccounts.Factories
{
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public AppClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor) { }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);

            if (!string.IsNullOrWhiteSpace(user.FirstName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                    new Claim (ClaimTypes.GivenName, user.FirstName)
                });
            }

            if (!string.IsNullOrWhiteSpace(user.LastName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                    new Claim (ClaimTypes.Surname, user.LastName),
                });
            }

            if (!string.IsNullOrWhiteSpace(user.FullName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                    new Claim (ClaimTypes.Name, user.FullName),
                });
            }

            return principal;
        }
    }
}
