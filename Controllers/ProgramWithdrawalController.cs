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

        public ViewResult Index()
        {
            var applicationfortermorcompleteprogramwithdrawals = db.applicationForTermOrCompleteProgramWithdrawals.Include("issue");
            return View(applicationfortermorcompleteprogramwithdrawals.ToList());
        }

        //
        // GET: /ProgramWithdrawal/Details/5

        public ActionResult Details(Guid id)
        {
            try
            {
                if (id != null)
                {
                    applicationForTermOrCompleteProgramWithdrawal applicationfortermorcompleteprogramwithdrawal = db.applicationForTermOrCompleteProgramWithdrawals.Single(a => a.withdrawid == id);
                    issue issue = db.issues.Single(i => i.issueid == applicationfortermorcompleteprogramwithdrawal.issueid);
                    student student = db.students.Include("program").Single(s => s.studentid == issue.studentid);
                    DetailsProgramWithdrawalModel Model = new DetailsProgramWithdrawalModel() { _applicationForTermOrCompleteProgramWithdrawal = applicationfortermorcompleteprogramwithdrawal, _student = student };
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
        // GET: /ProgramWithdrawal/Create

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
 
        public ActionResult Delete(Guid id)
        {
            applicationForTermOrCompleteProgramWithdrawal applicationfortermorcompleteprogramwithdrawal = db.applicationForTermOrCompleteProgramWithdrawals.Single(a => a.withdrawid == id);
            return View(applicationfortermorcompleteprogramwithdrawal);
        }

        //
        // POST: /ProgramWithdrawal/Delete/5

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