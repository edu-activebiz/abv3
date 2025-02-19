using Microsoft.AspNetCore.Mvc;

using FMCGWebApplication.Models;


namespace WebFMCGSystem.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class UtilityHelperController : ControllerBase
    {
        [HttpGet]//("ConvertCode")]
        public IActionResult CodCnvZ(string input)
        //public IActionResult ConvertCode([FromQuery] string input)
        {
            if (input == null)
                return BadRequest(new { message = "Input cannot be null" });

            string formattedCode = UtilityHelper.CodCnvZ(input.ToUpper(), 6);
            return Ok(new { formattedCode });
           // return Json(new { formattedCode });
        }
    }

    
}
