using Application.Common.Mapping;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employees.Queries
{
    public class GetEmployeeDto:IMapFrom<Employee>
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Resume { get; set; }
		public KeyValuePairDto City { get; set; }
		public KeyValuePairDto Country { get; set; }

		public ICollection<KeyValuePairDto> Skills { get; set; }

		public GetEmployeeDto()
		{
			Skills = new Collection<KeyValuePairDto>();
		}

		public void Mapping(Profile profile)
		{
			profile.CreateMap<City, KeyValuePairDto>();
			profile.CreateMap<Country, KeyValuePairDto>();
			profile.CreateMap<Employee, GetEmployeeDto>()
				.ForMember(c => c.Country, opt => opt.MapFrom(c => c.City.Country))
				.ForMember(s => s.Skills, opt => opt.MapFrom(s => s.EmployeeSkills.Select(s => new KeyValuePairDto { Id = s.SkillId, Name=s.Skill.LanguageSkill})));
		}
	}
}
