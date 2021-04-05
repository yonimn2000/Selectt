using System.ComponentModel.DataAnnotations;

namespace Selectt.Models
{
    public class AnswerCreateModel
    {
        [Required]
        public int PollId { get; set; }

        [Required]
        public string Answer { get; set; }
    }

    public class AnswerEditModel
    {
        [Required]
        public int AnswerId { get; set; }

        [Required]
        public string Answer { get; set; }
    }
}