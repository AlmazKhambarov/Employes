using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Employees.Services.Interface;
using Employees.Models;

namespace Employes.Controllers
{
	public class EmployesControler : Controller
	{
        private readonly IEmployeesRepository _employeesRepository;

        public EmployesControler(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _employeesRepository.GetAllEmployees();
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null) return NotFound();

            var product = await _employeesRepository.GetEmployeeIdAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        public IActionResult Create() => View();


        [HttpPost]

        public async Task<IActionResult> Create([Bind("Id,Title,Quantity,Price")] Employee employee)
        {
            if (!ModelState.IsValid) return View(employee);
            await _employeesRepository.CreateEmployeeAsync(employee);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Quantity,Price")] Employee employee)
        {
            if (id != employee.Id) return NotFound();

            if (!ModelState.IsValid) return View(employee);

            try
            {
                var newProduct = await _employeesRepository.UpdateEmployeeAsync(employee);
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

