using Microsoft.AspNetCore.Identity;
using Petalaka.Account.Contract.Repository.Entities;

namespace Petalaka.Account.Contract.Service.Interface;

public interface IRoleService
{
    Task<IdentityResult> CreateRoleAsync(string roleName);
    Task<List<ApplicationRole>> GetRolesAsync();
}