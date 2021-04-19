using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using contractor.Models;
using contractor.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace contractor.Controllers
{
    [ApiController]
    [Route("[controller]")]

    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly ProfilesService _pservice;

        public AccountController(ProfilesService pservice)
        {
            _pservice = pservice;
        }


        [HttpGet]
        // REVIEW async calls must return a System.Threading.Tasks, this is equivalent to a promise in JS
        public async Task<ActionResult<Profile>> GetAsync()
        {
            try
            {
                // REVIEW how to get the user info from the request token
                // same as to req.userInfo
                //MAKE SURE TO BRING IN codeworks.auth0provider
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_pservice.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}