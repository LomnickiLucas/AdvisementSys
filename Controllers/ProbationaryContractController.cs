using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdvisementSys.Models;
using AdvisementSys.Models.Request;

namespace AdvisementSys.Controllers
{ 
    public class ProbationaryContractController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /ProbationaryContract/
        /// <summary>
        /// Not in use
        /// </summary>
        /// <returns></returns>
        public ViewResult Index()
        {
            var probationarycontractplans = db.probationaryContractPlans.Include("issue");
            return View(probationarycontractplans.ToList());
        }

        //
        // GET: /ProbationaryContract/Details/5
        /// <summary>
        /// Get's single Entry by ID P.S. All of the Forms do...
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(Guid id)
        {
            try
            {
                if (id != null)
                {
                    probationaryContractPlan probationarycontractplan = db.probationaryContractPlans.Single(p => p.advisementid == id);
                    issue issue = db.issues.Single(i => i.issueid == probationarycontractplan.issueid);
                    student student = db.students.Include("program").Single(s => s.studentid == issue.studentid);
                    DetailsProbationaryContractModel Model = new DetailsProbationaryContractModel() { _probationaryContractPlan = probationarycontractplan, _student = student };
                    return View(Model);
                }
                else
                {
                    return RedirectToAction("Index", "Issue");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Issue");
            }
        }

        //
        // GET: /ProbationaryContract/Create
        /// <summary>
        /// Create gets the required data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Create(Guid id)
        {
            try
            {
                if (id != null)
                {
                    probationaryContractPlan probationaryContractPlan = new probationaryContractPlan() { issueid = id, date = DateTime.Now };
                    issue issue = db.issues.Single(i => i.issueid == id);
                    student student = db.students.Include("program").Single(s => s.studentid == issue.studentid);
                    CreateProbationaryContractModel Model = new CreateProbationaryContractModel() { _probationaryContractPlan = probationaryContractPlan, _student = student };
                    return View(Model);
                }
                else
                {
                    return RedirectToAction("Index", "Issue");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Issue");
            }
        } 

        //
        // POST: /ProbationaryContract/Create
        /// <summary>
        /// Submits the Form data to the database
        /// </summary>
        /// <param name="createprobationarycontractplanmodel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(CreateProbationaryContractModel createprobationarycontractplanmodel)
        {
            if (ModelState.IsValid)
            {
                createprobationarycontractplanmodel._probationaryContractPlan.advisementid = Guid.NewGuid();
                db.probationaryContractPlans.AddObject(createprobationarycontractplanmodel._probationaryContractPlan);
                db.SaveChanges();
                return RedirectToAction("Details/" + createprobationarycontractplanmodel._probationaryContractPlan.advisementid);  
            }

            return View(createprobationarycontractplanmodel);
        }
        
        //
        // GET: /ProbationaryContract/Edit/5
        /// <summary>
        /// Gets data required for Edit View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(Guid id)
        {
            try
            {
                if (id != null)
                {
                    probationaryContractPlan probationarycontractplan = db.probationaryContractPlans.Single(p => p.advisementid == id);
                    issue issue = db.issues.Single(i => i.issueid == probationarycontractplan.issueid);
                    student student = db.students.Include("program").Single(s => s.studentid == issue.studentid);
                    EditProbationaryContractModel Model = new EditProbationaryContractModel() { _probationaryContractPlan = probationarycontractplan, _student = student };
                    return View(Model);
                }
                else
                {
                    return RedirectToAction("Index", "Issue");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Issue");
            }
        }

        //
        // POST: /ProbationaryContract/Edit/5
        /// <summary>
        /// Submits the Edit
        /// </summary>
        /// <param name="editprobationarycontractplanmodel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(EditProbationaryContractModel editprobationarycontractplanmodel)
        {
            if (ModelState.IsValid)
            {
                db.probationaryContractPlans.Attach(editprobationarycontractplanmodel._probationaryContractPlan);
                db.ObjectStateManager.ChangeObjectState(editprobationarycontractplanmodel._probationaryContractPlan, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details/" + editprobationarycontractplanmodel._probationaryContractPlan.advisementid);  
            }

            return View(editprobationarycontractplanmodel);
        }

        //
        // GET: /ProbationaryContract/Delete/5
        /// <summary>
        /// Not in use EVER!!!
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(Guid id)
        {
            probationaryContractPlan probationarycontractplan = db.probationaryContractPlans.Single(p => p.advisementid == id);
            issue issue = db.issues.Single(i => i.issueid == probationarycontractplan.issueid);
            student student = db.students.Include("program").Single(s => s.studentid == issue.studentid);
            DeleteProbationaryContractModel Model = new DeleteProbationaryContractModel() { _probationaryContractPlan = probationarycontractplan, _student = student };
            return View(Model);

        }

        //
        // POST: /ProbationaryContract/Delete/5
        /// <summary>
        /// For future use
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            probationaryContractPlan probationarycontractplan = db.probationaryContractPlans.Single(p => p.advisementid == id);
            db.probationaryContractPlans.DeleteObject(probationarycontractplan);
            db.SaveChanges();
            return RedirectToAction("Details/" + probationarycontractplan.issueid, "Issue");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}