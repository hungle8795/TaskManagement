namespace Client.Models
{
    public class TaskDetail
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string? DetailDesciption { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Comment { get; set; }
        public virtual Task Task { get; set; }
    }
}
