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

        public ViewResult Details(Guid id)
        {
            applicationForTermOrCompleteProgramWithdrawal applicationfortermorcompleteprogramwithdrawal = db.applicationForTermOrCompleteProgramWithdrawals.Single(a => a.withdrawid == id);
            return View(applicationfortermorcompleteprogramwithdrawal);
        }

        //
        // GET: /ProgramWithdrawal/Create

        public ActionResult Create()
        {
            ViewBag.issueid = new SelectList(db.issues, "issueid", "studentid");
            return View();
        } 

        //
        // POST: /ProgramWithdrawal/Create

        [HttpPost]
        public ActionResult Create(applicationForTermOrCompleteProgramWithdrawal applicationfortermorcompleteprogramwithdrawal)
        {
            if (ModelState.IsValid)
            {
                applicationfortermorcompleteprogramwithdrawal.withdrawid = Guid.NewGuid();
                db.applicationForTermOrCompleteProgramWithdrawals.AddObject(applicationfortermorcompleteprogramwithdrawal);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.issueid = new SelectList(db.issues, "issueid", "studentid", applicationfortermorcompleteprogramwithdrawal.issueid);
            return View(applicationfortermorcompleteprogramwithdrawal);
        }
        
        //
        // GET: /ProgramWithdrawal/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            applicationForTermOrCompleteProgramWithdrawal applicationfortermorcompleteprogramwithdrawal = db.applicationForTermOrCompleteProgramWithdrawals.Single(a => a.withdrawid == id);
            ViewBag.issueid = new SelectList(db.issues, "issueid", "studentid", applicationfortermorcompleteprogramwithdrawal.issueid);
            return View(applicationfortermorcompleteprogramwithdrawal);
        }

        //
        // POST: /ProgramWithdrawal/Edit/5

        [HttpPost]
        public ActionResult Edit(applicationForTermOrCompleteProgramWithdrawal applicationfortermorcompleteprogramwithdrawal)
        {
            if (ModelState.IsValid)
            {
                db.applicationForTermOrCompleteProgramWithdrawals.Attach(applicationfortermorcompleteprogramwithdrawal);
                db.ObjectStateManager.ChangeObjectState(applicationfortermorcompleteprogramwithdrawal, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.issueid = new SelectList(db.issues, "issueid", "studentid", applicationfortermorcompleteprogramwithdrawal.issueid);
            return View(applicationfortermorcompleteprogramwithdrawal);
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