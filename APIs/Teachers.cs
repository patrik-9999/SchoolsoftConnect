using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace APIs;

[XmlRoot(ElementName = "teacher")]
public class Teacher
{

    [XmlElement(ElementName = "id")]
    public string? Id { get; set; }
    [XmlElement(ElementName = "fname")]
    public string? Fname { get; set; }
    [XmlElement(ElementName = "lname")]
    public string? Lname { get; set; }
    [XmlElement(ElementName = "orgid")]
    public string? Orgid { get; set; }
    [XmlElement(ElementName = "socialnumber")]
    public string? Socialnumber { get; set; }
    [XmlElement(ElementName = "username")]
    public string? Username { get; set; }
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
    [XmlElement(ElementName = "guid")]
    public string? Guid { get; set; }
    [XmlElement(ElementName = "role")]
    public string? Role { get; set; }

}

[XmlRoot(ElementName = "teachers")]
public class Teachers
{

    [XmlElement(ElementName = "teacher")]
    public List<Teacher> Teacher { get; set; } = new List<Teacher>();
}

