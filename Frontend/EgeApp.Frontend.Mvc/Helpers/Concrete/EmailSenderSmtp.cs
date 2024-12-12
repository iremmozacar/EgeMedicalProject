using System;
using System.Net;
using System.Net.Mail;
using EgeApp.Frontend.Mvc.Helpers.Abstract;
using EgeApp.Frontend.Mvc.Models.Email;

namespace EgeApp.Frontend.Mvc.Helpers.Concrete;

public class EmailSenderSmtp : IEmailSenderHelper
{
    private readonly string _host;
    private readonly int _port;
    private readonly bool _enableSsl;
    private readonly string _userName;
    private readonly string _password;

    public EmailSenderSmtp(string host, int port, bool enableSsl, string userName, string password)
    {
        _host = host;
        _port = port;
        _enableSsl = enableSsl;
        _userName = userName;
        _password = password;
    }

 
    public async Task SendEmailAsync(SendEmailModel model)
    {
        var client = new SmtpClient(_host, _port)
        {
            Credentials = new NetworkCredential(_userName, _password),
            EnableSsl = _enableSsl
        };
        await client.SendMailAsync(new MailMessage(_userName, model.EmailTo, model.Subject, model.Body)
        {
            IsBodyHtml = true
        });
    }

    public async Task SendEmailAsync(string emailTo, string subject, string body)
    {
        var client = new SmtpClient(_host, _port)
        {
            Credentials = new NetworkCredential(_userName, _password),
            EnableSsl = _enableSsl
        };
        await client.SendMailAsync(new MailMessage(_userName, emailTo, subject, body)
        {
            IsBodyHtml = true
        });
    }
}
