﻿using Application.DTOs;
using Application.Extensions;
using Application.Helpers;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (registerDTO == null)
            {
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Regis failed", "register"));
            }


            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetModelStateErrors();
                if (errors != null && errors.Count > 0)
                {
                    var msgBuilder = new StringBuilder();
                    return BadRequest(ApiResponseBuilder.GenerateBadRequest("Regis failed", "register"));
                }
            }


            var jwtToken = await _authService.Register(registerDTO);
            if (jwtToken == null || string.IsNullOrEmpty(jwtToken.JwtToken))
            {
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Regis failed", "register"));
            }

            return Ok(ApiResponseBuilder.GenerateOk(jwtToken, "OK", "success"));
        }

    }
}
