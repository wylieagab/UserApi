using System.Xml.Serialization;

namespace UserApi.Models.Dtos
{
    [XmlRoot(ElementName = "AddressDto")]
    public class AddressDto
    {
        [XmlElement(ElementName = "Id")]
        public int Id { get; set; }

        [XmlElement(ElementName = "Street")]
        public string Street { get; set; }

        [XmlElement(ElementName = "City")]
        public string City { get; set; }

        [XmlElement(ElementName = "State")]
        public string State { get; set; }

        [XmlElement(ElementName = "Country")]
        public string Country { get; set; }

        [XmlElement(ElementName = "ZipCode")]
        public string ZipCode { get; set; }
    }
}
