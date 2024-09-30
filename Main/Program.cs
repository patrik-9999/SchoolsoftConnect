using Serilog;
using Microsoft.Extensions.Configuration;

using APIs;
using Common;
using System.Reflection;
using System.Text;
using System.Globalization;


namespace Main;

public class Program
{
    static async Task Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        var settings = config.GetRequiredSection("Settings").Get<Settings>() ?? throw new Exception("settings is null");
        var outputDirectory = Path.GetFullPath(settings.OutputDirectory);

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .CreateLogger();

        Log.Debug("OutputDirectory: {0}", outputDirectory);
        var schoolsoft = new SchoolsoftAPI(
            fileNameSchoolsoftPassword: Path.Combine(settings.CredentialsDir, "schoolsoft_api.txt"),
            webApp: settings.WebApp,
            log: Log.Logger,
            useCache: false,
            cacheDir: Path.Combine(outputDirectory, "cache"));
        if (schoolsoft is null)
        {
            Log.Fatal("Kan inte starta SchoolsoftAPI");
            return;
        }

        Log.Information(schoolsoft.ToString());

        var studentStatuses = await schoolsoft.GetStudentStatuses();
        if (studentStatuses == null)
        {
            Log.Fatal("Cannot read studentStatuses from Schoolsoft");
            return;
        }
        foreach (var studentStatus in studentStatuses.Take(10))
        {
            Log.Information("StudentStatus: {0}", studentStatus.Name);
        }
        var students = await schoolsoft.GetStudents();
        if (students == null)
        {
            Log.Fatal("Cannot read students from Schoolsoft");
            return;
        }
        foreach (var student in students.Take(10))
        {
            Log.Information("Student: {0} {1}, {2}", student.Fname, student.Lname, student.Schoolname );
        }

        var teachers = await schoolsoft.GetTeachers();
        if (teachers == null)
        {
            Log.Fatal("Cannot read teachers from Schoolsoft");
            return;
        }
        foreach (var teacher in teachers.Take(10))
        {
            Log.Information("Teacher: {0} {1}, {2}", teacher.Fname, teacher.Lname, teacher.Orgid);
        }
    }
}