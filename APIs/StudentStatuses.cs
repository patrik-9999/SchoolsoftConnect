using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace APIs
{
    [XmlRoot(ElementName = "studentstatus")]
    public class StudentStatus
    {
        [XmlElement(ElementName = "id")]
        public string? Id { get; set; }
        [XmlElement(ElementName = "class")]
        public string? Class { get; set; }
        [XmlElement(ElementName = "name")]
        public string? Name { get; set; }
        [XmlElement(ElementName = "socialnumber")]
        public string? Socialnumber { get; set; }
        [XmlElement(ElementName = "fromdate")]
        public string? Fromdate { get; set; }
        [XmlElement(ElementName = "todate")]
        public string? Todate { get; set; }
        [XmlElement(ElementName = "district")]
        public string? District { get; set; }
        [XmlElement(ElementName = "invoicedate")]
        public string? Invoicedate { get; set; }
        [XmlElement(ElementName = "year")]
        public string? Year { get; set; }
        [XmlElement(ElementName = "schooltype")]
        public string? Schooltype { get; set; }
        [XmlElement(ElementName = "schoolunit")]
        public string? Schoolunit { get; set; }
        [XmlElement(ElementName = "hasmothertongue")]
        public string? Hasmothertongue { get; set; }
        [XmlElement(ElementName = "studyguidemothertongue")]
        public string? Studyguidemothertongue { get; set; }
        [XmlElement(ElementName = "sva")]
        public string? Sva { get; set; }
        [XmlElement(ElementName = "specialsupportindividual")]
        public string? Specialsupportindividual { get; set; }
        [XmlElement(ElementName = "specialteachinggroup")]
        public string? Specialteachinggroup { get; set; }
        [XmlElement(ElementName = "individualintegratedstudent")]
        public string? Individualintegratedstudent { get; set; }
        [XmlElement(ElementName = "graneducationtype")]
        public string? Graneducationtype { get; set; }
        [XmlElement(ElementName = "grancourseplan")]
        public string? Grancourseplan { get; set; }
        [XmlElement(ElementName = "specialdistanceteachings")]
        public string? Specialdistanceteachings { get; set; }
        [XmlElement(ElementName = "specialotherteachings")]
        public string? Specialotherteachings { get; set; }
        [XmlElement(ElementName = "adaptedstudies")]
        public string? Adaptedstudies { get; set; }
        [XmlElement(ElementName = "leisureschoolhours")]
        public string? Leisureschoolhours { get; set; }
        [XmlElement(ElementName = "inconvenienttimes")]
        public string? Inconvenienttimes { get; set; }
        [XmlElement(ElementName = "preschoolhours")]
        public string? Preschoolhours { get; set; }
        [XmlElement(ElementName = "nationalprogram")]
        public string? Nationalprogram { get; set; }
        [XmlElement(ElementName = "replacementprice")]
        public string? Replacementprice { get; set; }
        [XmlElement(ElementName = "parentprice")]
        public string? Parentprice { get; set; }
        [XmlElement(ElementName = "parentprice2")]
        public string? Parentprice2 { get; set; }
        [XmlElement(ElementName = "adjustmentprice")]
        public string? Adjustmentprice { get; set; }
        [XmlElement(ElementName = "category")]
        public string? Category { get; set; }
        [XmlElement(ElementName = "comment")]
        public string? Comment { get; set; }
        [XmlElement(ElementName = "orgid")]
        public string? Orgid { get; set; }
        [XmlElement(ElementName = "sequence")]
        public string? Sequence { get; set; }
    }

    [XmlRoot(ElementName = "studentstatuses")]
    public class StudentStatuses
    {
        [XmlElement(ElementName = "studentstatus")]
        public List<StudentStatus>? StudentStatus { get; set; }
    }

}
