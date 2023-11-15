using System;
using Employees.FluentValidation;
using Employees.Models;
using Employees.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employees.Controllers
{
    public class EmploysController : Controller
    {
        private readonly IEmployeesRepository _employeesRepository;

        public EmploysController(IEmployeesRepository employeesRepository)
        {
            
            _employeesRepository = employeesRepository;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _employeesRepository.GetAllEmployees();
            return View(products);
        }


        public IActionResult Create() => View();


        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            var validationResult = await new EmploysModelValidation().ValidateAsync(employee);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(employee);
            }
            if (!ModelState.IsValid) return View(employee);
            Employee employee1 = new Employee
            {
                UserName = employee.UserName,
                Department = employee.Department,
                BirthDate = employee.BirthDate?.ToUniversalTime(),
                DateOfEmployment = employee.DateOfEmployment?.ToUniversalTime(),
                Wage = employee.Wage
            };
            await _employeesRepository.CreateEmployeeAsync(employee1);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null) return NotFound();
            var product = await _employeesRepository.GetEmployeeIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit( Employee employee)
        {
            //if (id != employee.Id) return NotFound();

            if (!ModelState.IsValid) return View(employee);

            try
            {
                Employee employee1 = new Employee
                {
                    UserName = employee.UserName,
                    Department = employee.Department,
                    BirthDate = employee.BirthDate?.ToUniversalTime(),
                    DateOfEmployment = employee.DateOfEmployment?.ToUniversalTime(),
                    Wage = employee.Wage
                };
                var newProduct = await _employeesRepository.UpdateEmployeeAsync(employee1);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_employeesRepository.GetEmployeeIdAsync(employee.Id) == null)
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return NotFound();

            var product = await _employeesRepository.GetEmployeeIdAsync(id);

            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _employeesRepository.DeleteEmployeeAsync(id);
            if (product == null) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}

