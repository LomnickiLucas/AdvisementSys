using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdvisementSys.Models;

namespace AdvisementSys.Controllers
{
    public class ReportsController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Reports/Index

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Reports/

        public ActionResult CampusIssueReport()
        {
            String[] Programs = { "All", "Information Systems Security", "Telecommunications Technology", "Computer Programmer", "Systems Analyst", "Network Engineering", "Software Engineering" };
            CampusReportModel model = new CampusReportModel() { Program = "All", ProgList = Programs, StartDate = db.issues.OrderBy(i => i.date).First().date, EndDate = db.issues.OrderByDescending(i => i.date).First().date };
            return View(model);
        }

        //
        // POST: /Reports/Create

        [HttpPost]
        public ActionResult CampusIssueReport(CampusReportModel model)
        {
            String[] Programs = { "All", "Information Systems Security", "Telecommunications Technology", "Computer Programmer", "Systems Analyst", "Network Engineering", "Software Engineering" };
            model.ProgList = Programs;
            return View(model);
        }

        //
        // GET: /Reports/StudentIssueReport

        public ActionResult StudentIssueReport()
        {
            IEnumerable<student> students = db.students;
            List<String> studentID = new List<String>();
            foreach (student stud in students)
            {
                studentID.Add(stud.studentid);
            }
            StudentReportModel model = new StudentReportModel() { StudentID = "", StartDate = db.issues.OrderBy(i => i.date).First().date, EndDate = db.issues.OrderByDescending(i => i.date).First().date, StudID = studentID };
            return View(model);
        }

        //
        // POST: /Reports/StudentIssueReport

        [HttpPost]
        public ActionResult StudentIssueReport(StudentReportModel model)
        {
                IEnumerable<student> students = db.students;
                List<String> studentID = new List<String>();
                foreach (student stud in students)
                {
                    studentID.Add(stud.studentid);
                }
                model.StudID = studentID;
                return View(model);
        }

        //
        // GET: /Reports/StudentIssueReport

        public ActionResult AdvisorIssueReport()
        {
            IEnumerable<employee> employees = db.employees;
            List<String> employeeID = new List<String>();
            foreach (employee emp in employees)
            {
                employeeID.Add(emp.employeeid);
            }
            AdvisorReportModel model = new AdvisorReportModel() { EmpID = User.Identity.Name, EmployeeID = employeeID };
            return View(model);
        }

        //
        // POST: /Reports/StudentIssueReport

        [HttpPost]
        public ActionResult AdvisorIssueReport(AdvisorReportModel model)
        {
            IEnumerable<employee> employees = db.employees;
            List<String> employeeID = new List<String>();
            foreach (employee emp in employees)
            {
                employeeID.Add(emp.employeeid);
            }
            model.EmployeeID = employeeID;
            return View(model);
        }

        //
        // GET: /Reports/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Reports/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Reports/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Reports/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Reports/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Reports/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Reports/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
