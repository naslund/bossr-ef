using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BossrAPI.Models
{
    public class User : IdentityUser<int, BossrUserLogin, BossrUserRole, BossrUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }

    public class BossrUserRole : IdentityUserRole<int> { }
    public class BossrUserClaim : IdentityUserClaim<int> { }
    public class BossrUserLogin : IdentityUserLogin<int> { }

    public class Role : IdentityRole<int, BossrUserRole>
    {
        public Role() { }
        public Role(string name) { Name = name; }
    }

    public class BossrUserStore : UserStore<User, Role, int, BossrUserLogin, BossrUserRole, BossrUserClaim>
    {
        public BossrUserStore(BossrDbContext context) : base(context)
        {
        }
    }

    public class BossrRoleStore : RoleStore<Role, int, BossrUserRole>
    {
        public BossrRoleStore(BossrDbContext context) : base(context)
        {
        }
    }
}