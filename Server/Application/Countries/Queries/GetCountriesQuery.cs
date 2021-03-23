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

namespace Application.Countries.Queries
{
    public class GetCountriesQuery:IRequest<List<CountriesDto>>
    {
        
    }
	public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, List<CountriesDto>>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetCountriesQueryHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<List<CountriesDto>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
		{
			var countries = await _context.Countries.Include(c=>c.Cities).ProjectTo<CountriesDto>(_mapper.ConfigurationProvider).ToListAsync();
			return countries;
		}
	}
}
