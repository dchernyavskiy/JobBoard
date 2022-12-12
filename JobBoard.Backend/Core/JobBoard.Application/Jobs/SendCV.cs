using JobBoard.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Application.Jobs
{
    public class SendCV
    {
        public class SendCVCommand : IRequest<bool>
        {
            public Guid JobId { get; set; }
            public Guid EmployeeId { get; set; }
        }

        public class SendCVCommandHandler : IRequestHandler<SendCVCommand, bool>
        {
            private readonly IJobBoardDbContext _context;

            public SendCVCommandHandler()
            {
            }

            public async Task<bool> Handle(SendCVCommand request, CancellationToken cancellationToken)
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == request.EmployeeId);
                if (employee == null)
                    throw new Exception();

                var job = await _context.Jobs
                    .Include(x => x.Employer)
                    .FirstOrDefaultAsync(x => x.Id == request.JobId);
                if (job == null)
                    throw new Exception();

                MailMessage mail = new MailMessage();
                mail.To.Add("anastasiaglusenkoo7@gmail.com");
                mail.From = new MailAddress("anastasiaglusenkoo7@gmail.com");
                mail.Subject = "JobBoard";
                mail.Body = $"<p>{employee.FirstName} {employee.LastName} is looking for {job.Name} position</p><br><a href='{employee.CVLink}'>CV Link</a>";
                mail.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Credentials = new NetworkCredential("anastasiaglusenkoo7@gmail.com", "sstnzqgkvirmlook");
                smtpClient.Send(mail);

                //fdsfasdfa

                return true;
            }
        }
    }
}