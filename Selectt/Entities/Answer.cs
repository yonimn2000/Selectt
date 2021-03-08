using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectt.Entities
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }

        [ForeignKey("Poll")]
        public int PollId { get; set; }

        [Required]
        public string PollAnswer { get; set; }

        public virtual Poll Poll { get; set; }
        public virtual ICollection<Response> Responses { get; set; }
    }
}