using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Skills.Queries.GetSkills
{
	public class GetSkillsQuery : IRequest<List<Skill>>
	{

	}
	public class GetSkillsQueryHandler : IRequestHandler<GetSkillsQuery, List<Skill>>
	{
		private readonly IApplicationDbContext _context;

		public GetSkillsQueryHandler(IApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<List<Skill>> Handle(GetSkillsQuery request, CancellationToken cancellationToken)
		{
			var skills = await _context.Skills.ToListAsync();
			return skills;
		}
	}
}
