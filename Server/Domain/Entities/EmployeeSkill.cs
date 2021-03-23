using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EmployeeSkill
    {
		public int EmployeeId { get; set; }
		[JsonIgnore]
		public Employee Employee { get; set; }	
		
		public int SkillId { get; set; }
		[JsonIgnore]
		public Skill Skill { get; set; }
	}
}
