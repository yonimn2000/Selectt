using System.ComponentModel.DataAnnotations;

namespace Selectt.Models
{
    public class RespondentCreateModel
    {
        [Required]
        public int PollId { get; set; }

        [Required]
        public string Email { get; set; }
    }

    public class RespondentEditModel
    {
        [Required]
        public int RespondentId { get; set; }

        [Required]
        public string Email { get; set; }
    }
}