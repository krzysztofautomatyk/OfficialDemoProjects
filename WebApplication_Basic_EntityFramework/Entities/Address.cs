using System.ComponentModel.DataAnnotations;

namespace WebApplication_Basic_EntityFramework.Entities
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        // User and UserId are used to create a one-to-one relationship between Address and User
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
