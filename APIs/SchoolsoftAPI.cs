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
    

    //  public readonly string SchoolsExportUrl = "https://sms.schoolsoft.se/thorenframtid/export/schools.jsp?fileFormat=xml&includeAll=1";
    public readonly string TeachersExportUrl = "https://sms.schoolsoft.se/thorenframtid/export/teachers.jsp?fileFormat=xml&includeAll=1";
//    static readonly string StudentsExportUrl = "https://sms.schoolsoft.se/thorenframtid/export/students.jsp?fileFormat=xml&includeAll=1";
    static readonly string StudentsExportUrl = "https://sms.schoolsoft.se/thorenframtid/export/students.jsp?fileFormat=xml";
    static readonly string StudentStatusesExportUrl = "https://sms.schoolsoft.se/thorenframtid/export/studentstatuses.jsp?fileFormat=xml";
    static readonly string TeachersGetfileUrl = "https://sms.schoolsoft.se/thorenframtid/jsp/remote/get_file.jsp?school={0}&fileType=teacher&fileFormat=xml&includeAll=1";
    static readonly string TeachersGetfileUrlBegin = "https://sms.schoolsoft.se/thorenframtid/jsp/remote/get_file.jsp?school=";
    static readonly string TeachersGetfileUrlEnd = "&fileType=teacher&fileFormat=xml&includeAll=1";

    static readonly string StudentStatusesUrlTemplate = "https://sms.schoolsoft.se/{0}/export/studentstatuses.jsp?fileFormat=xml";
    static readonly string SchoolsUrlTemplate = "https://sms.schoolsoft.se/{0}/export/schools.jsp?fileFormat=xml";
    static readonly string StudentsUrlTemplate = "https://sms.schoolsoft.se/{0}/export/students.jsp?fileFormat=xml";
    static readonly string TeachersUrlTemplate = "https://sms.schoolsoft.se/{0}/export/teachers.jsp?fileFormat=xml";

    private static readonly string ok = "Ok";

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





    //    //public async Task<(List<Teacher>?, string)> ReadAllTeachers(List<string> web)
    //    public async Task<(List<Teacher>?, string)> GetTeachers_Getfile(string webapp)
    //    {
    //        var result = await Client.GetStringAsync(string.Format(TeachersGetfileUrl, webapp));
    //        if (string.IsNullOrEmpty(result))
    //            return (null, "Empty result from API");
    //        if (result.StartsWith("API PARTNER ERROR"))
    //            return (null, "API PARTNER ERROR");
    //        string textFilename = Path.Combine(CacheDir, $"schoolsoft_teachers_{webapp}.xml");
    //        File.WriteAllText(textFilename, result);
    //        var serializer = new XmlSerializer(typeof(Teachers));
    //        var reader = new StringReader(result);
    //        if (serializer.Deserialize(reader) is not Teachers teachers)
    //        {
    ////            Log.Error("Kan inte tolka XML från Schoolsoft");
    //            return(null, "Kan inte tolka XML från Schoolsoft");
    //        }
    ////        Log.Information("Antal lärare (inkl inaktiva): {0}", teachers.Teacher.Count);
    //        return (teachers.Teacher, ok);
    //    }

    //    public async Task<(List<StudentExport>?, string)> GetAllStudentsExport()
    //    {
    //        string filenameStudentExport = Path.Combine(CacheDir, "StudentsExport.json");
    //        string filenameRawStudentExport = Path.Combine(CacheDir, "StudentsExport.txt");
    //        //if (UseCache && File.Exists(filenameStudentExport))
    //        //{
    //        //    if (JsonSerializer.Deserialize<List<StudentExport>>(File.ReadAllText(filenameStudentExport)) is not List<StudentExport> schoolsoftStudents)
    //        //        return (null, "Schoolsoft: Kan inte läsa från cache");
    //        //    OnProgress?.Invoke($"Schoolsoft: Read from cache ({schoolsoftStudents.Count})");
    //        //    return (schoolsoftStudents, ok);
    //        //}
    //        var studentsString = await Client.GetStringAsync(StudentsExportUrl);
    //        if (string.IsNullOrEmpty(studentsString))
    //            return (null, "Empty result from API");
    //        if (studentsString.StartsWith("API PARTNER ERROR"))
    //            return (null, "API PARTNER ERROR");
    //        File.WriteAllText(filenameRawStudentExport, studentsString);

    //        var serializer = new XmlSerializer(typeof(StudentsExport));
    //        var allStudents = (StudentsExport?)serializer.Deserialize(new StringReader(studentsString));
    //        if (allStudents is null)
    //        {
    //            return (null, "Schoolsoft: Kan inte läsa från Schoolsoft API");
    //        }
    //        var options = new JsonSerializerOptions { 
    //            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, 
    //            WriteIndented = true };
    //        File.WriteAllText(filenameStudentExport, JsonSerializer.Serialize(allStudents.StudentExport , options));
    //        return (allStudents.StudentExport, ok);
    //    }


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
        Log?.Information("Schoolsoft: Start reading from API");
        var studentsString = await Client.GetStringAsync(StudentStatusesExportUrl);
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



    //public async Task<(List<TeacherExport>?, string)> GetAllTeachersExport()
    //{
    //    string filename = Path.Combine(CacheDir, "TeachersExport.json");
    //    if (UseCache && File.Exists(filename))
    //    {
    //        if (JsonSerializer.Deserialize<List<TeacherExport>>(File.ReadAllText(filename)) is not List<TeacherExport> schoolsoftTeachers)
    //            return (null, "Schoolsoft: Kan inte läsa från cache");
    //        OnProgress?.Invoke($"Schoolsoft: Read from cache ({schoolsoftTeachers.Count})");
    //        return (schoolsoftTeachers, ok);
    //    }
    //    var teachersString = await Client.GetStringAsync(TeachersExportUrl);
    //    if (string.IsNullOrEmpty(teachersString))
    //        return (null, "Empty result from API");
    //    if (teachersString.StartsWith("API PARTNER ERROR"))
    //        return (null, "API PARTNER ERROR");

    //    var serializer = new XmlSerializer(typeof(TeachersExport));
    //    var teachers = (TeachersExport?)serializer.Deserialize(new StringReader(teachersString));
    //    if (teachers is null)
    //    {
    //        return (null, "Schoolsoft: Kan inte läsa från Schoolsoft API");
    //    }
    //    var options = new JsonSerializerOptions { WriteIndented = true };
    //    File.WriteAllText(filename, JsonSerializer.Serialize(teachers.TeacherExport, options));
    //    return (teachers.TeacherExport, ok);
    //}


    //public async Task<List<TeacherExport>> GetTeachersExport()
    //{
    //    string filename = Path.Combine(CacheDir, "TeachersExport.json");
    //    if (UseCache && File.Exists(filename))
    //    {
    //        List<TeacherExport>? all_teachers = JsonSerializer.Deserialize<List<TeacherExport>>(File.ReadAllText(filename));
    //        if (all_teachers is not null)
    //            return all_teachers;
    //    }
    //    var teachers_as_string = await ReadSchoolsoftAPI(TeachersExportUrl);
    //    //File.WriteAllText("temp.xml", students_as_string);
    //    var serializer = new XmlSerializer(typeof(TeachersExport));
    //    var teachers = (TeachersExport?)serializer.Deserialize(new StringReader(teachers_as_string));
    //    if (teachers is null)
    //    {
    //        return new List<TeacherExport>();
    //    }
    //    File.WriteAllText(filename, JsonSerializer.Serialize(teachers.TeacherExport));
    //    return teachers.TeacherExport;
    //}

    //public async Task<List<TeacherGetfile>> GetTeachersGetfile(string webapp, int orgid)
    //{
    //    string filename = Path.Combine(CacheDir, $"schoolsoft_teachers_{webapp}.json");
    //    if (UseCache && File.Exists(filename))
    //    {
    //        List<TeacherGetfile>? all_teachers = JsonSerializer.Deserialize<List<TeacherGetfile>>(File.ReadAllText(filename));
    //        if (all_teachers is not null)
    //        {
    //            OnProgress?.Invoke($"Schoolsoft {webapp} read teachers cache");
    //            return all_teachers;
    //        }
    //    }
    //    //var skola = "vaxjo";

    //    var teachers_as_string = await ReadSchoolsoftAPI(TeachersGetfileUrlBegin + webapp + TeachersGetfileUrlEnd);
    //    var serializer = new XmlSerializer(typeof(TeachersGetfile));
    //    var teachers = (TeachersGetfile?)serializer.Deserialize(new StringReader(teachers_as_string));
    //    if (teachers is null)
    //    {
    //        return new List<TeacherGetfile>();
    //    }
    //    List<TeacherGetfile> TeachersOrgidFixed = new();
    //    foreach (var teacher in teachers.TeacherGetfile)
    //    {
    //        var NewTeacher = teacher.Clone();
    //        NewTeacher.orgid = orgid;
    //        TeachersOrgidFixed.Add( NewTeacher );
    //    }
    //    File.WriteAllText(filename, JsonSerializer.Serialize(TeachersOrgidFixed));
    //    OnProgress?.Invoke($"Schoolsoft {webapp} read teachers API");
    //    return TeachersOrgidFixed;
    //}


}
