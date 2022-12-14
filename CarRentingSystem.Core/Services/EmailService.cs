using CarRentingSystem.Common;
using CarRentingSystem.Configuration;
using CarRentingSystem.Core.Constants;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CarRentingSystem.Core.Services
{
    /// <summary>
    /// EmailService represent all services related to emails.
    /// </summary>
    public class EmailService : IEmailService
    {
        /// <summary>
        /// Private properties of the class
        /// </summary>
        private readonly IServiceProvider serviceProvider;
        private readonly IRepository repository;

        /// <summary>
        /// Constructor of the class EmailService
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="repository"></param>
        public EmailService(IServiceProvider serviceProvider, IRepository repository)
        {
            this.serviceProvider = serviceProvider;
            this.repository = repository;
        }

        /// <summary>
        /// Method return string represent email body.
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<string> GetEmailBody(int carId, string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(EmailConstants.ParametersAreNullOrEmptyError);
            }
            StringBuilder sb = new StringBuilder();

            var lastAddedCar = await this.repository.GetByIdAsync<Car>(carId);

            var agent = await this.repository.AllReadonly<Agent>()
                .Include(a => a.User)
                .Where(a => a.UserId == userId)
                .FirstOrDefaultAsync();


            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"en\">");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta charset=\"UTF-8\">");
            sb.AppendLine("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">");
            sb.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            sb.AppendLine("<link rel=\"stylesheet\" href=\"https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/css/bootstrap.min.css\" integrity=\"sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T\" crossorigin=\"anonymous\">");
            sb.AppendLine($"<title>{EmailConstants.EmailSubject}</title>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<div class=\"card\" style=\"width: 18rem;\">");
            sb.AppendLine($"<img src=\"{lastAddedCar.ImageUrl}\" style=\"height:300px; width: auto;\" class=\"card-img-top\" alt=\"...\">");
            sb.AppendLine("<div class=\"card-body\">");
            sb.AppendLine($"<h2 class=\"card-title\">{lastAddedCar.Title}</h2>");
            sb.AppendLine($"<h4 class=\"card-text\">{lastAddedCar.Description}</h4>");
            sb.AppendLine($"<h5 class=\"card-text\">Agent: {agent.User.FirstName} {agent.User.LastName}</h5>");
            sb.AppendLine($"<h5 class=\"card-text\">PhoneNumber: {agent.PhoneNumber}</h5>");
            sb.AppendLine($"<a href=\"https://localhost:7290/\" class=\"btn btn-primary\">Go DasWeltAuto Renting System</a>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("<script src=\"https://code.jquery.com/jquery-3.3.1.slim.min.js\" integrity=\"sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo\" crossorigin=\"anonymous\"></script>");
            sb.AppendLine("<script src=\"https://cdn.jsdelivr.net/npm/popper.js@1.14.7/dist/umd/popper.min.js\" integrity=\"sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1\" crossorigin=\"anonymous\"></script>");
            sb.AppendLine("<script src=\"https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/js/bootstrap.min.js\" integrity=\"sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM\" crossorigin=\"anonymous\"></script>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString().TrimEnd();

        }

        /// <summary>
        /// Method send email for current Car and email has been sended by user ID.
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task Send(int carId, string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(EmailConstants.ParametersAreNullOrEmptyError);
            }
            using (IServiceScope scope = this.serviceProvider.CreateScope())
            {
                var emailConfiguration = scope.ServiceProvider.GetService<IOptions<EmailConfiguration>>().Value;

                var Tos = await this.repository.AllReadonly<User>()
                    .Select(x => x.Email)
                    .ToListAsync();

                var fromAddress = new MailAddress(emailConfiguration.FromAddress, EmailConstants.FromDisplayName);
                var toAddress = new MailAddress(emailConfiguration.FromAddress, EmailConstants.ToDisplayName);

                MailMessage mailMessage = new MailMessage(fromAddress, toAddress);

                mailMessage.IsBodyHtml = true;
                mailMessage.Body = await GetEmailBody(carId, userId);
                mailMessage.Subject = EmailConstants.EmailSubject;

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = emailConfiguration.SmtpServer;
                smtpClient.Port = emailConfiguration.Port;
                smtpClient.EnableSsl = emailConfiguration.EnableSsl;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new NetworkCredential(fromAddress.Address, EmailConstants.FromPassword);
                smtpClient.Timeout = 20000;

                await smtpClient.SendMailAsync(mailMessage);

            }

        }
    }
}
