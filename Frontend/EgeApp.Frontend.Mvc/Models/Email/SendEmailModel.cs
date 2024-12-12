using System;

namespace EgeApp.Frontend.Mvc.Models.Email;

public class SendEmailModel
{
    public string EmailTo { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
