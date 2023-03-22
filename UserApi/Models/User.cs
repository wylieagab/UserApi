namespace UserApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Address? Address { get; set; }
        public DateTime? DOB { get; set; }
    }
}
