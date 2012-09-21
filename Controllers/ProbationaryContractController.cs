using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdvisementSys.Models;

namespace AdvisementSys.Controllers
{ 
    public class ProbationaryContractController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /ProbationaryContract/

        public ViewResult Index()
        {
            var probationarycontractplans = db.probationaryContractPlans.Include("issue");
            return View(probationarycontractplans.ToList());
        }

        //
        // GET: /ProbationaryContract/Details/5

        public ViewResult Details(Guid id)
        {
            probationaryContractPlan probationarycontractplan = db.probationaryContractPlans.Single(p => p.advisementid == id);
            return View(probationarycontractplan);
        }

        //
        // GET: /ProbationaryContract/Create

        public ActionResult Create()
        {
            ViewBag.issueid = new SelectList(db.issues, "issueid", "studentid");
            return View();
        } 

        //
        // POST: /ProbationaryContract/Create

        [HttpPost]
        public ActionResult Create(probationaryContractPlan probationarycontractplan)
        {
            if (ModelState.IsValid)
            {
                probationarycontractplan.advisementid = Guid.NewGuid();
                db.probationaryContractPlans.AddObject(probationarycontractplan);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.issueid = new SelectList(db.issues, "issueid", "studentid", probationarycontractplan.issueid);
            return View(probationarycontractplan);
        }
        
        //
        // GET: /ProbationaryContract/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            probationaryContractPlan probationarycontractplan = db.probationaryContractPlans.Single(p => p.advisementid == id);
            ViewBag.issueid = new SelectList(db.issues, "issueid", "studentid", probationarycontractplan.issueid);
            return View(probationarycontractplan);
        }

        //
        // POST: /ProbationaryContract/Edit/5

        [HttpPost]
        public ActionResult Edit(probationaryContractPlan probationarycontractplan)
        {
            if (ModelState.IsValid)
            {
                db.probationaryContractPlans.Attach(probationarycontractplan);
                db.ObjectStateManager.ChangeObjectState(probationarycontractplan, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.issueid = new SelectList(db.issues, "issueid", "studentid", probationarycontractplan.issueid);
            return View(probationarycontractplan);
        }

        //
        // GET: /ProbationaryContract/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            probationaryContractPlan probationarycontractplan = db.probationaryContractPlans.Single(p => p.advisementid == id);
            return View(probationarycontractplan);
        }

        //
        // POST: /ProbationaryContract/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            probationaryContractPlan probationarycontractplan = db.probationaryContractPlans.Single(p => p.advisementid == id);
            db.probationaryContractPlans.DeleteObject(probationarycontractplan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}