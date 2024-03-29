﻿using Business.Abstract;
using Business.Dtos.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginRequest userForLoginRequest)
        {
            var userToLogin = _authService.Login(userForLoginRequest);
            if (userToLogin == null)
            {
                return BadRequest();
            }

            var result = _authService.CreateAccessToken(userToLogin.Result);
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterRequest userForRegisterRequest)
        {
            var userExists = _authService.UserExists(userForRegisterRequest.Email);
            if (userExists != null )
            {
                return BadRequest(userExists);
            }

            var registerResult = _authService.Register(userForRegisterRequest, userForRegisterRequest.Password);
            var result = _authService.CreateAccessToken(registerResult.Result);
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
