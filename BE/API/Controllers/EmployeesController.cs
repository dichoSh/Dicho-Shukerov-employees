using Employees.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController(IEmployeeService employeeService) : ControllerBase
    {
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file, CancellationToken ct)
        {
            var result = await employeeService.FindLongestPeriodTeam(file?.OpenReadStream(), ct);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.TeamProjectDays);
        }
    }
}
