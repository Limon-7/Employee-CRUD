using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Employees.Commands.CreateEmployee
{
	public class CreateEmployeeCommand : IRequest<Employee>
	{
		public string Name { get; set; }
		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
		[DataType(DataType.Date)]
		public DateTime DateOfBirth { get; set; }
		// public IFormFile Resume { get; set; }
		[Required]
		public int CityId { get; set; }

		public ICollection<int> Skills { get; set; }

	}

	public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Employee>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public CreateEmployeeCommandHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
		{
			// string uniquePhoto = ProcessUploadFile(request);
			Console.WriteLine("formdtatatatatta>>>>:", request);
			var employee = new Employee
			{
				Name = request.Name,
				DateOfBirth = request.DateOfBirth,
				// Resume = uniquePhoto,
				CityId = request.CityId
			};
			_context.Employees.Add(employee);
			await _context.SaveChangesAsync(cancellationToken);

			var addToSkill = new List<EmployeeSkill>();
			foreach (var skill in request.Skills)
			{
				addToSkill.Add(new EmployeeSkill { EmployeeId = employee.Id, SkillId = skill });
			}
			_context.EmployeeSkills.AddRange(addToSkill);
			await _context.SaveChangesAsync(cancellationToken);
			return employee;
			throw new NotImplementedException();
		}

		// private string ProcessUploadFile(CreateEmployeeCommand request)
		// {
		//     string uniqueResume = null;

		//     var folderName = Path.Combine("Resources", "Resumes");
		//     var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
		//     Console.WriteLine("pathToSave:" + pathToSave);
		//     Console.WriteLine("folderName:" + folderName);
		//     uniqueResume = Guid.NewGuid().ToString() + "_" + request.Resume.FileName;
		//     request.Resume.CopyTo(new FileStream(pathToSave, FileMode.Create));
		//     return uniqueResume;
		//     //throw new NotImplementedException();
		// }
	}
}
