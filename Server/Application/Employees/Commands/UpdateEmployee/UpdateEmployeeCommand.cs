using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand:IRequest<Employee>
    {
		public int Id { get; set; }
		public string Name { get; set; }
		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
		[DataType(DataType.Date)]
		public DateTime DateOfBirth { get; set; }
		//public string Resume { get; set; }
		[Required]
		public int CityId { get; set; }

		public ICollection<int> Skills { get; set; }
	}
	public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand,Employee>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public UpdateEmployeeCommandHandler(IApplicationDbContext context,IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<Employee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
		{
			var employee = await _context.Employees.FirstOrDefaultAsync(em => em.Id == request.Id);
			var skillToAdd = new List<EmployeeSkill>();
			if (employee == null)
			{
				throw new NotFoundException(nameof(Employee), request.Id);
			}
			if (employee!=null)
			{
				employee.Name = request.Name?? employee.Name;
				//employee.Resume = request.Resume;
				employee.CityId = request.CityId;
				foreach (var skill in request.Skills)
				{
					Console.WriteLine("item:>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>"+skill);
					var exitingCourse = await _context.EmployeeSkills.FirstOrDefaultAsync(i => i.EmployeeId == request.Id && i.SkillId == skill);
					if (exitingCourse== null)
					{
					   skillToAdd.Add(new EmployeeSkill { EmployeeId = employee.Id, SkillId = skill });
					}
				}
			}
			_context.EmployeeSkills.AddRange(skillToAdd);
			var success = await _context.SaveChangesAsync(cancellationToken);
			if (success > -1)
				return employee;

			throw new NotImplementedException();
		}
	}
}
