using DemoShared.Constants;
using DemoShared.Models;
using DemoWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using DemoShared.Models.Parameter;
using DemoShared.Models.Entities;
using DemoShared.Models.DTO;

namespace DemoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _ias;
        private readonly IServiceScopeFactory _scopeFactory;
        public AccountController(IAccountService ias, IServiceScopeFactory scopeFactory)
        {
            _ias = ias;
            _scopeFactory = scopeFactory;
        }

        [ProducesResponseType(typeof(EmployeeDTO[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorModel), StatusCodes.Status500InternalServerError)]
        [HttpGet("getUser/{empId}")]
        public async Task<IActionResult> getUserLogin(string empId)
        {
            try
            {
                var result = await _ias.getUserLogin(empId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorModel), StatusCodes.Status500InternalServerError)]
        [HttpPost("isExist")]
        public async Task<IActionResult> isExistAccount(AccountParam param)
        {
            try
            {
                var result = await _ias.isExistAccount(param);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [ProducesResponseType(typeof(AccountModel[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorModel), StatusCodes.Status500InternalServerError)]
        [HttpPost("getIdByUserName")]
        public async Task<IActionResult> getIdByUserName(AccountParam param)
        {
            try
            {
                var result = await _ias.getIdByUserName(param);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorModel), StatusCodes.Status500InternalServerError)]
        [HttpPost("insert")]
        public async Task<IActionResult> Insert(AccountModel account)
        {
            var japaneseTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Common.TimeZone.JAPAN_TIME_ZONE);
            account.created_at = TimeZoneInfo.ConvertTime(DateTime.Now, japaneseTimeZone);
            try
            {
                var result = await _ias.Add(account);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorModel), StatusCodes.Status500InternalServerError)]
        [HttpPost("update")]
        public async Task<IActionResult> Update(AccountModel account)
        {
            var japaneseTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Common.TimeZone.JAPAN_TIME_ZONE);
            account.updated_at = TimeZoneInfo.ConvertTime(DateTime.Now, japaneseTimeZone);
            try
            {
                var result = await _ias.Update(account);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [ProducesResponseType(typeof(AccountModel[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorModel), StatusCodes.Status500InternalServerError)]
        [HttpGet("getPatt/{empId}")]
        public async Task<IActionResult> getPattById(string empId)
        {
            try
            {
                var result = await _ias.getPattById(empId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
