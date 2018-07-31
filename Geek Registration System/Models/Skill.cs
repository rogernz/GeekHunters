using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Geek_Registration_System.Models
{
    public class Skill
    {
        public Skill()
        {
            this.Candidates = new HashSet<Candidate>();
        }

        public int SkillID { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }


    }
}