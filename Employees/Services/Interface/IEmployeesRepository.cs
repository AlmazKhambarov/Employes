using System;
using Employees.Models;

namespace Employees.Services.Interface
{
    public interface IEmployeesRepository
    {
        Task<Employee> GetEmployeeIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> CreateEmployeeAsync(Employee entity);
        Task<Employee> UpdateEmployeeAsync(Employee entity);
        Task<Employee> DeleteEmployeeAsync(int Id);
    }
}

