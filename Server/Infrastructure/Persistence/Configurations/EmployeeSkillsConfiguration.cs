using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
	public class EmployeeSkillsConfiguration : IEntityTypeConfiguration<EmployeeSkill>
	{
		public void Configure(EntityTypeBuilder<EmployeeSkill> builder)
		{
			builder.HasKey(s => new {s.EmployeeId,s.SkillId });
			builder.HasOne<Employee>(s => s.Employee)
				.WithMany(s => s.EmployeeSkills)
				.HasForeignKey(em => em.EmployeeId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne<Skill>(s => s.Skill)
				.WithMany(s => s.EmployeeSkills)
				.HasForeignKey(s => s.SkillId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
