using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using UserApi.Models.Extensions;

namespace UserApi.Models.Dtos
{
    [XmlRoot(ElementName = "UserDto")]
    public class UserDto
    {
        [XmlElement(ElementName = "Id")]
        public int Id { get; set; }

        [Required]
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [Required]
        [XmlElement(ElementName = "Email")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [XmlElement(ElementName = "Address")]
        public AddressDto? Address { get; set; }

        [XmlElement(ElementName = "DOB")]
        [DataType(DataType.DateTime)]
        [JsonConverter(typeof(DateTimeJsonConverter), "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-ddTHH:mm:ss.fffZ", "MM-dd-yyyy")]
        public DateTime? DOB { get; set; }
    }
}
