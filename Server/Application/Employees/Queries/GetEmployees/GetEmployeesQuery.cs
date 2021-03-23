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

namespace Application.Employees.Queries.GetEmployees
{
    public class GetEmployeesQuery: IRequest<List<GetEmployeeDto>>
    {
        
    }
	public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<GetEmployeeDto>>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetEmployeesQueryHandler(IApplicationDbContext context,IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<List<GetEmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
		{

			var employees = await _context.Employees
				.Include(c=>c.City).ThenInclude(co=>co.Country)
				.Include(s=>s.EmployeeSkills).ThenInclude(s=>s.Skill)
				.ProjectTo<GetEmployeeDto>(_mapper.ConfigurationProvider).ToListAsync();

			return employees;
			//return await _context.Employees.ToListAsync();
			throw new Exception();
		}
	}
}
