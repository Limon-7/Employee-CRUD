using Application.Common.Exceptions;
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

namespace Application.Employees.Commands.DeleteEmployee
{
	public class DeleteEmployeeCommand : IRequest
	{
		public int Id { get; set; }
	}
	public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
	{
		private readonly IApplicationDbContext _context;

		public DeleteEmployeeCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}
		public  async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
		{
			var employee = await _context.Employees.FirstOrDefaultAsync(i => i.Id == request.Id);
			if (employee == null)
			{
				throw new NotFoundException(nameof(Employee), request.Id);
			}
			_context.Employees.Remove(employee);
			var result=await _context.SaveChangesAsync(cancellationToken);
			if (result > 0) return Unit.Value;

			throw new DeleteFailureException(nameof(Employee), request.Id, "There are was server error.");
		}
	}
}
