using System.ComponentModel.DataAnnotations;

namespace WebApplication_Basic_EntityFramework.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; } 
        public User Author { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set;}

        // WorkItem and WorkItemId are used to create a one-to-one relationship between Comment and WorkItem
        // WorkItem is the navigation property, and WorkItemId is the foreign key
        // WorkItemId is type int because it is a reference to the primary key of WorkItem it also has a int Type 
        public int WorkItemId { get; set; }
        public WorkItem WorkItem { get; set; }

    }
}
