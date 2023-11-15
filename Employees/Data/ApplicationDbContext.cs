using System;
using Employees.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Employees.Data
{
	public class ApplicationDbContext:DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Employee> Employee { get; set; }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<Employee>()
        //   .HasData(
        //       //new Employee { Id = 1, UserName = "HDD 1TB", BirthDate = 55, Wage = 7409, DateOfEmployment = 23, Department= "Bugs"},
        //       //new Employee { Id = 2, UserName = "HDD SDD 512 GB", BirthDate = 102, Wage = 19099, Department = "full-stack", DateOfEmployment = 2},
        //       //new Employee { Id = 3, UserName = "RAM DDR4 16GB", BirthDate = 47, Wage = 8032, DateOfEmployment = 21, Department = "back-end"}
        //   );
        //}
    }
}

