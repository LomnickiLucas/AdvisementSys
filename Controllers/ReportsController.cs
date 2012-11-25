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
            try
            {
                employee employee = db.employees.Single(emp => emp.employeeid == User.Identity.Name);
                String[] Programs = { "All", "Information Systems Security", "Telecommunications Technology", "Computer Programmer", "Systems Analyst", "Network Engineering", "Software Engineering" };
                CampusReportModel model = new CampusReportModel() { Program = "All", ProgList = Programs, StartDate = db.issues.OrderBy(i => i.date).First().date, EndDate = db.issues.OrderByDescending(i => i.date).First().date, User = employee.fname + " " + employee.lname };
                return View(model);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //
        // POST: /Reports/Create

        [HttpPost]
        public ActionResult CampusIssueReport(CampusReportModel model)
        {
            try
            {
                    employee employee = db.employees.Single(emp => emp.employeeid == User.Identity.Name);
                    String[] Programs = { "All", "Information Systems Security", "Telecommunications Technology", "Computer Programmer", "Systems Analyst", "Network Engineering", "Software Engineering" };
                    model.ProgList = Programs;
                    model.User = employee.fname + " " + employee.lname;
                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        //
        // GET: /Reports/StudentIssueReport

        public ActionResult StudentIssueReport()
        {
            try
            {
                employee employee = db.employees.Single(emp => emp.employeeid == User.Identity.Name);
                IEnumerable<student> students = db.students;
                List<AutoCompletePOCO> StudentID = new List<AutoCompletePOCO>();
                foreach (student stud in students)
                {
                    AutoCompletePOCO poco = new AutoCompletePOCO()
                    {
                        value = stud.fname + " " + stud.lname + " (" + stud.studentid + ")",
                        Label = stud.fname + " " + stud.lname + " (" + stud.studentid + ")",
                        Email = stud.email,
                        Role = "Student"
                    };
                    StudentID.Add(poco);
                }
                StudentReportModel model = new StudentReportModel() { StudentID = "", Student = "", StartDate = db.issues.OrderBy(i => i.date).First().date, EndDate = db.issues.OrderByDescending(i => i.date).First().date, StudID = StudentID, User = employee.fname + " " + employee.lname + " (" + employee.employeeid + ")" };
                return View(model);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //
        // POST: /Reports/StudentIssueReport

        [HttpPost]
        public ActionResult StudentIssueReport(StudentReportModel model)
        {
            try
            {
                    employee employee = db.employees.Single(emp => emp.employeeid == User.Identity.Name);
                    IEnumerable<student> students = db.students;
                    List<AutoCompletePOCO> StudentID = new List<AutoCompletePOCO>();
                    foreach (student stud in students)
                    {
                        AutoCompletePOCO poco = new AutoCompletePOCO()
                        {
                            value = stud.fname + " " + stud.lname + " (" + stud.studentid + ")",
                            Label = stud.fname + " " + stud.lname + " (" + stud.studentid + ")",
                            Email = stud.email,
                            Role = "Student"
                        };
                        StudentID.Add(poco);
                    }

                    model.Student = model.StudentID.Remove(0, model.StudentID.Length - 10);
                    model.Student = model.Student.Remove(model.Student.Length - 1);

                    model.StudID = StudentID;
                    model.User = employee.fname + " " + employee.lname + " (" + employee.employeeid + ")";
                    return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        //
        // GET: /Reports/StudentIssueReport

        public ActionResult AdvisorIssueReport()
        {
            try
            {
                employee employee = db.employees.Single(emp => emp.employeeid == User.Identity.Name);

                IEnumerable<employee> employees = db.employees.Where(emp => emp.role == "Student Advisor");
                List<AutoCompletePOCO> EmployeeID = new List<AutoCompletePOCO>();
                foreach (employee emp in employees)
                {
                    AutoCompletePOCO poco = new AutoCompletePOCO()
                    {
                        value = emp.fname + " " + emp.lname + " (" + emp.employeeid + ")",
                        Label = emp.fname + " " + emp.lname + " (" + emp.employeeid + ")",
                        Email = emp.email,
                        Role = emp.role
                    };
                    EmployeeID.Add(poco);
                }

                AdvisorReportModel model = new AdvisorReportModel() { EmpID = employee.fname + " " + employee.lname + " (" + employee.employeeid + ")", Employee = employee.employeeid, EmployeeID = EmployeeID, startDate = db.issues.OrderBy(i => i.date).First().date, endDate = db.issues.OrderByDescending(i => i.date).First().date, User = employee.fname + " " + employee.lname + " (" + employee.employeeid + ")" };
                return View(model);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //
        // POST: /Reports/StudentIssueReport

        [HttpPost]
        public ActionResult AdvisorIssueReport(AdvisorReportModel model)
        {
            try
            {
                    employee employee = db.employees.Single(emp => emp.employeeid == User.Identity.Name);
                    IEnumerable<employee> employees = db.employees.Where(emp => emp.role == "Student Advisor");
                    List<AutoCompletePOCO> EmployeeID = new List<AutoCompletePOCO>();
                    foreach (employee emp in employees)
                    {
                        AutoCompletePOCO poco = new AutoCompletePOCO()
                        {
                            value = emp.fname + " " + emp.lname + " (" + emp.employeeid + ")",
                            Label = emp.fname + " " + emp.lname + " (" + emp.employeeid + ")",
                            Email = emp.email,
                            Role = emp.role
                        };
                        EmployeeID.Add(poco);
                    }

                    model.Employee = model.EmpID.Remove(0, model.EmpID.Length - 10);
                    model.Employee = model.Employee.Remove(model.Employee.Length - 1);

                    model.EmployeeID = EmployeeID;
                    model.User = employee.fname + " " + employee.lname + " (" + employee.employeeid + ")";
                    return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }
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
