using AutoMapper;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Services.Contracts;

namespace Services
{
    public class AuthManager : IAuthService
    {
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly IMapper _mapper;

        public AuthManager(RoleManager<IdentityRole> rolemanager, UserManager<IdentityUser> usermanager, IMapper mapper)
        {
            _rolemanager = rolemanager;
            _usermanager = usermanager;
            _mapper = mapper;
        }

        public IEnumerable<IdentityRole> Roles => _rolemanager.Roles;

        public async Task<IdentityResult> CreateUser(UserDtoForCreation userDto)
        {
            var user = _mapper.Map<IdentityUser>(userDto);
            var result = await _usermanager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
                throw new Exception("User could not be created.");

            if (userDto.Roles.Count > 0)
            {
                var roleResult = await _usermanager.AddToRolesAsync(user, userDto.Roles);
                if (!roleResult.Succeeded)
                    throw new Exception("System have problems with roles.");
            }

            return result;
        }

        public async Task<IdentityResult> DeleteOneUser(string userName)
        {
            var user = await GetOneUser(userName);
            return await _usermanager.DeleteAsync(user);
        }

        public IEnumerable<IdentityUser> GetAllUser()
        {
            return _usermanager.Users.ToList();
        }

        public async Task<IdentityUser> GetOneUser(string userName)
        {
            var user = await _usermanager.FindByNameAsync(userName);
            return user;
        }

        public async Task<UserDtoForUpdate> GetOneUserForUpdate(string userName)
        {
            var user = await GetOneUser(userName);
            var userDto = _mapper.Map<UserDtoForUpdate>(user);
            userDto.Roles = new HashSet<string>(Roles.Select(r => r.Name).ToList());
            userDto.UserRoles = new HashSet<string>(await _usermanager.GetRolesAsync(user));
            return userDto;

        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordDto model)
        {
            var user = await GetOneUser(model.UserName);
            await _usermanager.RemovePasswordAsync(user);
            var result = await _usermanager.AddPasswordAsync(user, model.Password);
            return result;

        }

        public async Task Update(UserDtoForUpdate userDto)
        {
            var user = await GetOneUser(userDto.UserName);
            user.PhoneNumber = userDto.PhoneNumber;
            user.Email = userDto.Email;
            var result = await _usermanager.UpdateAsync(user);
            if (userDto.Roles.Count > 0)
            {
                var userRoles = await _usermanager.GetRolesAsync(user);
                var r1 = await _usermanager.RemoveFromRolesAsync(user, userRoles);
                var r2 = await _usermanager.AddToRolesAsync(user, userDto.Roles);
            }
            return;



        }
    }
}