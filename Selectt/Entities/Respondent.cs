using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectt.Entities
{
    public class Respondent
    {
        [Key]
        public int RespondentId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }

        [DisplayName("Has Voted")]
        public bool HasVoted { get; set; }

        [ForeignKey("Poll")]
        public int PollId { get; set; }

        public virtual Poll Poll { get; set; }

        public Respondent()
        {
            Token = Utils.GenerateToken(16);
        }
    }
}