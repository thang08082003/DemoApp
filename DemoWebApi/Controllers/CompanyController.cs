using DemoWebApi.Data;
using DemoShared.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoWebApi.Services;
using DemoShared.Models;

namespace DemoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _ics;
        private readonly IServiceScopeFactory _scopeFactory;
        public CompanyController(ICompanyService ics, IServiceScopeFactory scopeFactory)
        {
            _ics = ics;
            _scopeFactory = scopeFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(EmployeesModel[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorModel), StatusCodes.Status500InternalServerError)]
        [HttpGet("getAll")]
        public async Task<IActionResult> getAllCompany()
        {
            try
            {
                var result = await _ics.getAllCompany();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
