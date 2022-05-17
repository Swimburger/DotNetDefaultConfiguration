var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

EmailOptions GetEmailOptions(string emailSectionName)
{
    var emailOptions = new EmailOptions();
    
    var emailsSection = app.Configuration.GetSection("Emails");
    emailsSection.GetSection("Defaults").Bind(emailOptions);
    emailsSection.GetSection(emailSectionName).Bind(emailOptions);

    return emailOptions;
}


void LogEmailOptions(EmailOptions emailOptions)
{
    var logger = app.Logger;
    logger.LogInformation(@"Logging Email Option:
    FromEmail: {FromEmail}
    FromName: {FromName}
    ToEmail: {ToEmail}
    ToName: {ToName}
    Subject: {Subject}
    Body: {Body}", 
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