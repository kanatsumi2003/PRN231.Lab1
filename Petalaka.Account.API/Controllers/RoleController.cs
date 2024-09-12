using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petalaka.Account.API.Base;
using Petalaka.Account.Contract.Service.Interface;

namespace Petalaka.Account.API.Controllers;

public class RoleController : BaseController
{
    private readonly IRoleService _roleService;
    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }
    
    [HttpPost]
    [Route("v1/roles")]
    public async Task<IActionResult> CreateRole([FromBody] string roleName)
    {
        var result = await _roleService.CreateRoleAsync(roleName);
        return Ok(result);
    }
    
    [HttpGet]
    [Route("v1/roles")]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> GetRoles()
    {
        return Ok(await _roleService.GetRolesAsync());
    }
}