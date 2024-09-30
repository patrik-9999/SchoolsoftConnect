using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using Common;
using Serilog;

namespace APIs;

public class SchoolsoftAPI
{
    public string CacheDir { get; }
    public bool UseCache { get; set; } = true;
    private JsonSerializerOptions JsonOptions { get; }
    private string SchoolsoftPassword { get; }
    private readonly HttpClient Client = new();
    private readonly ILogger? Log;
    private string WebApp { get; }
    private string StudentStatusesUrl { get; }
    private string SchoolsUrl { get; }
    private string StudentsUrl { get; }
    private string TeachersUrl { get; }
    

    //public readonly string TeachersExportUrl = "https://sms.schoolsoft.se/thorenframtid/export/teachers.jsp?fileFormat=xml&includeAll=1";
    //static readonly string StudentsExportUrl = "https://sms.schoolsoft.se/thorenframtid/export/students.jsp?fileFormat=xml";
    //static readonly string StudentStatusesExportUrl = "https://sms.schoolsoft.se/thorenframtid/export/studentstatuses.jsp?fileFormat=xml";
    //static readonly string TeachersGetfileUrl = "https://sms.schoolsoft.se/thorenframtid/jsp/remote/get_file.jsp?school={0}&fileType=teacher&fileFormat=xml&includeAll=1";
    //static readonly string TeachersGetfileUrlBegin = "https://sms.schoolsoft.se/thorenframtid/jsp/remote/get_file.jsp?school=";
    //static readonly string TeachersGetfileUrlEnd = "&fileType=teacher&fileFormat=xml&includeAll=1";

    static readonly string StudentStatusesUrlTemplate = "https://sms.schoolsoft.se/{0}/export/studentstatuses.jsp?fileFormat=xml";
    static readonly string SchoolsUrlTemplate = "https://sms.schoolsoft.se/{0}/export/schools.jsp?fileFormat=xml";
    static readonly string StudentsUrlTemplate = "https://sms.schoolsoft.se/{0}/export/students.jsp?fileFormat=xml";
    static readonly string TeachersUrlTemplate = "https://sms.schoolsoft.se/{0}/export/teachers.jsp?fileFormat=xml";

    //private static readonly string ok = "Ok";

    //public SchoolsoftAPI(string fileNameSchoolsoftPassword, ILogger? log = null, Action<string>? OnProgressListener = null, bool useCache = true, string cacheDir = "")
    public SchoolsoftAPI(string fileNameSchoolsoftPassword, string webApp, ILogger? log=null, bool useCache = true, string cacheDir="")
    {
        WebApp = webApp;
        Log = log;
        UseCache = useCache;
        StudentStatusesUrl = string.Format(StudentStatusesUrlTemplate, webApp);
        SchoolsUrl = string.Format(SchoolsUrlTemplate, webApp);
        StudentsUrl = string.Format(StudentsUrlTemplate, webApp);
        TeachersUrl = string.Format(TeachersUrlTemplate, webApp);
        if (string.IsNullOrEmpty(cacheDir))
            CacheDir = Path.Combine(Directory.GetCurrentDirectory(), "cache");
        else
            CacheDir = cacheDir;
        Directory.CreateDirectory(CacheDir);
        using StreamReader reader = new StreamReader(fileNameSchoolsoftPassword, Encoding.UTF8);
        SchoolsoftPassword = reader.ReadToEnd();
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Add("X-REMOTEPWD", SchoolsoftPassword);
        JsonOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        //Log?.Information("SchoolsoftAPI: Created");
    }
    public override string ToString()
    {
        return $"\nSchoolsoftAPI:\n" +
               $"    UseCache: {UseCache}\n" +
               $"    ILogger: {Log}\n" +
               $"    CacheDir: {CacheDir}\n" +
               $"    StudentStatusesUrl: {StudentStatusesUrl}\n" +
               $"    SchoolsUrl: {SchoolsUrl}\n";
    }

    public async Task<List<School>?> GetSchools()
    {
        string cacheJson = Path.Combine(CacheDir, "Schools.json");
        string filenameRaw = Path.Combine(CacheDir, "Schools.xml");
        if (UseCache && File.Exists(cacheJson))
        {
            if (JsonSerializer.Deserialize<List<School>>(File.ReadAllText(cacheJson)) is List<School> schoolsCached)
            {
                Log?.Information("Schoolsoft: Schools read from cache");
                return schoolsCached;
            }
            Log?.Information("Schoolsoft: Cannot read Schools from cache, trying API");
        }
        Log?.Information("Schoolsoft: Start reading Schools from API");
        var schoolsString = await Client.GetStringAsync(SchoolsUrl);
        if (string.IsNullOrEmpty(schoolsString))
        {
            Log?.Error("Empty result from Schoolsoft API");
            return null;
        }
        if (schoolsString.StartsWith("API PARTNER ERROR"))
        {
            Log?.Error("Schoolsoft: API PARTNER ERROR");
            return null;
        }
        File.WriteAllText(filenameRaw, schoolsString);

        var serializer = new XmlSerializer(typeof(Schools));
        var schools = (Schools?)serializer.Deserialize(new StringReader(schoolsString));
        if (schools is null)
        {
            Log?.Error("Schoolsoft: Cannot deserialize schoolsoft schools data");
            return null;
        }
        File.WriteAllText(cacheJson, JsonSerializer.Serialize(schools.SchoolExport, JsonOptions));
        return schools.SchoolExport;
    }


