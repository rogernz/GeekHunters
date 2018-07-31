using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace Geek_Registration_System.Models
{
    public class DataInitializer : System.Data.Entity.DropCreateDatabaseAlways<GRSDBContext>
    {
        protected override void Seed(GRSDBContext context)
        {
            var candidates = new List<Candidate>
            {
            new Candidate{FirstName ="Carson",LastName="Alexander"},
            new Candidate{FirstName="Meredith",LastName="Alonso"},
            new Candidate{FirstName="Arturo",LastName="Anand"},
            new Candidate{FirstName="Gytis",LastName="Barzdukas"},
            new Candidate{FirstName="Yan",LastName="Li"},
            new Candidate{FirstName="Peggy",LastName="Justice"},
            new Candidate{FirstName="Laura",LastName="Norman"},
            new Candidate{FirstName="Nino",LastName="Olivetto"}
            };

            candidates.ForEach(s => context.Candidates.Add(s));
            context.SaveChanges();
            var skills = new List<Skill>
            {
            new Skill{ Name ="C#"},
            new Skill{ Name ="C++"},
            new Skill{ Name ="VB"},
            new Skill{ Name ="Java"},
            new Skill{ Name ="Linq"},
            new Skill{ Name ="Sharepoint"},
            new Skill{ Name ="ASP.NET"},
            new Skill{ Name ="XML"},
            };
            skills.ForEach(s => context.Skills.Add(s));
            context.SaveChanges();

            // Candidate skillToUpdate = new Candidate();
            for (int n = 1; n < 8; n++)
            {
                var skillToUpdate = context.Candidates
                   .Include(i => i.Skills).First(i => i.CandidateId == n);
                for (int t = n; t < 8; t++)
                {
                    foreach (Skill skill in context.Skills)
                    {

                        if (skill.SkillID == t)
                        {
                            skillToUpdate.Skills.Add(skill);
                        }
                    }
                }
                context.Entry(skillToUpdate).State = EntityState.Modified;
                context.SaveChanges();
            }

        }
    }
}