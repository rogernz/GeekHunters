using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Geek_Registration_System.Models
{
    public class Candidate
    {
        public Candidate()
        {
            this.Skills = new HashSet<Skill>();
        }

        public int CandidateId { get; set; }
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
    }
}
