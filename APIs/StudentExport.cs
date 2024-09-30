using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

#pragma warning disable IDE1006  // Namnkonvention för variabler

namespace APIs;

// using System.Xml.Serialization;
// XmlSerializer serializer = new XmlSerializer(typeof(students));
// using (StringReader reader = new StringReader(xml))
// {
//    var test = (students)serializer.Deserialize(reader);
// }

[XmlRoot(ElementName = "student")]
public class StudentExport
{
	[XmlElement(ElementName = "id")]
	public int id { get; set; }

    [XmlElement(ElementName = "personguid")]
    public string personguid { get; set; } = string.Empty;

    [XmlElement(ElementName = "studentorganizationguid")]
    public string studentorganizationguid { get; set; } = string.Empty;
    
    [XmlElement(ElementName = "externalref")]
    public string externalref { get; set; } = string.Empty;
    
    [XmlElement(ElementName = "externalid")]
    public string externalid { get; set; } = string.Empty;

    [XmlElement(ElementName = "orgid")]
	public int orgid { get; set; }
	
	[XmlElement(ElementName = "schoolname")]
	public string schoolname { get; set; } = string.Empty;

    [XmlElement(ElementName = "schoolguid")]
    public string schoolguid { get; set; } = string.Empty;

    [XmlElement(ElementName = "webapp")]
	public string webapp { get; set; } = string.Empty;

    [XmlElement(ElementName = "schoolexternalid")]
    public string schoolexternalid { get; set; } = string.Empty;

    [XmlElement(ElementName = "class")]
	public string @class { get; set; } = string.Empty;

	[XmlElement(ElementName = "group")]
	public List<string> group { get; set; } = [];

    [XmlElement(ElementName = "year")]
	public string year { get; set; } = string.Empty;

	[XmlElement(ElementName = "fname")]
	public string fname { get; set; } = string.Empty;

	[XmlElement(ElementName = "lname")]
	public string lname { get; set; } = string.Empty;

	[XmlElement(ElementName = "username")]
	public string username { get; set; } = string.Empty;
	
	[XmlElement(ElementName = "socialnumber")]
	public string socialnumber { get; set; } = string.Empty;

	[XmlElement(ElementName = "guid")]
	public string guid { get; set; } = string.Empty;
	
	[XmlElement(ElementName = "notpublish")]
	public int notpublish { get; set; }

	[XmlElement(ElementName = "email")]
	public string email { get; set; } = string.Empty;

	[XmlElement(ElementName = "workphone")]
	public string workphone { get; set; } = string.Empty;

	[XmlElement(ElementName = "mobile")]
	public string mobile { get; set; } = string.Empty;

	[XmlElement(ElementName = "homephone")]
	public string homephone { get; set; } = string.Empty;

	[XmlElement(ElementName = "address1")]
	public string address1 { get; set; } = string.Empty;

	[XmlElement(ElementName = "address2")]
	public string address2 { get; set; } = string.Empty;

	[XmlElement(ElementName = "pocode")]
	public string pocode { get; set; } = string.Empty;

	[XmlElement(ElementName = "city")]
	public string city { get; set; } = string.Empty;

	[XmlElement(ElementName = "sex")]
	public string sex { get; set; } = string.Empty;

	[XmlElement(ElementName = "swimmingability")]
	public int swimmingability { get; set; }

	[XmlElement(ElementName = "language2")]
	public string language2 { get; set; } = string.Empty;

	[XmlElement(ElementName = "language1")]
	public string language1 { get; set; } = string.Empty;

	[XmlElement(ElementName = "mothertongue")]
	public string mothertongue { get; set; } = string.Empty;

	[XmlElement(ElementName = "userfield0")]
	public string userfield0 { get; set; } = string.Empty;
	
	[XmlElement(ElementName = "userfield1")]
	public string userfield1 { get; set; } = string.Empty;
	
	[XmlElement(ElementName = "userfield2")]
	public string userfield2 { get; set; } = string.Empty;
	
	[XmlElement(ElementName = "userfield3")]
	public string userfield3 { get; set; } = string.Empty;
	
	[XmlElement(ElementName = "userfield4")]
	public string userfield4 { get; set; } = string.Empty;
	
	[XmlElement(ElementName = "userfield5")]
	public string userfield5 { get; set; } = string.Empty;
	
	[XmlElement(ElementName = "userfield6")]
	public string userfield6 { get; set; } = string.Empty;
	
	[XmlElement(ElementName = "userfield7")]
	public string userfield7 { get; set; } = string.Empty;
	
	[XmlElement(ElementName = "userfield8")]
	public string userfield8 { get; set; } = string.Empty;
	
	[XmlElement(ElementName = "userfield9")]
	public string userfield9 { get; set; } = string.Empty;

	[XmlElement(ElementName = "schooltype")]
	public string schooltype { get; set; } = string.Empty;

	[XmlElement(ElementName = "leisureschool")]
	public int leisureschool { get; set; }

	[XmlElement(ElementName = "absencemessagetype")]
	public int absencemessagetype { get; set; }

	[XmlElement(ElementName = "active")]
	public int active { get; set; }

	[XmlElement(ElementName = "contactinfo")]
	public string contactinfo { get; set; } = string.Empty;

	[XmlElement(ElementName = "lastlogin")]
	public string lastlogin { get; set; } = string.Empty;

	[XmlElement(ElementName = "logincount")]
	public int logincount { get; set; }
	
	[XmlElement(ElementName = "classroomaccount")]
	public string classroomaccount { get; set; } = string.Empty;

    [XmlElement(ElementName = "classroompaired")]
	public bool classroompaired { get; set; }
    //public string classroompaired { get; set; } = string.Empty;

    [XmlElement(ElementName = "startdate")]
	public string _startdate { get; set; } = string.Empty;
	public DateTime? startdate
	{ 
        get
	    {
			return !string.IsNullOrEmpty(_startdate) && DateTime.TryParse(_startdate, out DateTime ret) ? ret : null;
		}
	}


	[XmlElement(ElementName = "enddate")]
	public string _enddate { get; set; } = string.Empty;
	public DateTime? enddate { 
		get
            {
                return !string.IsNullOrEmpty(_enddate) && DateTime.TryParse(_enddate, out DateTime ret) ? ret: null;
            }
	}
}

[XmlRoot(ElementName = "students")]
public class StudentsExport
{

	[XmlElement(ElementName = "student")]
	public List<StudentExport> StudentExport { get; set; } = [];
}

