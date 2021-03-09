using Selectt.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Selectt.Models
{
    public class PollCreateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Question { get; set; }

        [DisplayName("End Date Time (Optional)")]
        public DateTime? EndDateTime { get; set; }

        [DisplayName("Are Results Public")]
        public bool AreResultsPublic { get; set; }
    }

    public class PollEditModel : PollCreateModel
    {
        [Required]
        public int PollId { get; set; }

        public ICollection<Answer> Answers { get; set; }
        public ICollection<Respondent> Respondents { get; set; }
    }
}