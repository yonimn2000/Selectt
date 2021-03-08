using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectt.Entities
{
    public class Response
    {
        [Key]
        public int ResponseId { get; set; }

        [ForeignKey("Answer")]
        public int AnswerId { get; set; }

        public virtual Answer Answer { get; set; }
    }
}