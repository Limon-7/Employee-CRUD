using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employee
    {
		public int Id { get; set; }
		public string Name { get; set; }

		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
		[DataType(DataType.Date)]
		[Required]
		public DateTime DateOfBirth { get; set; }

		public string Resume { get; set; }

		public int CityId { get; set; }
		[JsonIgnore]
		public City City { get; set; }

		[JsonIgnore]
		public ICollection<EmployeeSkill> EmployeeSkills { get; set; }

		public Employee()
		{
			EmployeeSkills = new Collection<EmployeeSkill>();
		}
	}
}
