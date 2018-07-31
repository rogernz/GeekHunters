using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Geek_Registration_System.Models
{
    public class CandidateViewModel
    {
        public Candidate Candidate { get; set; }
        public IEnumerable<SelectListItem> AllSkills { get; set; }

        private List<int> _selectedAllSkills;
        public List<int> SelectedAllSkills
        {
            get
            {
                if (_selectedAllSkills == null)
                {
                    if (Candidate == null) { return _selectedAllSkills; }
                    _selectedAllSkills = Candidate.Skills.Select(m => m.SkillID).ToList();
                }
                return _selectedAllSkills;
            }
            set { _selectedAllSkills = value; }
        }
    }
}