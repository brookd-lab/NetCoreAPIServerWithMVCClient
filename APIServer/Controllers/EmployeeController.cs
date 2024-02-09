using APIServer.Models;
using APIServer.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIServer.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]

    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _service;

        public EmployeeController(
            ILogger<EmployeeController> logger,
            IEmployeeService service)
        {
            _logger = logger;
            _service= service;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
        {
            var employeeList = await _service.GetAllEmployees();
            return employeeList;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _service.GetEmployeeById(id);
            return employee == null ? NotFound() : Ok(employee);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Employee employee)
        {
            await _service.CreateEmployee(employee);

            return CreatedAtAction(nameof(GetById), new { id = employee.EmployeeID }, employee); ;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            if (id != employee.EmployeeID) return BadRequest();
            await _service.UpdateEmployeeById(id, employee);
            
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _service.GetEmployeeById(id);
            await _service.DeleteEmployeeById(id);

            return Ok(employee);
        }
    }
}