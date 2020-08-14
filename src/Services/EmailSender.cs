using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        //dependency injection
        public SendGridOptions _sendGridOptions { get; }
        public IDotnetdesk _dotnetdesk { get; }
        public SmtpOptions _smtpOptions { get; }
        public SmtpOptions _pop3Options { get; }
        public EmailSender(IOptions<SendGridOptions> sendGridOptions,
                IDotnetdesk dotnetdesk,
                IOptions<SmtpOptions> smtpOptions,
                IOptions<Pop3Ayarlari> pop3Options)
        {
            
            _sendGridOptions = sendGridOptions.Value;
            _dotnetdesk = dotnetdesk;
            _smtpOptions = smtpOptions.Value;
            _pop3Options = smtpOptions.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            //send email using sendgrid via dotnetdesk
          /*  _dotnetdesk.SendEmailBySendGridAsync(_sendGridOptions.SendGridKey, 
                _sendGridOptions.FromEmail, 
                _sendGridOptions.FromFullName, 
                subject, 
                message, 
                email).Wait();*/

            //send email using smtp via dotnetdesk. uncomment to use it
            /*
            _dotnetdesk.SendEmailByGmailAsync(_smtpOptions.fromEmail,
                _smtpOptions.fromFullName,
                subject,
                message,
                email,
                email,
                _smtpOptions.smtpUserName,
                _smtpOptions.smtpPassword,
                _smtpOptions.smtpHost,
                _smtpOptions.smtpPort,
                _smtpOptions.smtpSSL).Wait();
                */

            _dotnetdesk.SendEmailByRetailMarkupAsync(_pop3Options.fromEmail,
                _pop3Options.fromFullName,
                subject,
                message,
                email,
                email,
                _pop3Options.smtpUserName,
                _pop3Options.smtpPassword,
                _pop3Options.smtpHost,
                _pop3Options.smtpPort,
                _pop3Options.smtpSSL).Wait();
            
            return Task.CompletedTask;
        }
    }
}
