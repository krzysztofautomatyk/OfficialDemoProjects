using System.ComponentModel.DataAnnotations;

namespace WebApplication_Basic_EntityFramework.Entities
{
    public class Tag
    {
        //[Key]
        public int Id { get; set; }
        public string Value { get; set; }

        public List<WorkItem> WorkItems { get; set; }

        // This is for old version of EF Core
        //public List<WorkItemTag> WorkItemTags { get; set; } = new List<WorkItemTag>();
    }
}
