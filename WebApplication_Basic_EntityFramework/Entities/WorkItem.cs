using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_Basic_EntityFramework.Entities
{
    public class Epic : WorkItem
    {
        // Epic <============
        public DateTime? StartDate { get; set; } //nullable 

        //[Precision(3)]
        public DateTime? EndDate { get; set; } //nullable
    }   

    public class Issue : WorkItem
    {
        // Issue <============
        //[Column(TypeName = "decimal(5,2)")]
        public decimal Efford { get; set; }
    }

    public class Task : WorkItem
    {
        // Task <============
        //[MaxLength(200)]
        public string Activity { get; set; }
        //[Precision(14,2)]
        public decimal RemaningWork { get; set; }
    }

    // This is for the many-to-many relationship between WorkItem and Tag
    // WorkItem is the navigation property, and WorkItemTags is the foreign key
    // WorkItemTags is type List<WorkItemTag> because it is a reference to the primary key of WorkItemTag it also has a int Type
    public abstract class WorkItem
    {
        //[Key]
        public int Id { get; set; }
          
        //[Column(TypeName = "varchar(200)")]
        public string Area { get; set; }

        //[Column("Iteration_Path")]
        public string IterationPath { get; set; }
        public int Priority { get; set; }
  
        public string Type { get; set; }

        // This is for the one-to-many relationship between WorkItem and Comment
        // WorkItem is the navigation property, and Comments is the foreign key
        // Comments is type List<Comment> because it is a reference to the primary key of Comment it also has a int Typ
        public List<Comment> Comments { get; set; } = new List<Comment>();

        // This is for the one-to-one relationship between User and WorkItem
        // User is the navigation property, and AuthorId is the foreign key
        // AuthorId is type Guid because it is a reference to the primary key of User it also has a Guid Type
        public User Author { get; set; }
        public Guid AuthorId { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();

        public WorkItemState  State { get; set; }
        public int StateId { get; set; }

        // This is for the many-to-many relationship between WorkItem and Tag
        // WorkItem is the navigation property, and WorkItemTags is the foreign key
        // WorkItemTags is type List<WorkItemTag> because it is a reference to the primary key of WorkItemTag it also has a int Type
        // This is for old version of EF Core
        //public List<WorkItemTag> WorkItemTags { get; set; } = new List<WorkItemTag>();
    }
}
