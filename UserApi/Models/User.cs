using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace UserApi.Models
{
    [XmlRoot(ElementName = "user")]
    public class User
    {
        [XmlElement(ElementName = "id")]
        public int Id { get; set; }

        [Required]
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        
        [Required]
        [XmlElement(ElementName = "email")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [XmlElement(ElementName = "address")]
        public string? Address { get; set; }

        [XmlElement(ElementName = "dob")]
        [DataType(DataType.DateTime)]
        public DateTime? DOB { get; set; }
    }
}
