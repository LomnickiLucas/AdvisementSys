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
    public class ReadmissionController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Readmission/

        public ViewResult Index()
        {
            var applicationforreadmissions = db.applicationForReadmissions.Include("issue");
            return View(applicationforreadmissions.ToList());
        }

        //
        // GET: /Readmission/Details/5

        public ViewResult Details(Guid id)
        {
            applicationForReadmission applicationforreadmission = db.applicationForReadmissions.Single(a => a.readmissionid == id);
            return View(applicationforreadmission);
        }

        //
        // GET: /Readmission/Create

        public ActionResult Create()
        {
            ViewBag.issueid = new SelectList(db.issues, "issueid", "studentid");
            return View();
        } 

        //
        // POST: /Readmission/Create

        [HttpPost]
        public ActionResult Create(applicationForReadmission applicationforreadmission)
        {
            if (ModelState.IsValid)
            {
                applicationforreadmission.readmissionid = Guid.NewGuid();
                db.applicationForReadmissions.AddObject(applicationforreadmission);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.issueid = new SelectList(db.issues, "issueid", "studentid", applicationforreadmission.issueid);
            return View(applicationforreadmission);
        }
        
        //
        // GET: /Readmission/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            applicationForReadmission applicationforreadmission = db.applicationForReadmissions.Single(a => a.readmissionid == id);
            ViewBag.issueid = new SelectList(db.issues, "issueid", "studentid", applicationforreadmission.issueid);
            return View(applicationforreadmission);
        }

        //
        // POST: /Readmission/Edit/5

        [HttpPost]
        public ActionResult Edit(applicationForReadmission applicationforreadmission)
        {
            if (ModelState.IsValid)
            {
                db.applicationForReadmissions.Attach(applicationforreadmission);
                db.ObjectStateManager.ChangeObjectState(applicationforreadmission, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.issueid = new SelectList(db.issues, "issueid", "studentid", applicationforreadmission.issueid);
            return View(applicationforreadmission);
        }

        //
        // GET: /Readmission/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            applicationForReadmission applicationforreadmission = db.applicationForReadmissions.Single(a => a.readmissionid == id);
            return View(applicationforreadmission);
        }

        //
        // POST: /Readmission/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            applicationForReadmission applicationforreadmission = db.applicationForReadmissions.Single(a => a.readmissionid == id);
            db.applicationForReadmissions.DeleteObject(applicationforreadmission);
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