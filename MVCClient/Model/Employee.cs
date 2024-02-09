using System.ComponentModel.DataAnnotations;

namespace MVCClient.Model
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
    }
}
