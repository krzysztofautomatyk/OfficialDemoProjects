namespace WebApplication_Basic_EntityFramework.Entities
{

    // This join table is called WorkItemTag
    // This join table has two foreign keys, one for WorkItem and one for Tag
    // The WorkItemTag class is used to create a many-to-many relationship between WorkItem and Tag
    // This is for old version of EF Core we not use this class
    public class WorkItemTag
    {
        public int WorkItemId { get; set; }
        public WorkItem WorkItem { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public DateTime? PublicationDate { get; set; }
    }
}
