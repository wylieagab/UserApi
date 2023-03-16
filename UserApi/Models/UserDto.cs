using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using UserApi.Models.Extensions;

namespace UserApi.Models
{
    [XmlRoot(ElementName = "user")]
    public class UserDto
    {
        [XmlElement(ElementName = "id")]
        public int Id { get; set; }

        [Required]
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        
        [Required]
        [XmlElement(ElementName = "email")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [XmlElement(ElementName = "address")]
        public string? Address { get; set; }

        [XmlElement(ElementName = "dob")]
        [DataType(DataType.DateTime)]
        [JsonConverter(typeof(DateTimeJsonConverter), "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ss", "MM-dd-yyyy")]
        public DateTime? DOB { get; set; }
    }
}
