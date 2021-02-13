using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.BusinessLogicClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModels;
using ViewModels.Helpers;

namespace SamplePOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthenticationController : ControllerBase
    {
        public IUserAuthenticationService _UserAuthentication { get; set; }

        public UserAuthenticationController(IUserAuthenticationService AuthenticationService)
        {
            _UserAuthentication = AuthenticationService;
        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserVM model)
        {
            TransactionResult<UserVM> result = _UserAuthentication.BL_Login(model.Email, model.Password);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(result.Data);
        }

        [AllowAnonymous]
        [HttpPost("Registration")]
        public IActionResult UserRegistration(UserVM p_UserDetailsModel)
        {
            TransactionResult<UserVM> result = _UserAuthentication.BL_Registration(p_UserDetailsModel);
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(result.Data);
        }
    }
}
