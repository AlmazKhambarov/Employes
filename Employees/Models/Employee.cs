﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Employees.Models
{
	public class Employee
	{
		public int Id { get; set; }

		public string? UserName { get; set; }

        public string? Department { get; set; }

		public DateTime? BirthDate { get; set; }

		public DateTime? DateOfEmployment { get; set; }

		public double? Wage { get; set; }
	}
}

