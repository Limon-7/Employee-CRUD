
using Application.Countries.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WEB.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CountriesController : ControllerBase
	{
		private readonly IMediator _matiator;

		public CountriesController(IMediator matiator)
		{
			_matiator = matiator;
		}
		


		[HttpGet]
		public Task<List<CountriesDto>> Get()
		{
			return _matiator.Send(new GetCountriesQuery());
		}

	}
}
