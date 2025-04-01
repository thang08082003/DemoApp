using DemoShared.Models;
using Microsoft.AspNetCore.Mvc;
using DemoShared.Models.Parameter;

namespace DemoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class FileUploadController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorModel), StatusCodes.Status500InternalServerError)]
        [HttpPost("getFile")]
        public async Task<IActionResult> Uploadfile(FileUpload param)
        {
            var path = Directory.GetCurrentDirectory();
            path = Path.Combine(path, @"../DemoWeb/wwwroot/images");
            if (Directory.Exists(path) == false) Directory.CreateDirectory(path);
            path = Path.Combine(path, param.ImageName);
            try
            {
                await System.IO.File.WriteAllBytesAsync(path, param.UserImage);
                return Ok(path);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
