namespace Server.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Assignee { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public virtual ICollection<TaskDetail> TaskDetails { get; set; } = new List<TaskDetail>();
    }
}
