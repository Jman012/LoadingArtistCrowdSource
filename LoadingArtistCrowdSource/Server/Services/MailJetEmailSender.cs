using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using Mailjet.Client.TransactionalEmails.Response;
using Newtonsoft.Json.Linq;

namespace LoadingArtistCrowdSource.Server.Services
{
	public class MailJetEmailSender : IEmailSender
	{
		private readonly IConfiguration _configuration;
		private readonly ILogger _logger;

		public MailJetEmailSender(IConfiguration configuration, ILogger<MailJetEmailSender> logger)
		{
			_configuration = configuration;
			_logger = logger;
		}

		public Task SendEmailAsync(string email, string subject, string message)
		{
			return Execute(
				_configuration.GetValue<string>("MailJet:ApiKey"),
				_configuration.GetValue<string>("MailJet:ApiSecret"),
				subject,
				message,
				email,
				_configuration.GetValue<string>("LACS:FromEmailAddress"),
				_configuration.GetValue<string>("LACS:FromEmailName"));
		}

		public async Task Execute(string apiKey, string apiSecret, string subject, string message, string toEmailAddress, string fromEmailAddress, string fromEmailName)
		{
			_logger.LogWarning($"Sending email from {fromEmailAddress} to {toEmailAddress} with subject line {subject}");
			var client = new MailjetClient(apiKey, apiSecret);
			MailjetRequest request = new MailjetRequest
			{
				Resource = Send.Resource,
			};

			var email = new TransactionalEmailBuilder()
				.WithFrom(new SendContact(fromEmailAddress, fromEmailName))
				.WithSubject(subject)
				.WithHtmlPart(message)
				.WithTo(new SendContact(toEmailAddress))
				.Build();
			var response = await client.SendTransactionalEmailAsync(email);
			var messageResponse = response.Messages.First();
			if (messageResponse.Errors?.Count > 0)
			{
				throw new Exception(string.Join("\n", messageResponse.Errors!.Select(e => e.ToString())));
			}
		}
	}
}
