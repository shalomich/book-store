using BookStore.Application.Providers;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Services;
public class EmailService
{
    private MailSettings MailSettings { get; }

    public EmailService(
        IOptions<MailSettings> settingOption)
    {
        MailSettings = settingOption.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message, 
        CancellationToken cancellationToken)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress(MailSettings.SenderName, MailSettings.Email));
        emailMessage.To.Add(new MailboxAddress("", email));
        
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = message
        };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(MailSettings.Host, MailSettings.Port, useSsl: true, cancellationToken);
            await client.AuthenticateAsync(MailSettings.Email, MailSettings.Password, cancellationToken);
            var a = await client.SendAsync(emailMessage, cancellationToken);

            await client.DisconnectAsync(quit: true, cancellationToken);
        }
    }
}
