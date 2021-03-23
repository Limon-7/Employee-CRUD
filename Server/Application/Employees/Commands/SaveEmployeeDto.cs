using Application.Common.Mapping;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employees.Commands
{
    public class SaveEmployeeDto:IMapFrom<Employee>
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Resume { get; set; }
		public int CityId { get; set; }

		public ICollection<int> Skills { get; set; }

		public SaveEmployeeDto()
		{
			Skills = new Collection<int>();
		}

		public void Mapping(Profile profile)
		{
			profile.CreateMap<SaveEmployeeDto,Employee>()
				.ForMember(e => e.Id, opt => opt.Ignore())
				.ForMember(s=>s.EmployeeSkills,opt=>opt.Ignore())
				.AfterMap((vr, v) => {
					// Remove unselected features
					var removedSkills = v.EmployeeSkills.Where(f => !vr.Skills.Contains(f.SkillId)).ToList();
					foreach (var f in removedSkills)
						v.EmployeeSkills.Remove(f);

					// Add new features
					var addedSkills = vr.Skills.Where(id => !v.EmployeeSkills.Any(f => f.SkillId == id)).Select(id => new EmployeeSkill { SkillId = id }).ToList();
					foreach (var f in addedSkills)
						v.EmployeeSkills.Add(f);
				});

		}
	}
}
