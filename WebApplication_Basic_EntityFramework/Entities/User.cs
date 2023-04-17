using System.ComponentModel.DataAnnotations;

namespace WebApplication_Basic_EntityFramework.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }  
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        
        public Address Address { get; set; }

        public List<WorkItem> WorkItems { get; set; } = new List<WorkItem>();

    }
}
