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
    public class LateEnrollmentController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /LateEnrollment/

        public ViewResult Index()
        {
            var requestforlateenrolments = db.requestForLateEnrolments.Include("issue");
            return View(requestforlateenrolments.ToList());
        }

        //
        // GET: /LateEnrollment/Details/5

        public ViewResult Details(Guid id)
        {
            requestForLateEnrolment requestforlateenrolment = db.requestForLateEnrolments.Single(r => r.enrolementid == id);
            return View(requestforlateenrolment);
        }

        //
        // GET: /LateEnrollment/Create

        public ActionResult Create()
        {
            ViewBag.issueid = new SelectList(db.issues, "issueid", "studentid");
            return View();
        } 

        //
        // POST: /LateEnrollment/Create

        [HttpPost]
        public ActionResult Create(requestForLateEnrolment requestforlateenrolment)
        {
            if (ModelState.IsValid)
            {
                requestforlateenrolment.enrolementid = Guid.NewGuid();
                db.requestForLateEnrolments.AddObject(requestforlateenrolment);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.issueid = new SelectList(db.issues, "issueid", "studentid", requestforlateenrolment.issueid);
            return View(requestforlateenrolment);
        }
        
        //
        // GET: /LateEnrollment/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            requestForLateEnrolment requestforlateenrolment = db.requestForLateEnrolments.Single(r => r.enrolementid == id);
            ViewBag.issueid = new SelectList(db.issues, "issueid", "studentid", requestforlateenrolment.issueid);
            return View(requestforlateenrolment);
        }

        //
        // POST: /LateEnrollment/Edit/5

        [HttpPost]
        public ActionResult Edit(requestForLateEnrolment requestforlateenrolment)
        {
            if (ModelState.IsValid)
            {
                db.requestForLateEnrolments.Attach(requestforlateenrolment);
                db.ObjectStateManager.ChangeObjectState(requestforlateenrolment, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.issueid = new SelectList(db.issues, "issueid", "studentid", requestforlateenrolment.issueid);
            return View(requestforlateenrolment);
        }

        //
        // GET: /LateEnrollment/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            requestForLateEnrolment requestforlateenrolment = db.requestForLateEnrolments.Single(r => r.enrolementid == id);
            return View(requestforlateenrolment);
        }

        //
        // POST: /LateEnrollment/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            requestForLateEnrolment requestforlateenrolment = db.requestForLateEnrolments.Single(r => r.enrolementid == id);
            db.requestForLateEnrolments.DeleteObject(requestforlateenrolment);
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