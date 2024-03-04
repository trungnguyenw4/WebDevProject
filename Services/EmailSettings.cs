using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;

public class EmailSettings{
public string SmtpServer { get; set; }
public int SmtpPort { get; set; }
public string SmtpUsername { get; set; }
public string SmtpPassword { get; set; }
}