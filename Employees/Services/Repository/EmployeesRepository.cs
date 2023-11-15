using System;
using Employees.Data;
using Employees.Models;
using Employees.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Employees.Services.Repository
{
	public class EmployeesRepository: IEmployeesRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EmployeesRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
      public  async Task<IEnumerable<Employee>> GetAllEmployees() => await _applicationDbContext.Employee.ToListAsync();

       public async Task<Employee> GetEmployeeIdAsync(int id)
        {
            var currentEmployee = await _applicationDbContext.Employee.FirstOrDefaultAsync(x => x.Id == id);
            return currentEmployee ?? throw new Exception("Employee not found");
        }

     public async   Task<Employee> CreateEmployeeAsync(Employee entity)
        {
            await _applicationDbContext.Employee.AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee entity)
        {
            var currentEmployee = await _applicationDbContext.Employee.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (currentEmployee != null) throw new Exception("Employee not found");

            currentEmployee.UserName = entity.UserName;
            currentEmployee.Department = entity.Department;
            currentEmployee.BirthDate = entity.BirthDate?.ToUniversalTime();
            currentEmployee.DateOfEmployment = entity.DateOfEmployment?.ToUniversalTime();
            currentEmployee.Wage = entity.Wage;
            await _applicationDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Employee> DeleteEmployeeAsync(int Id)
        {
            var currentEmployee = await _applicationDbContext.Employee.FirstOrDefaultAsync(x => x.Id == Id);
            if (currentEmployee == null) throw new Exception("Employee not found");
            _applicationDbContext.Employee.Remove(currentEmployee);
            _applicationDbContext.SaveChanges();
            return currentEmployee;
        }
    }
}

