using Selectt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selectt.Entities
{
    public class Poll
    {
        [Key]
        public int PollId { get; set; }

        [Required]
        [ForeignKey("Owner")]
        public string OwnerId { get; set; }        

        [Required]
        public string Name { get; set; }

        [Required]
        public string Question { get; set; }

        [DisplayName("End Date Time")]
        public DateTime? EndDateTime { get; set; }

        [DisplayName("Are Results Public")]
        public bool AreResultsPublic { get; set; }

        [DisplayName("Was Sent")]
        public bool WasSent { get; set; }

        public virtual ApplicationUser Owner { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Respondent> Respondents { get; set; }
    }
}