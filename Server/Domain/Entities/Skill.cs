using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Skill
    {
        public int Id { get; set; }
        public string LanguageSkill { get; set; }

		[JsonIgnore]
		public ICollection<EmployeeSkill> EmployeeSkills { get; set; }

		public Skill()
		{
			EmployeeSkills = new Collection<EmployeeSkill>();
		}
	}
}
