using Microsoft.AspNetCore.Identity;
using Services.Contracts;

namespace Services{
    public class AuthManager : IAuthService
    {
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly UserManager<IdentityUser> _usermanager;
        public AuthManager(RoleManager<IdentityRole> rolemanager, UserManager<IdentityUser> usermanager)
        {
            _rolemanager = rolemanager;
            _usermanager = usermanager;
        }

        public IEnumerable<IdentityRole> Roles => _rolemanager.Roles;

        public IEnumerable<IdentityUser> GetAllUser()
        {
            return _usermanager.Users.ToList();
        }
    }
}