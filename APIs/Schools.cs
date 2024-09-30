using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace APIs;
#pragma warning disable IDE1006  // Namnkonvention för variabler, vill ha samma namn på varibeln som i dok från Schoolsoft


// using System.Xml.Serialization;
// XmlSerializer serializer = new XmlSerializer(typeof(schools));
// using (StringReader reader = new StringReader(xml))
// {
//    var test = (schools)serializer.Deserialize(reader);
// }

[XmlRoot(ElementName = "school")]
public class School
{

    [XmlElement(ElementName = "orgid")]
    public int orgid { get; set; }

    [XmlElement(ElementName = "name")]
    public string name { get; set; } = string.Empty;

    [XmlElement(ElementName = "description")]
    public string description { get; set; } = string.Empty;

    [XmlElement(ElementName = "authority")]
    public string authority { get; set; } = string.Empty;

    [XmlElement(ElementName = "active")]
    public int active { get; set; }

    [XmlElement(ElementName = "schoolcodeyh")]
    public string schoolcodeyh { get; set; } = string.Empty;

    [XmlElement(ElementName = "schoolidyh")]
    public string schoolidyh { get; set; } = string.Empty;

    [XmlElement(ElementName = "schoolunitgymn11")]
    public string schoolunitgymn11 { get; set; } = string.Empty;

    [XmlElement(ElementName = "schoolunitgrund11")]
    public string schoolunitgrund11 { get; set; } = string.Empty;

    [XmlElement(ElementName = "schoolunitsargrund11")]
    public string schoolunitsargrund11 { get; set; } = string.Empty;

    [XmlElement(ElementName = "schoolunitsargymn13")]
    public string schoolunitsargymn13 { get; set; } = string.Empty;

    [XmlElement(ElementName = "schoolunitvux12")]
    public string schoolunitvux12 { get; set; } = string.Empty;

    [XmlElement(ElementName = "schoolunitvuxs12")]
    public string schoolunitvuxs12 { get; set; } = string.Empty;

    [XmlElement(ElementName = "schoolunitsfi12")]
    public string schoolunitsfi12 { get; set; } = string.Empty;

    [XmlElement(ElementName = "gymnschool11")]
    public int gymnschool11 { get; set; }

    [XmlElement(ElementName = "sargrundschool11")]
    public int sargrundschool11 { get; set; }

    [XmlElement(ElementName = "sargymnschool13")]
    public int sargymnschool13 { get; set; }

    [XmlElement(ElementName = "vux12")]
    public int vux12 { get; set; }

    [XmlElement(ElementName = "vuxs12")]
    public int vuxs12 { get; set; }

    [XmlElement(ElementName = "sfi12")]
    public int sfi12 { get; set; }

    [XmlElement(ElementName = "yh")]
    public int yh { get; set; }

    [XmlElement(ElementName = "grundschool11")]
    public int grundschool11 { get; set; }

    [XmlElement(ElementName = "preschool")]
    public int preschool { get; set; }

    [XmlElement(ElementName = "codecsn")]
    public string codecsn { get; set; } = string.Empty;

    [XmlElement(ElementName = "teacherid")]
    public int teacherid { get; set; }

    [XmlElement(ElementName = "fname")]
    public string fname { get; set; } = string.Empty;

    [XmlElement(ElementName = "lname")]
    public string lname { get; set; } = string.Empty;

    [XmlElement(ElementName = "email")]
    public string email { get; set; } = string.Empty;

    [XmlElement(ElementName = "address1")]
    public string address1 { get; set; } = string.Empty;

    [XmlElement(ElementName = "address2")]
    public string address2 { get; set; } = string.Empty;

    [XmlElement(ElementName = "pocode")]
    public string pocode { get; set; } = string.Empty;

    [XmlElement(ElementName = "city")]
    public string city { get; set; } = string.Empty;

    [XmlElement(ElementName = "districtcode")]
    public int districtcode { get; set; }

    [XmlElement(ElementName = "webapp")]
    public string webapp { get; set; } = string.Empty;

    [XmlElement(ElementName = "logoname")]
    public string logoname { get; set; } = string.Empty;

    [XmlElement(ElementName = "parentorgid")]
    public int parentorgid { get; set; }

    [XmlElement(ElementName = "externalid")]
    public string externalid { get; set; } = string.Empty;

    [XmlElement(ElementName = "externalref")]
    public string externalref { get; set; } = string.Empty;

    [XmlElement(ElementName = "guid")]
    public string guid { get; set; } = string.Empty;

    override public string ToString()
    {
        return $"{webapp} {orgid} {name} '{description}'";
    }
}

[XmlRoot(ElementName = "schools")]
public class Schools
{

    [XmlElement(ElementName = "school")]
    public List<School> SchoolExport { get; set; } = new List<School>();
}

