using Application.Skills.Queries.GetSkills;
using Domain.Entities;
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
	public class SkillsController : ControllerBase
	{
        private readonly IMediator _matiator;

        public SkillsController(IMediator matiator)
        {
            _matiator = matiator;
        }



        [HttpGet]
        public async Task<List<Skill>> Get()
        {
            return await _matiator.Send(new GetSkillsQuery());
        }
    }
}
