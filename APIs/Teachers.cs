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


    //[XmlElement(ElementName = "group")]
    //public List<string> Group { get; set; } = new List<string>();

    //[XmlElement(ElementName = "guid")]
    //public string Guid { get; set; } = string.Empty;

    //[XmlElement(ElementName = "id")]
    //public int Id { get; set; }

    //[XmlElement(ElementName = "externalref")]
    //public string Externalref { get; set; } = string.Empty;

    //[XmlElement(ElementName = "externalid")]
    //public string Externalid { get; set; } = string.Empty;

    //[XmlElement(ElementName = "fname")]
    //public string Fname { get; set; } = string.Empty;

    //[XmlElement(ElementName = "lname")]
    //public string Lname { get; set; } = string.Empty;

    //[XmlElement(ElementName = "initial")]
    //public string Initial { get; set; } = string.Empty;

    //[XmlElement(ElementName = "socialnumber")]
    //public string Socialnumber { get; set; } = string.Empty;

    //[XmlElement(ElementName = "username")]
    //public string Username { get; set; } = string.Empty;

    //[XmlElement(ElementName = "email")]
    //public string Email { get; set; } = string.Empty;

    //[XmlElement(ElementName = "workphone")]
    //public string Workphone { get; set; } = string.Empty;

    //[XmlElement(ElementName = "mobile")]
    //public string Mobile { get; set; } = string.Empty;

    //[XmlElement(ElementName = "homephone")]
    //public string Homephone { get; set; } = string.Empty;

    //[XmlElement(ElementName = "address1")]
    //public string Address1 { get; set; } = string.Empty;

    //[XmlElement(ElementName = "address2")]
    //public string Address2 { get; set; } = string.Empty;

    //[XmlElement(ElementName = "pocode")]
    //public string Pocode { get; set; } = string.Empty;

    //[XmlElement(ElementName = "city")]
    //public string City { get; set; } = string.Empty;

    //[XmlElement(ElementName = "contactinfo")]
    //public string Contactinfo { get; set; } = string.Empty;

    //[XmlElement(ElementName = "active")]
    //public int Active { get; set; }

    //[XmlElement(ElementName = "type")]
    //public int Type { get; set; }

    //[XmlElement(ElementName = "lastlogin")]
    //public string Lastlogin { get; set; } = string.Empty;

    //[XmlElement(ElementName = "logincount")]
    //public int Logincount { get; set; }

    //[XmlElement(ElementName = "role")]
    //public List<string> Role { get; set; } = new List<string>();
}

[XmlRoot(ElementName = "teachers")]
public class Teachers
{

    [XmlElement(ElementName = "teacher")]
    public List<Teacher> Teacher { get; set; } = new List<Teacher>();
}




//namespace Xml2CSharp
//{
//    [XmlRoot(ElementName = "teacher")]
//    public class Teacher
//    {
//        [XmlElement(ElementName = "id")]
//        public string Id { get; set; }
//        [XmlElement(ElementName = "fname")]
//        public string Fname { get; set; }
//        [XmlElement(ElementName = "lname")]
//        public string Lname { get; set; }
//        [XmlElement(ElementName = "orgid")]
//        public string Orgid { get; set; }
//        [XmlElement(ElementName = "socialnumber")]
//        public string Socialnumber { get; set; }
//        [XmlElement(ElementName = "username")]
//        public string Username { get; set; }
//        [XmlElement(ElementName = "email")]
//        public string Email { get; set; }
//        [XmlElement(ElementName = "workphone")]
//        public string Workphone { get; set; }
//        [XmlElement(ElementName = "mobile")]
//        public string Mobile { get; set; }
//        [XmlElement(ElementName = "homephone")]
//        public string Homephone { get; set; }
//        [XmlElement(ElementName = "address1")]
//        public string Address1 { get; set; }
//        [XmlElement(ElementName = "address2")]
//        public string Address2 { get; set; }
//        [XmlElement(ElementName = "pocode")]
//        public string Pocode { get; set; }
//        [XmlElement(ElementName = "city")]
//        public string City { get; set; }
//        [XmlElement(ElementName = "contactinfo")]
//        public string Contactinfo { get; set; }
//        [XmlElement(ElementName = "lastlogin")]
//        public string Lastlogin { get; set; }
//        [XmlElement(ElementName = "logincount")]
//        public string Logincount { get; set; }
//        [XmlElement(ElementName = "classroomaccount")]
//        public string Classroomaccount { get; set; }
//        [XmlElement(ElementName = "classroompaired")]
//        public string Classroompaired { get; set; }
//        [XmlElement(ElementName = "guid")]
//        public string Guid { get; set; }
//        [XmlElement(ElementName = "role")]
//        public string Role { get; set; }
//    }

//    [XmlRoot(ElementName = "teachers")]
//    public class Teachers
//    {
//        [XmlElement(ElementName = "teacher")]
//        public List<Teacher> Teacher { get; set; }
//    }

//}