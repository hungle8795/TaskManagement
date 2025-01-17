using System.ComponentModel.DataAnnotations;

namespace Client.Models
{
    public class TaskDetail
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "名前は必須です。")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "担当は必須です。")]
        public string Assignee { get; set; } = string.Empty;

        public DateTime? StartDate { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public string? Description { get; set; } = string.Empty;
    }
}
