using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Contract.Service.Interface;

namespace Petalaka.Account.Service.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IUnitOfWork _unitOfWork;
    public RoleService(RoleManager<ApplicationRole> roleManager, 
        IUnitOfWork unitOfWork)
    {
        _roleManager = roleManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<IdentityResult> CreateRoleAsync(string roleName)
    {
        var role = new ApplicationRole
        {
            Name = roleName
        };
        return await _roleManager.CreateAsync(role);
    }

    public async Task<List<ApplicationRole>> GetRolesAsync()
    {
        return await _roleManager.Roles.ToListAsync();
    }
}