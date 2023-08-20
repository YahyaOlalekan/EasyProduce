using System.Collections.Generic;
using System.Diagnostics;
using Application.Abstractions;
using System.Threading.Tasks;
using System;
using Application.Dtos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace Persistence;
public class MailService : IMailService
{
     private readonly IConfiguration _configuration;
        public string _mailApikey;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _mailApikey = _configuration.GetSection("MailConfig")["mailApikey"];
        }

        
     public async Task<string> EmailVerificationTemplate(EmailSenderDetails model)
         {
               var key = _configuration.GetSection("MailConfig")["mailApikey"];;
            var senderName = "Easy Produce Farm";
            var senderEmail = "mybluvedcreator@gmail.com";

            Configuration.Default.ApiKey.Clear();
            Configuration.Default.ApiKey.Add("api-key", key);
            var apiInstance = new TransactionalEmailsApi();

            var emailSender = new SendSmtpEmailSender(senderName, senderEmail);

            var emailReciever = new SendSmtpEmailTo(model.ReceiverEmail, model.ReceiverName);

            var emailRecievers = new List<SendSmtpEmailTo>
            {
                emailReciever
            };

        var replyTo = new SendSmtpEmailReplyTo("treehays90@gmail.com", "Do not reply");


        var sendEmail = new SendSmtpEmail
        {
            Sender = emailSender,
            HtmlContent =  model.HtmlContent,
            Subject = $"Registration Status {DateTime.Now}",
            ReplyTo = replyTo,
            To = emailRecievers,
        };

        try
        {
            var result = await apiInstance.SendTransacEmailAsync(sendEmail);

            return "ok";
        }
        catch (System.Exception e)
        {
            return "ok";

        }
    }
  
 }

