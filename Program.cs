using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
    
EmailOptions GetEmailOptions(string emailSectionName)
{
    var emailOptions = new EmailOptions();
    
    var emailsSection = config.GetSection("Emails");
    var emailSection = emailsSection.GetSection(emailSectionName);

    emailOptions.FromEmail = emailSection["FromEmail"];
    emailOptions.FromName = emailSection["FromName"];
    emailOptions.ToEmail = emailSection["ToEmail"];
    emailOptions.ToName = emailSection["ToName"];
    emailOptions.Subject = emailSection["Subject"];
    emailOptions.Body = emailSection["Body"];

    return emailOptions;
}

void LogEmailOptions(EmailOptions emailOptions)
{
    Console.WriteLine(@"Logging Email Option:
    FromEmail: {0}
    FromName: {1}
    ToEmail: {2}
    ToName: {3}
    Subject: {4}
    Body: {5}", 
        emailOptions.FromEmail, 
        emailOptions.FromName, 
        emailOptions.ToEmail, 
        emailOptions.ToName, 
        emailOptions.Subject, 
        emailOptions.Body
    );
}

var welcomeEmail = GetEmailOptions("Welcome");
LogEmailOptions(welcomeEmail);

var passwordResetEmail = GetEmailOptions("PasswordReset");
LogEmailOptions(passwordResetEmail);

var offerEmail = GetEmailOptions("Offer");
LogEmailOptions(offerEmail);

public class EmailOptions
{
    public string FromEmail { get; set; }
    public string FromName { get; set; }
    public string ToEmail { get; set; }
    public string ToName { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}