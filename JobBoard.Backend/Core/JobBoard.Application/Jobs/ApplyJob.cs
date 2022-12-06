﻿using JobBoard.Application.Common.Exceptions;
using JobBoard.Application.Interfaces;
using JobBoard.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Application.Jobs
{
    public class ApplyJob
    {
        public class ApplyJobCommand : IRequest
        {
            public Guid EmployeeId { get; set; }
            public Guid JobId { get; set; }

        }

        public class ApplyJobCommandHandler : IRequestHandler<ApplyJobCommand>
        {
            private readonly IJobBoardDbContext _context;

            public ApplyJobCommandHandler(IJobBoardDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(ApplyJobCommand request, CancellationToken cancellationToken)
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == request.EmployeeId);

                if (employee == null)
                    throw new NotFoundException(nameof(Employee), request.EmployeeId);

                var job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == request.JobId);
                
                if (job == null)
                    throw new NotFoundException(nameof(Job), request.JobId);

                employee.AppliedJobs.Add(job);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
