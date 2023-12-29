using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;

namespace PWS.Services
{
	public class EmailService : IEmailService
	{
		
		private readonly IConfiguration _config;
		public EmailService(IConfiguration config)
		{
			_config=config;
		}
		public async Task SendEmailAsync(string email, string fname, string message)
		{
			using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_config.GetSection("FromName").Value, _config.GetSection("FromAdress").Value));
            emailMessage.To.Add(new MailboxAddress(fname,email));
			emailMessage.Subject = "Test";
			emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
			{
				Text = message
			};

			using (var client = new SmtpClient())
			{
				await client.ConnectAsync(_config.GetSection("SmtpHost").Value, 587, false);
				await client.AuthenticateAsync(_config.GetSection("SmtpUsername").Value, _config.GetSection("SmtpPassword").Value);
				await client.SendAsync(emailMessage);

				await client.DisconnectAsync(true);
			}
		}
	}
}

