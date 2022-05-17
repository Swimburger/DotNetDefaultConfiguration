using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var emailsSection = app.Configuration
    .GetSection("Emails");


var welcomeEmail = new EmailOptions();
emailsSection.GetSection("Defaults").Bind(welcomeEmail);
Console.WriteLine(JsonSerializer.Serialize(welcomeEmail));

emailsSection.GetSection("Welcome").Bind(welcomeEmail);
Console.WriteLine(JsonSerializer.Serialize(welcomeEmail));

welcomeEmail = app.Configuration.GetSection("Emails")
    .GetWithDefault<EmailOptions>("Defaults", "Welcome");
Console.WriteLine(JsonSerializer.Serialize(welcomeEmail));

public class EmailOptions
{
    public string FromEmail { get; set; }
    public string FromName { get; set; }
    public string ToEmail { get; set; }
    public string ToName { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}

public static class ConfigurationExtensions
{
    public static T GetWithDefault<T>(this IConfiguration configuration, string defaultSectionKey, string sectionKey)
    {
        var resultOptions = configuration.GetSection(defaultSectionKey).Get<T>();
        configuration.GetSection(sectionKey).Bind(resultOptions);
        return resultOptions;
    }
}