    public async Task<List<StudentStatus>?> GetStudentStatuses()
    {
        string cacheJson = Path.Combine(CacheDir, "StudentStatuses.json");
        string filenameRaw = Path.Combine(CacheDir, "StudentStatuses.xml");
        if (UseCache && File.Exists(cacheJson))
        {
            if (JsonSerializer.Deserialize<List<StudentStatus>>(File.ReadAllText(cacheJson)) is List<StudentStatus> studentStatuses)
            {
                Log?.Information("Schoolsoft: Read from cache");
                return studentStatuses;
            }
            Log?.Information("Schoolsoft: Cannot read from cache, trying API");
        }
        Log?.Information("Schoolsoft: Start reading from API StudentStatuses");
        var studentsString = await Client.GetStringAsync(StudentStatusesUrl);
        if (string.IsNullOrEmpty(studentsString))
        {
            Log?.Error("Empty result from Schoolsoft API");
            return null;
        }
        if (studentsString.StartsWith("API PARTNER ERROR"))
        {
            Log?.Error("Schoolsoft: API PARTNER ERROR");
            return null;
        }
        File.WriteAllText(filenameRaw, studentsString);

        var serializer = new XmlSerializer(typeof(StudentStatuses));
        var students = (StudentStatuses?)serializer.Deserialize(new StringReader(studentsString));
        if (students is null)
        {
            Log?.Error("Schoolsoft: Cannot deserialize schoolsoft data");
            return null;
        }
        File.WriteAllText(cacheJson, JsonSerializer.Serialize(students.StudentStatus, JsonOptions));
        Log?.Information("Schoolsoft: Read {0} StudentStatuses", students.StudentStatus?.Count);
        return students.StudentStatus;
    }


    public async Task<List<Student>?> GetStudents()
    {
        string cacheJson = Path.Combine(CacheDir, "Students.json");
        string filenameRaw = Path.Combine(CacheDir, "Students.xml");
        if (UseCache && File.Exists(cacheJson))
        {
            if (JsonSerializer.Deserialize<List<Student>>(File.ReadAllText(cacheJson)) is List<Student> studentsList)
            {
                Log?.Information("Schoolsoft: Read from cache");
                return studentsList;
            }
            Log?.Information("Schoolsoft: Cannot read from cache, trying API");
        }
        Log?.Information("Schoolsoft: Start reading from API students");
        var studentsString = await Client.GetStringAsync(StudentsUrl);
        if (string.IsNullOrEmpty(studentsString))
        {
            Log?.Error("Empty result from Schoolsoft API");
            return null;
        }
        if (studentsString.StartsWith("API PARTNER ERROR"))
        {
            Log?.Error("Schoolsoft: API PARTNER ERROR");
            return null;
        }
        File.WriteAllText(filenameRaw, studentsString);

        var serializer = new XmlSerializer(typeof(Students));
        var students = (Students?)serializer.Deserialize(new StringReader(studentsString));
        if (students is null)
        {
            Log?.Error("Schoolsoft: Cannot deserialize schoolsoft data");
            return null;
        }
        File.WriteAllText(cacheJson, JsonSerializer.Serialize(students.Student, JsonOptions));
        return students.Student;
    }

    public async Task<List<Teacher>?> GetTeachers()
    {
        string cacheJson = Path.Combine(CacheDir, "Teachers.json");
        string filenameRaw = Path.Combine(CacheDir, "Teachers.xml");
        if (UseCache && File.Exists(cacheJson))
        {
            if (JsonSerializer.Deserialize<List<Teacher>>(File.ReadAllText(cacheJson)) is List<Teacher> teachersList)
            {
                Log?.Information("Schoolsoft: Read from cache");
                return teachersList;
            }
            Log?.Information("Schoolsoft: Cannot read from cache, trying API");
        }
        Log?.Information("Schoolsoft: Start reading from API teachers");
        var teachersString = await Client.GetStringAsync(TeachersUrl);
        if (string.IsNullOrEmpty(teachersString))
        {
            Log?.Error("Empty result from Schoolsoft API");
            return null;
        }
        if (teachersString.StartsWith("API PARTNER ERROR"))
        {
            Log?.Error("Schoolsoft: API PARTNER ERROR");
            return null;
        }
        File.WriteAllText(filenameRaw, teachersString);

        var serializer = new XmlSerializer(typeof(Teachers));
        var teachers = (Teachers?)serializer.Deserialize(new StringReader(teachersString));
        if (teachers is null)
        {
            Log?.Error("Schoolsoft: Cannot deserialize schoolsoft data");
            return null;
        }
        File.WriteAllText(cacheJson, JsonSerializer.Serialize(teachers.Teacher, JsonOptions));
        return teachers.Teacher;
    }

}
