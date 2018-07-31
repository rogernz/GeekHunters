using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Geek_Registration_System.Models;

namespace Geek_Registration_System.Controllers
{
    public class CandidatesController : Controller
    {
        public GRSDBContext db = null;

        public CandidatesController()
        {
            db = new GRSDBContext();
        }
        // GET: Candidates
        public ViewResult Index()
        {

            var result = TempData["Candidates"] as List<Candidate>;
            if (result == null)
            {
                result = db.Candidates.ToList();
            }
            ViewBag.DBSkills = db.Skills.ToList();
            return View("Index", result);
        }




        [HttpPost]
        public ActionResult SearchCandidate(int[] skills)
        {


            var candidates = db.Candidates.Include(i => i.Skills).ToList();
            var candidatesTmp = db.Candidates.Include(i => i.Skills).ToList();
            for (int k = 0; k < candidates.Count(); k++)

            {
                var skillList = candidates[k].Skills;
                var flag = true;
                for (int i = 0; i < skills.Length; i++)
                {
                    if (skillList.Where(a => a.SkillID == skills[i]).Count() == 0)
                    {
                        flag = false;
                        break;
                    }
                }
                if (!flag) candidatesTmp.Remove(candidates[k]);
            }


            TempData["Candidates"] = candidatesTmp.ToList();
            return PartialView("_SearchResultView", candidatesTmp.ToList());
            //return RedirectToAction("Index");

        }

        // GET: Candidates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = db.Candidates.ToList();
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // GET: Candidates/Create
        public ActionResult Create()
        {
            var CVM = new CandidateViewModel();
            var allSkillList = db.Skills.ToList();
            CVM.AllSkills = allSkillList.Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.SkillID.ToString()
            });

            return View(CVM);

        }

        // POST: Candidates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CandidateViewModel candidateVM)
        {
            if (ModelState.IsValid)
            {
                db.Candidates.Add(candidateVM.Candidate);
                db.SaveChanges();

                var sKillToUpdate = db.Candidates
                    .Include(i => i.Skills).First(i => i.CandidateId == candidateVM.Candidate.CandidateId);
                //var newSkills = db.Skills.Where(
                //  m => candidateVM.SelectedAllSkills.Contains(m.SkillID)).ToList();
                var newSkill = new HashSet<int>(candidateVM.SelectedAllSkills);
                foreach (Skill skill in db.Skills)
                {

                    if (newSkill.Contains(skill.SkillID))
                    {
                        sKillToUpdate.Skills.Add(skill);
                    }
                }

                db.Entry(sKillToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(candidateVM);



        }

        // GET: Candidates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var candidateViewModel = new CandidateViewModel
            {
                Candidate = db.Candidates.Include(i => i.Skills).First(i => i.CandidateId == id),
            };

            if (candidateViewModel.Candidate == null)
                return HttpNotFound();

            var allSkillList = db.Skills.ToList();
            candidateViewModel.AllSkills = allSkillList.Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.SkillID.ToString()
            });


            return View(candidateViewModel);

        }

        // POST: Candidates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CandidateViewModel candidateView)
        {
            if (ModelState.IsValid)
            {
                var sKillToUpdate = db.Candidates
                    .Include(i => i.Skills).First(i => i.CandidateId == candidateView.Candidate.CandidateId);

                if (TryUpdateModel(sKillToUpdate, "Candidate", new string[] { "FirstName", "LastName" }))
                {
                    // var newJobTags = db.Skills.Where(
                    //     m => candidateView.SelectedAllSkills.Contains(m.SkillID)).ToList();
                    var updatedSkill = new HashSet<int>(candidateView.SelectedAllSkills);
                    foreach (Skill skill in db.Skills)
                    {
                        if (!updatedSkill.Contains(skill.SkillID))
                        {
                            sKillToUpdate.Skills.Remove(skill);
                        }
                        else
                        {
                            sKillToUpdate.Skills.Add((skill));
                        }
                    }

                    db.Entry(sKillToUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            return RedirectToAction("Index");
        }

        // GET: Candidates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Candidate candidate = db.Candidates.First(j => j.CandidateId == id);

            db.Candidates.Remove(candidate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
