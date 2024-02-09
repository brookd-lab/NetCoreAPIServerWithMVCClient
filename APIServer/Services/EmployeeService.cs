using APIServer.Data;
using APIServer.Models;
using Microsoft.EntityFrameworkCore;

namespace APIServer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        async Task<IEnumerable<Employee>> IEmployeeService.GetAllEmployees()
        {
            var employeeList = await _context.Employee.ToListAsync();
            return employeeList;
        }
        async Task<Employee> IEmployeeService.GetEmployeeById(int id)
        {
            Employee? employee = await _context.Employee.FindAsync(id);
            return employee;
        }

        async Task IEmployeeService.CreateEmployee(Employee employee)
        {
            await _context.Employee.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        async Task IEmployeeService.UpdateEmployeeById(int id, Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        async Task IEmployeeService.DeleteEmployeeById(int id)
        {
            var employeeToDelete = await _context.Employee.FindAsync(id);
            if (employeeToDelete != null)
            {
                _context.Remove(employeeToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
