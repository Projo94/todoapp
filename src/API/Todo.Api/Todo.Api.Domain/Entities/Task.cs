using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.Api.Domain.Entities
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "The max length of Title field is 50 characters!")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required(ErrorMessage = "The max length of Description field is 100 characters!")]
        [MaxLength(100)]
        public string Description { get; set; }

        public DateTimeOffset Deadline { get; set; }

        public bool IsDone { get; set; }

        public int TaskListId { get; set; }

        [ForeignKey("TaskListId")]

        public virtual TaskList TaskList { get; set; }
    }
}