using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cities.CreateCity
{
    public class CreateCityCommand:IRequest<City>
    {
		public string Name { get; set; }
		public int CountryId { get; set; }
	}
	public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, City>
	{
		private readonly IApplicationDbContext _context;

		public CreateCityCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<City> Handle(CreateCityCommand request, CancellationToken cancellationToken)
		{
			var city = new City
			{
				Name = request.Name,
				CountryId = request.CountryId
			};
			_context.Cities.Add(city);
			var result=await _context.SaveChangesAsync(cancellationToken);
			if (result > 0)
				return city;

			throw new BadRequestException("File not save");
		}
	}

}
