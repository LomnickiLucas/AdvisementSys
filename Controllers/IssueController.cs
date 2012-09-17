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
    public class IssueController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Issue/

        public ViewResult Index()
        {
            var issues = db.issues.Include("student");
            return View(issues.ToList());
        }

        //
        // GET: /Issue/Details/5

        public ViewResult Details(Guid id)
        {
            issue issue = db.issues.Include("student").Single(i => i.issueid == id);
            return View(issue);
        }

        //
        // GET: /Issue/Create

        public ActionResult Create(String id)
        {
            student student = db.students.Single(i => i.studentid == id);
            issue issue = new issue() { studentid = id, date = DateTime.Now };
            CreateIssueRequestModel model = new CreateIssueRequestModel() { _issue = issue, _student = student };
            return View(model);
        } 

        //
        // POST: /Issue/Create

        [HttpPost]
        public ActionResult Create(CreateIssueRequestModel model, String id)
        {
            if (ModelState.IsValid)
            {
                model._issue.issueid = Guid.NewGuid();
                model._issue.studentid = id;
                db.issues.AddObject(model._issue);
                db.SaveChanges();
                return RedirectToAction("Details/" + id, "Student");  
            }

            ViewBag.studentid = new SelectList(db.students, "studentid", "fname", model._issue.studentid);
            return View(model);
        }
        
        //
        // GET: /Issue/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            issue issue = db.issues.Include("student").Single(i => i.issueid == id);
            ViewBag.studentid = new SelectList(db.students, "studentid", "fname", issue.studentid);
            return View(issue);
        }

        //
        // POST: /Issue/Edit/5

        [HttpPost]
        public ActionResult Edit(issue issue)
        {
            if (ModelState.IsValid)
            {
                db.issues.Attach(issue);
                db.ObjectStateManager.ChangeObjectState(issue, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details/" + issue.issueid);  
            }
            ViewBag.studentid = new SelectList(db.students, "studentid", "fname", issue.studentid);
            return View(issue);
        }

        //
        // GET: /Issue/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            issue issue = db.issues.Single(i => i.issueid == id);
            return View(issue);
        }

        //
        // POST: /Issue/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            issue issue = db.issues.Single(i => i.issueid == id);
            db.issues.DeleteObject(issue);
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