﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petalaka.Account.API.Base;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Service.Interface;

namespace Petalaka.Account.API.Controllers;

public class AccountController : BaseController
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpPost]
    [Route("v1/register")]
    public async Task<IActionResult> RegisterAccount([FromBody] RegisterRequestModel request)
    {
        await _accountService.RegisterAccount(request);
        return Ok();
    }
    
    [HttpGet]
    [Route("v1/users")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _accountService.GetAllUsers());
    }

    [HttpPost]
    [Route("v1/login")]
    public async Task<ActionResult<(string accessToken, string refreshToken)>> Login([FromBody] LoginRequestModel request)
    {
        var a = await _accountService.Login(request);
        return Ok(a);
    }
  
    
}