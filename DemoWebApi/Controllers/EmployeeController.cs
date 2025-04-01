using DemoShared.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using DemoShared.Models;
using DemoWebApi.Services;
using DemoShared.Constants;
using DemoShared.Models.DTO;


namespace DemoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeesService _ies;
        private readonly IServiceScopeFactory _scopeFactory;
        public EmployeeController(IEmployeesService ies, IServiceScopeFactory scopeFactory)
        {
            _ies = ies;
            _scopeFactory = scopeFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(EmployeesModel[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorModel), StatusCodes.Status500InternalServerError)]
        [HttpGet("getAll")]
        public async Task<IActionResult> getAllEmployee()
        {
            try
            {
                var result = await _ies.getAllEmployee();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(EmployeesModel[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorModel), StatusCodes.Status500InternalServerError)]
        [HttpGet("searchByName/{name}")]
        public async Task<IActionResult> SearchEmployee(string name)
        {
            try
            {
                var result = await _ies.SearchEmployee(name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorModel), StatusCodes.Status500InternalServerError)]
        [HttpPost("insert")]
        public async Task<IActionResult> Insert(EmployeesModel employee)
        {
            var japaneseTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Common.TimeZone.JAPAN_TIME_ZONE);
            employee.created_at = TimeZoneInfo.ConvertTime(DateTime.Now, japaneseTimeZone);
            try
            {
                var result = await _ies.Add(employee);
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
        public async Task<IActionResult> isExistEmployee(List<EmployeesModel> employeeLst)
        {
            try
            {
                var result = await _ies.isExistEmployee(employeeLst);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorModel), StatusCodes.Status500InternalServerError)]
        [HttpPost("update")]
        public async Task<IActionResult> Update(EmployeesModel employee)
        {
            var japaneseTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Common.TimeZone.JAPAN_TIME_ZONE);
            employee.updated_at = TimeZoneInfo.ConvertTime(DateTime.Now, japaneseTimeZone);
            try
            {
                var result = await _ies.Update(employee);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorModel), StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _ies.DeleteById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorModel), StatusCodes.Status500InternalServerError)]
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteItems(List<EmployeesModel> employees)
        {
            try
            {
                var result = await _ies.DeleteItems(employees);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorModel), StatusCodes.Status500InternalServerError)]
        [HttpPost("insertLst")]
        public async Task<IActionResult> CreateItems(List<EmployeesModel> employees)
        {
            try
            {
                var result = await _ies.AddItems(employees);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorModel), StatusCodes.Status500InternalServerError)]
        [HttpPost("updateLst")]
        public async Task<IActionResult> UpdateItems(List<EmployeesModel> employees)
        {
            try
            {
                var result = await _ies.UpdateItems(employees);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [ProducesResponseType(typeof(EmployeesModel[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorModel), StatusCodes.Status500InternalServerError)]
        [HttpGet("getInfor/{empId}")]
        public async Task<IActionResult> getInfor(string empId)
        {
            try
            {
                var result = await _ies.getInfor(empId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
