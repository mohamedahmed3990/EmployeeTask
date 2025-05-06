using CRUD_Task.BLL.DTOs;
using CRUD_Task.BLL.Services.EmployeeServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Task.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }



        [HttpGet]
        public async Task<Ok<List<EmployeeReadDto>>> GetEmployees()
        {
            var employees = await _employeeService.GetAllAsync();
            return TypedResults.Ok(employees);
        }

        [HttpPost]

        public async Task<ActionResult> AddEmployee([FromBody] EmployeeAddDto employeeAddDto)
        {

            try
            {
                await _employeeService.AddAsync(employeeAddDto);

                return Ok(new { message = "Employee added successfully." });
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }


        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeReadDto>> GetEmployee(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDoctor(int id, [FromBody] EmployeeUpdateDto employeeUpdateDto)
        {
         
            var updatedEmp = await _employeeService.UpdateAsync(id, employeeUpdateDto );
            
            if (updatedEmp is null)
            {
                return NotFound();
            }

            return Ok(new { message = "Employee added successfully." });

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeService.DeleteAsync(id);

            if (!employee)
            {
                return NotFound();
            }

            return NoContent();

        }


        [HttpGet("search/{name}")]
        public async Task<IActionResult> SearchEmployeeByName(string name)
        {
            try
            {
                var employees = await _employeeService.SearchEmployeeAsync(name);
                if (employees == null)
                {
                    return NotFound($"Employee with name {name} not found.");
                }
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("pagination")]
        [HttpGet("pagination/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetAllEmployees(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0 || pageSize <= 0)
                    return BadRequest("Page number and size must be greater than 0.");

                var employees = await _employeeService.GetEmployeesAsync(pageNumber, pageSize);
                if (employees == null)
                    return NotFound("No employees found.");
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }





    }
}
