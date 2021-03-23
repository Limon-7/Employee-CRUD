
using Application.Employees.Commands.CreateEmployee;
using Application.Employees.Commands.DeleteEmployee;
using Application.Employees.Commands.UpdateEmployee;
using Application.Employees.Queries;
using Application.Employees.Queries.GetEmployeeById;
using Application.Employees.Queries.GetEmployees;
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
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<List<GetEmployeeDto>> Get()
        {
            return await _mediator.Send(new GetEmployeesQuery());
        }

        [HttpGet("{id}")]
        public async Task<GetEmployeeDto> Get(int id)
        {

            return await _mediator.Send(new GetEmployeeByIdQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Post([FromBody] CreateEmployeeCommand cmd)
        {
            return await _mediator.Send(cmd);
        }

        [HttpPut("{id}")]
        public async Task<object> Put([FromRoute] int id, UpdateEmployeeCommand cmd)
        {
            cmd.Id = id;
            return await _mediator.Send(cmd);
        }

        [HttpDelete("{id}")]
        public async Task<Unit> Delete([FromRoute] int id, DeleteEmployeeCommand cmd)
        {
            cmd.Id = id;
            return await _mediator.Send(cmd);
        }
    }
}
