using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.BusinessLogicClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SamplePOC.Helpers;
using ViewModels;
using ViewModels.Helpers;

namespace SamplePOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationsController : ControllerBase
    {
        public IUserOperationsService _UserOperations { get; set; }

        public UserOperationsController(IUserOperationsService UserOperationsService)
        {
            _UserOperations = UserOperationsService;
        }

        [Authorize]
        [HttpGet("UserList")]
        public IActionResult GetUserList()
        {
            TransactionResult<List<UserVM>> result = _UserOperations.BL_GetUserList();

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(result.Data);
        }

        [Authorize]
        [HttpGet("ProfileData/{UserId}")]
        public IActionResult GetProfileData(int UserId)
        {
            TransactionResult<UserVM> result = _UserOperations.BL_GetProfileData(UserId);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(result.Data);
        }

        [Authorize]
        [HttpPost("SaveUserDetails")]
        public IActionResult SaveUserDetails(UserVM UserVM)
        {
            TransactionResult<object> result = _UserOperations.BL_SaveUserDetails(UserVM);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }

        [Authorize]
        [HttpPost("SaveCountry")]
        public IActionResult SaveCountry(CountryVM CountryVM)
        {
            TransactionResult<List<CountryVM>> result = _UserOperations.BL_SaveCountry(CountryVM);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(result.Data);
        }

        [Authorize]
        [HttpGet("DeleteCountry/{CountryId}/{UserId}")]
        public IActionResult DeleteCountry(int CountryId,int UserId)
        {
            TransactionResult<List<CountryVM>> result = _UserOperations.BL_DeleteCountry(CountryId,UserId);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(result.Data);
        }

        [Authorize]
        [HttpGet("GetCountries/{UserId}")]
        public IActionResult GetCountries(int UserId)
        {
            TransactionResult<List<CountryVM>> result = _UserOperations.BL_GetCountries(UserId);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(result.Data);
        }
    }
}
