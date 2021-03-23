using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQuery:IRequest<GetEmployeeDto>
    {
		public int Id { get; set; }
	}
	public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, GetEmployeeDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetEmployeeByIdQueryHandler(IApplicationDbContext context,IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<GetEmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
		{
			var employee = await _context.Employees
				.Include(c => c.City).ThenInclude(co => co.Country)
				.Include(s => s.EmployeeSkills).ThenInclude(s => s.Skill)
				.ProjectTo<GetEmployeeDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(i=>i.Id==request.Id);
			if (employee == null)
			{
				throw new NotFoundException(nameof(Employee), request.Id);
			}
			return employee;
		}
	}
}
