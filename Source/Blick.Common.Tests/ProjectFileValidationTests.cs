using System;
using System.IO;
using System.Text;
using Xunit;

namespace Blick.Common.Tests;

public class ProjectFileValidationTests
{
    [Fact]
    public void AllProjectFiles_ShouldBeValid()
    {
        var projectFiles = GetProjectFiles();

        var hasErrors = false;
        var errors = new StringBuilder();

        foreach (var projectFile in projectFiles)
        {
            var projectFileName = projectFile.Split("\\")[projectFile.Split("\\").Length - 1];
            
            var content = File.ReadAllText(projectFile)
                .Replace(" ", string.Empty)
                .Replace(Environment.NewLine, string.Empty);

            if (!content.Contains("<PropertyGroupCondition=\"'$(Configuration)'=='Debug'\"><TreatWarningsAsErrors>true</TreatWarningsAsErrors></PropertyGroup>"))
            {
                hasErrors = true;
                errors.AppendLine($" - {projectFileName}: the option 'TreatWarningsAsErrors' should be enabled in configuration 'Debug'");
            }

            if (!content.Contains("<PropertyGroupCondition=\"'$(Configuration)'=='Release'\"><TreatWarningsAsErrors>true</TreatWarningsAsErrors></PropertyGroup>"))
            {
                hasErrors = true;
                errors.AppendLine($" - {projectFileName}: the option 'TreatWarningsAsErrors' should be enabled in configuration 'Release'");
            }

            if (content.Contains("<ImplicitUsings>enable</ImplicitUsings>"))
            {
                hasErrors = true;
                errors.AppendLine($" - {projectFileName}: the option 'ImplicitUsings' should not be enabled");
            }
        }
        
        Assert.False(hasErrors, $"The following errors were found in project files:{Environment.NewLine}{errors}");
    }

    private static string[] GetProjectFiles()
    {
        const string sourceDirectoryName = "Source";

        var baseDirectory = AppContext.BaseDirectory;
        var rootDirectory = baseDirectory.Split(sourceDirectoryName)[0];
        var sourceDirectory = Path.Join(rootDirectory, sourceDirectoryName);

        return Directory.GetFiles(sourceDirectory, "*.csproj", SearchOption.AllDirectories);
    }
}