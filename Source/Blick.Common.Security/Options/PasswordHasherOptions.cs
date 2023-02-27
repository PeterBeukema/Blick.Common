namespace Blick.Common.Security.Options;

public class PasswordHasherOptions
{
    public const string ConfigurationSectionName = nameof(PasswordHasherOptions); 
    
    public string Key { get; set; } = string.Empty;
    
    public string InitializationVector { get; set; } = string.Empty;
}