using System;
using Microsoft.AspNetCore.Mvc;
using StoreCRM.DTOs;
using StoreCRM.Interfaces;
using StoreCRM.Services;
namespace StoreCRM.Controllers
{
	public class LoginController : Controller
	{
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Login for existing users
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <response code="200">Successfully logged in, returns auth token</response>
        /// <response code="400">Bad input parameter(s) / User does not exist</response>
        [HttpPost("/login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO user)
        {
            try
            {
                var encodedJwt = await _userService.GetTokenAsync(user);
                return Ok(new { token = encodedJwt });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }


        /// <summary>
        /// Register a new user
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Successfully created a new user</response>
        /// <response code="400">Bad input parameter(s)</response>
        [HttpPost("/register")]
        public async Task<ActionResult> Register([FromBody] RegisterDTO user)
        {
            try
            {
                await _userService.AddNewUserAsync(user);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

