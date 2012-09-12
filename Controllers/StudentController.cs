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
    public class StudentController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Student/

        public ViewResult Index()
        {
            var students = db.students.Include("campu").Include("program");
            return View(students.ToList());
        }

        //
        // GET: /Student/Details/5

        public ViewResult Details(string id)
        {
            student student = db.students.Single(s => s.studentid == id);
            return View(student);
        }

        //
        // GET: /Student/Create

        public ActionResult Create()
        {
            ViewBag.campus = new SelectList(db.campus, "cname", "street");
            ViewBag.programcode = new SelectList(db.programs, "programcode", "programname");
            return View();
        } 

        //
        // POST: /Student/Create

        [HttpPost]
        public ActionResult Create(student student)
        {
            if (ModelState.IsValid)
            {
                db.students.AddObject(student);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.campus = new SelectList(db.campus, "cname", "street", student.campus);
            ViewBag.programcode = new SelectList(db.programs, "programcode", "programname", student.programcode);
            return View(student);
        }
        
        //
        // GET: /Student/Edit/5
 
        public ActionResult Edit(string id)
        {
            student student = db.students.Single(s => s.studentid == id);
            ViewBag.campus = new SelectList(db.campus, "cname", "street", student.campus);
            ViewBag.programcode = new SelectList(db.programs, "programcode", "programname", student.programcode);
            return View(student);
        }

        //
        // POST: /Student/Edit/5

        [HttpPost]
        public ActionResult Edit(student student)
        {
            if (ModelState.IsValid)
            {
                db.students.Attach(student);
                db.ObjectStateManager.ChangeObjectState(student, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.campus = new SelectList(db.campus, "cname", "street", student.campus);
            ViewBag.programcode = new SelectList(db.programs, "programcode", "programname", student.programcode);
            return View(student);
        }

        //
        // GET: /Student/Delete/5
 
        public ActionResult Delete(string id)
        {
            student student = db.students.Single(s => s.studentid == id);
            return View(student);
        }

        //
        // POST: /Student/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {            
            student student = db.students.Single(s => s.studentid == id);
            db.students.DeleteObject(student);
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