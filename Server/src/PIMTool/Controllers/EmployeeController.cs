using Microsoft.AspNetCore.Mvc;
using PIMTool.Core.Contracts.Response;
using PIMTool.Core.Interfaces.Services;

namespace PIMTool.Controllers
{
    public class EmployeeController : ApiController
    {
        private IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public ActionResult<EmployeeResponse> GetEmployeesByVisas([FromQuery] string visa)
        {
            var employees = _employeeService.GetEmployee(visa);
            return Ok(employees);
        }

        [HttpGet("find-visas")]
        public ActionResult<EmployeeResponse> FindVisasByProjectId([FromQuery] int projectId)
        {
            var visas = _employeeService.GetEmployeeVisas(projectId);
            return Ok(visas);
        }
    }
}
