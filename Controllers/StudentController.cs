using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdvisementSys.Models;
using System.Text;

namespace AdvisementSys.Controllers
{ 
    public class StudentController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Student/

        public ViewResult Index()
        {
            ViewBag.campus = new SelectList(db.campus, "cname", "cname");
            var program = db.programs;
            List<String> collection = new List<String>();
            foreach (program pcode in program)
            {
                collection.Add(pcode.programcode.ToString().Trim() + " - " + pcode.programname.ToString().Trim());
            }
            ViewBag.programcode = new SelectList(collection);
            var students = db.students.Include("campu").Include("program");
            return View(students.ToList());
        }

        //
        // POST: /Student

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            ViewBag.campus = new SelectList(db.campus, "cname", "cname");
            var program = db.programs;
            List<String> collections = new List<String>();
            foreach (program pcode in program)
            {
                collections.Add(pcode.programcode.ToString().Trim() + " - " + pcode.programname.ToString().Trim());
            }
            ViewBag.programcode = new SelectList(collections);
            var students = db.students.Include("campu").Include("program");
            IEnumerable<student> studentz = students;
            if (!collection.Get(0).Equals(""))
                studentz = studentz.Where(stud => stud.studentid.Trim().ToUpper().Contains(collection.Get(0).Trim().ToUpper()));
            if (!collection.Get(1).Equals(""))
                studentz = studentz.Where(stud => stud.fname.Trim().ToUpper().Contains(collection.Get(1).Trim().ToUpper()));
            if (!collection.Get(2).Equals(""))
                studentz = studentz.Where(stud => stud.lname.Trim().ToUpper().Contains(collection.Get(2).Trim().ToUpper()));
            if (!collection.Get(3).Equals(""))
            {
                StringBuilder sb = new StringBuilder(collection.Get(3).Trim().ToUpper());
                sb.Remove(5, sb.Length - 5);
                studentz = studentz.Where(stud => stud.programcode.Trim().ToUpper().Contains(sb.ToString()));
            }
            if (!collection.Get(4).Equals(""))
                studentz = studentz.Where(stud => stud.campus.Trim().ToUpper().Contains(collection.Get(4).Trim().ToUpper()));
            if (!collection.Get(5).Equals(""))
                studentz = studentz.Where(stud => stud.email.Trim().ToUpper().Contains(collection.Get(5).Trim().ToUpper()));
            if (collection.Get(6).Equals("true,false"))
                studentz = studentz.Where(stud => stud.acadprobation.Equals(true));
            if (collection.Get(7).Equals("true,false"))
                studentz = studentz.Where(stud => stud.fulltimestatus.Equals(true));
            if (collection.Get(8).Equals("true,false"))
                studentz = studentz.Where(stud => stud.enrolled.Equals(true));
            return View(studentz);
        }

        //
        // GET: /Student/Details/5

        public ActionResult Details(string id)
        {
            try
            {
                if (id != null)
                {
                    student student = db.students.Include("program").Single(s => s.studentid == id);
                    return View(student);
                }
                else
                {
                    return RedirectToAction("Index", "Student");
                }
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("Index", "Student");
            }
        }

        //
        // GET: /Student/Create

        public ActionResult Create()
        {
            ViewBag.campus = new SelectList(db.campus, "cname", "cname");
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

            ViewBag.campus = new SelectList(db.campus, "cname", "cname", student.campus);
            ViewBag.programcode = new SelectList(db.programs, "programcode", "programname", student.programcode);
            return View(student);
        }
        
        //
        // GET: /Student/Edit/5

        public ActionResult Edit(string id)
        {
            try
            {
                if (id != null)
                {
                    student student = db.students.Single(s => s.studentid == id);
                    ViewBag.campus = new SelectList(db.campus, "cname", "cname", student.campus);
                    ViewBag.programcode = new SelectList(db.programs, "programcode", "programname", student.programcode);
                    return View(student);
                }
                else
                {
                    return RedirectToAction("Index", "Student");
                }
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("Index", "Student");
            }
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
            ViewBag.campus = new SelectList(db.campus, "cname", "cname", student.campus);
            ViewBag.programcode = new SelectList(db.programs, "programcode", "programname", student.programcode);
            return View(student);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}