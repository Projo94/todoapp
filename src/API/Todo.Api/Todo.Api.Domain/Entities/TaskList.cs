using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.Api.Domain.Entities
{
    public class TaskList
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

        [Required(ErrorMessage = "DailyList is required field!")]
        public DateTimeOffset DailyList { get; set; }

        [Required(ErrorMessage = "CreatedByUserId is required field")]
        [MaxLength(450)]
        public string CreatedByUserId { get; set; }

        [Required(ErrorMessage = "TimeZone is required field")]
        [MaxLength(50)]
        public string TimeZone { get; set; }
    }
}