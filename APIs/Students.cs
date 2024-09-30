using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace APIs;


[XmlRoot(ElementName = "student")]
public class Student
{
    [XmlElement(ElementName = "id")]
    public string? Id { get; set; }
    [XmlElement(ElementName = "personguid")]
    public string? Personguid { get; set; }
    [XmlElement(ElementName = "studentorganizationguid")]
    public string? Studentorganizationguid { get; set; }
    [XmlElement(ElementName = "orgid")]
    public string? Orgid { get; set; }
    [XmlElement(ElementName = "schoolname")]
    public string? Schoolname { get; set; }
    [XmlElement(ElementName = "schoolguid")]
    public string? Schoolguid { get; set; }
    [XmlElement(ElementName = "webapp")]
    public string? Webapp { get; set; }
    [XmlElement(ElementName = "schoolexternalid")]
    public string? Schoolexternalid { get; set; }
    [XmlElement(ElementName = "class")]
    public string? Class { get; set; }
    [XmlElement(ElementName = "group")]
    public string? Group { get; set; }
    [XmlElement(ElementName = "year")]
    public string? Year { get; set; }
    [XmlElement(ElementName = "fname")]
    public string? Fname { get; set; }
    [XmlElement(ElementName = "lname")]
    public string? Lname { get; set; }
    [XmlElement(ElementName = "username")]
    public string? Username { get; set; }
    [XmlElement(ElementName = "socialnumber")]
    public string? Socialnumber { get; set; }
    [XmlElement(ElementName = "guid")]
    public string? Guid { get; set; }
    [XmlElement(ElementName = "notpublish")]
    public string? Notpublish { get; set; }
    [XmlElement(ElementName = "email")]
    public string? Email { get; set; }
    [XmlElement(ElementName = "workphone")]
    public string? Workphone { get; set; }
    [XmlElement(ElementName = "mobile")]
    public string? Mobile { get; set; }
    [XmlElement(ElementName = "homephone")]
    public string? Homephone { get; set; }
    [XmlElement(ElementName = "address1")]
    public string? Address1 { get; set; }
    [XmlElement(ElementName = "address2")]
    public string? Address2 { get; set; }
    [XmlElement(ElementName = "pocode")]
    public string? Pocode { get; set; }
    [XmlElement(ElementName = "city")]
    public string? City { get; set; }
    [XmlElement(ElementName = "sex")]
    public string? Sex { get; set; }
    [XmlElement(ElementName = "swimmingability")]
    public string? Swimmingability { get; set; }
    [XmlElement(ElementName = "language2")]
    public string? Language2 { get; set; }
    [XmlElement(ElementName = "language1")]
    public string? Language1 { get; set; }
    [XmlElement(ElementName = "mothertongue")]
    public string? Mothertongue { get; set; }
    [XmlElement(ElementName = "schooltype")]
    public string? Schooltype { get; set; }
    [XmlElement(ElementName = "leisureschool")]
    public string? Leisureschool { get; set; }
    [XmlElement(ElementName = "absencemessagetype")]
    public string? Absencemessagetype { get; set; }
    [XmlElement(ElementName = "active")]
    public string? Active { get; set; }
    [XmlElement(ElementName = "contactinfo")]
    public string? Contactinfo { get; set; }
    [XmlElement(ElementName = "lastlogin")]
    public string? Lastlogin { get; set; }
    [XmlElement(ElementName = "logincount")]
    public string? Logincount { get; set; }
    [XmlElement(ElementName = "classroomaccount")]
    public string? Classroomaccount { get; set; }
    [XmlElement(ElementName = "classroompaired")]
    public string? Classroompaired { get; set; }
    [XmlElement(ElementName = "startdate")]
    public string? Startdate { get; set; }
    [XmlElement(ElementName = "enddate")]
    public string? Enddate { get; set; }
    [XmlElement(ElementName = "source")]
    public string? Source { get; set; }
}

[XmlRoot(ElementName = "students")]
public class Students
{
    [XmlElement(ElementName = "student")]
    public List<Student>? Student { get; set; }
}


