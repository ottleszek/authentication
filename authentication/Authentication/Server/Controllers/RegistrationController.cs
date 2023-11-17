﻿using Authentication.Server.Services;
using AuthenticationLibrary.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;
       
        public RegistrationController(IRegistrationService registationService)
        {
            _registrationService = registationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> UserRegistration([FromBody] UserRegistrationDto registrationPlayload)
        {
            AuthenticationResponseDto result = await _registrationService.RegisterNewUser(registrationPlayload);
            if (result.IsAuthenticationSuccessful)
            {
                return Ok(result);
            }
            else
                return BadRequest(result);
        }

        [HttpGet("check-unique-user-email")]
        public IActionResult ChaeckUniqueUserEmail(string email)
        {
            bool result = _registrationService.ChaeckUniqueUserEmail(email);
            return Ok(result);
        }           
    }
}
