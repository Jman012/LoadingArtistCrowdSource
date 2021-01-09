using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using Mailjet.Client.TransactionalEmails.Response;
using Newtonsoft.Json.Linq;

namespace LoadingArtistCrowdSource.Server.Services
{
	public class MailJetEmailSender : IEmailSender
	{
		public IConfiguration Configuration { get; }

		public MailJetEmailSender(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public Task SendEmailAsync(string email, string subject, string message)
		{
			return Execute(
				Configuration.GetValue<string>("MailJet:ApiKey"),
				Configuration.GetValue<string>("MailJet:ApiSecret"),
				subject,
				message,
				email,
				Configuration.GetValue<string>("LACS:FromEmailAddress"),
				Configuration.GetValue<string>("MailJet:FromEmailName"));
		}

		public async Task Execute(string apiKey, string apiSecret, string subject, string message, string toEmailAddress, string fromEmailAddress, string fromEmailName)
		{
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
