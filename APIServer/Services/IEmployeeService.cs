using APIServer.Models;

namespace APIServer.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task CreateEmployee(Employee employee);
        Task UpdateEmployeeById(int id, Employee employee);
        Task DeleteEmployeeById(int id);
    }
}
