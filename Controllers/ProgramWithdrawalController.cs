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
    public class ProgramWithdrawalController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /ProgramWithdrawal/
        /// <summary>
        /// Not in use
        /// </summary>
        /// <returns></returns>
        public ViewResult Index()
        {
            var applicationfortermorcompleteprogramwithdrawals = db.applicationForTermOrCompleteProgramWithdrawals.Include("issue");
            return View(applicationfortermorcompleteprogramwithdrawals.ToList());
        }

        //
        // GET: /ProgramWithdrawal/Details/5
        /// <summary>
        /// Grabs a single entry in the database to populate the View 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(Guid id)
        {
            try
            {
                if (id != null)
                {
                    applicationForTermOrCompleteProgramWithdrawal applicationfortermorcompleteprogramwithdrawal = db.applicationForTermOrCompleteProgramWithdrawals.Single(a => a.withdrawid == id);
                    issue issue = db.issues.Single(i => i.issueid == applicationfortermorcompleteprogramwithdrawal.issueid);
                    student student = db.students.Include("program").Single(s => s.studentid == issue.studentid);
                    DetailsProgramWithdrawalModel Model = new DetailsProgramWithdrawalModel() { _applicationForTermOrCompleteProgramWithdrawal = applicationfortermorcompleteprogramwithdrawal, _student = student, _note = db.notes.Include("employee").Where(note => note.formid == id).OrderByDescending(f => f.dates), _employee = db.employees.Single(e => e.employeeid == User.Identity.Name), _date = DateTime.Now };
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Details(DetailsProgramWithdrawalModel _model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    Guid? id = _model._NewNote.formid;
                    _model._NewNote.noteid = Guid.NewGuid();
                    _model._NewNote.employeeid = User.Identity.Name;
                    _model._NewNote.dates = DateTime.Now;
                    _model._NewNote.controller = "ProgramWithdrawal";
                    db.notes.AddObject(_model._NewNote);
                    db.SaveChanges();

                    applicationForTermOrCompleteProgramWithdrawal applicationfortermorcompleteprogramwithdrawal = db.applicationForTermOrCompleteProgramWithdrawals.Single(a => a.withdrawid == id);
                    issue issue = db.issues.Single(i => i.issueid == applicationfortermorcompleteprogramwithdrawal.issueid);
                    student student = db.students.Include("program").Single(s => s.studentid == issue.studentid);
                    DetailsProgramWithdrawalModel Model = new DetailsProgramWithdrawalModel() { _applicationForTermOrCompleteProgramWithdrawal = applicationfortermorcompleteprogramwithdrawal, _student = student, _note = db.notes.Include("employee").Where(note => note.formid == id).OrderByDescending(f => f.dates), _employee = db.employees.Single(e => e.employeeid == User.Identity.Name), _date = DateTime.Now };
                    return View(Model);

                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Issue");
                }
            }

            return View(_model);
        }

        //
        // GET: /ProgramWithdrawal/Create
        /// <summary>
        /// Grabs required  data for View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Create(Guid id)
        {
            try
            {
                if (id != null)
                {
                    applicationForTermOrCompleteProgramWithdrawal applicationForTermOrCompleteProgramWithdrawal = new applicationForTermOrCompleteProgramWithdrawal() { issueid = id, date = DateTime.Now };
                    issue issue = db.issues.Single(i => i.issueid == id);
                    student student = db.students.Include("program").Single(s => s.studentid == issue.studentid);
                    CreateProgramWithdrawalModel Model = new CreateProgramWithdrawalModel() { _applicationForTermOrCompleteProgramWithdrawal = applicationForTermOrCompleteProgramWithdrawal, _student = student };
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
        // POST: /ProgramWithdrawal/Create
        /// <summary>
        /// Submits Create to Database
        /// </summary>
        /// <param name="_CreateProgramWithdrawalModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(CreateProgramWithdrawalModel _CreateProgramWithdrawalModel)
        {
            if (ModelState.IsValid)
            {
                _CreateProgramWithdrawalModel._applicationForTermOrCompleteProgramWithdrawal.withdrawid = Guid.NewGuid();
                db.applicationForTermOrCompleteProgramWithdrawals.AddObject(_CreateProgramWithdrawalModel._applicationForTermOrCompleteProgramWithdrawal);
                db.SaveChanges();
                return RedirectToAction("Details/" + _CreateProgramWithdrawalModel._applicationForTermOrCompleteProgramWithdrawal.withdrawid);  
            }

            return View(_CreateProgramWithdrawalModel);
        }
        
        //
        // GET: /ProgramWithdrawal/Edit/5
        /// <summary>
        /// Ensures Edit View has the single entry from the Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(Guid id)
        {
            try
            {
                if (id != null)
                {
                    applicationForTermOrCompleteProgramWithdrawal applicationfortermorcompleteprogramwithdrawal = db.applicationForTermOrCompleteProgramWithdrawals.Single(a => a.withdrawid == id);
                    issue issue = db.issues.Single(i => i.issueid == applicationfortermorcompleteprogramwithdrawal.issueid);
                    student student = db.students.Include("program").Single(s => s.studentid == issue.studentid);
                    EditProgramWithdrawalModel Model = new EditProgramWithdrawalModel() { _applicationForTermOrCompleteProgramWithdrawal = applicationfortermorcompleteprogramwithdrawal, _student = student };
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
        // POST: /ProgramWithdrawal/Edit/5
        /// <summary>
        /// Edit is Submited to the Database
        /// </summary>
        /// <param name="_EditProgramWithdrawalModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(EditProgramWithdrawalModel _EditProgramWithdrawalModel)
        {
            if (ModelState.IsValid)
            {
                db.applicationForTermOrCompleteProgramWithdrawals.Attach(_EditProgramWithdrawalModel._applicationForTermOrCompleteProgramWithdrawal);
                db.ObjectStateManager.ChangeObjectState(_EditProgramWithdrawalModel._applicationForTermOrCompleteProgramWithdrawal, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details/" + _EditProgramWithdrawalModel._applicationForTermOrCompleteProgramWithdrawal.withdrawid);
            }

            return View(_EditProgramWithdrawalModel);
        }

        //
        // GET: /ProgramWithdrawal/Delete/5
        /// <summary>
        /// not in use, prob never will be
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(Guid id)
        {
            applicationForTermOrCompleteProgramWithdrawal applicationfortermorcompleteprogramwithdrawal = db.applicationForTermOrCompleteProgramWithdrawals.Single(a => a.withdrawid == id);
            return View(applicationfortermorcompleteprogramwithdrawal);
        }

        //
        // POST: /ProgramWithdrawal/Delete/5
        /// <summary>
        /// Future use (GOT VISION)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            applicationForTermOrCompleteProgramWithdrawal applicationfortermorcompleteprogramwithdrawal = db.applicationForTermOrCompleteProgramWithdrawals.Single(a => a.withdrawid == id);
            db.applicationForTermOrCompleteProgramWithdrawals.DeleteObject(applicationfortermorcompleteprogramwithdrawal);
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