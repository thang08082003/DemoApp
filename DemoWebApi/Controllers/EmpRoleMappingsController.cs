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
    public class EmpRoleMappingsController : ControllerBase
    {
        private readonly IEmpRoleMappingsService _ierms;
        private readonly IServiceScopeFactory _scopeFactory;
        public EmpRoleMappingsController(IEmpRoleMappingsService ierms, IServiceScopeFactory scopeFactory)
        {
            _ierms = ierms;
            _scopeFactory = scopeFactory;
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorModel), StatusCodes.Status500InternalServerError)]
        [HttpPost("insert")]
        public async Task<IActionResult> Insert(EmpRoleMappingsModel EmpRole)
        {
            var japaneseTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Common.TimeZone.JAPAN_TIME_ZONE);
            EmpRole.created_at = TimeZoneInfo.ConvertTime(DateTime.Now, japaneseTimeZone);
            try
            {
                var result = await _ierms.Add(EmpRole);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
