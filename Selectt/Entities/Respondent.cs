using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectt.Entities
{
    public class Respondent
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Poll")]
        public int PollId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }

        [DisplayName("Has Voted")]
        public bool HasVoted { get; set; }

        public virtual Poll Poll { get; set; }

        public Respondent()
        {
            Token = Utils.GenerateToken(16);
        }
    }
}