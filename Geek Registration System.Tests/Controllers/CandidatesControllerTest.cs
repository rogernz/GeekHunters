using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geek_Registration_System.Controllers;
using System.Web.Mvc;
using Geek_Registration_System.Models;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Geek_Registration_System.Tests.Controllers
{


    [TestClass]
    public class CandidatesControllerTest
    {
        //public TestContext TestContext { get; set; }

        //// In test initialization - note the signature should be exactly this
        //// A static void method with one argument of type TestContext 
        //[ClassInitialize]
        //public static void SetUp(TestContext context)
        //{
        //    AppDomain.CurrentDomain.SetData("DataDirectory", context.TestDeploymentDir);
        //}

        CandidatesController controller = null;
        GRSDBContext db = null;
        public CandidatesControllerTest()
        {
            var controller = new CandidatesController();
            db = controller.db;
        }
        [TestMethod]
        public void TestDetailsView()
        {
            var controller = new CandidatesController();

            var result = controller.Details(2) as ViewResult;
            var candidate = (Candidate)result.Model;
            Assert.AreEqual("Meredith", candidate.FirstName);
        }

        [TestMethod]

        public void TestIndexView()

        {

            var controller = new CandidatesController();
            ViewResult result = controller.Index();

            Assert.AreEqual("Index", result.ViewName);

        }



        Candidate GetCandidate(int id, string firstName, string lastName)

        {

            return new Candidate { CandidateId = id, FirstName = firstName, LastName = lastName };

        }
        public List<int> getSkill()
        {
            return new List<int> { 1, 2, 3, }; ;

        }
        public IEnumerable<Candidate> getAllCandidate()

        {

            return db.Candidates.ToList();

        }


        public void Create_Post()

        {

            var controller = new CandidatesController();


            Candidate candidate = GetCandidate(9, "roger", "test");
            CandidateViewModel cvm = new CandidateViewModel();
            cvm.SelectedAllSkills = getSkill();
            cvm.Candidate = candidate;
            controller.Create(cvm);

            IEnumerable<Candidate> candidates = getAllCandidate();

            Assert.IsTrue(candidates.Contains(candidate));

        }
    }
}
