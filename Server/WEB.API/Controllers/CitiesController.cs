using Application.Cities.CreateCity;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CitiesController : ControllerBase
	{
		private readonly IMediator _meditor;

		public CitiesController(IMediator meditor)
		{
			_meditor = meditor;
		}
		 [HttpPost]
		public async Task<City> Post([FromBody] CreateCityCommand cmd)
		{
			return await _meditor.Send(cmd);
		}
	}
}
