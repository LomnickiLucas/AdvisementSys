using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdvisementSys.Models;

namespace AdvisementSys.Controllers
{
    public class AdvisorSearchController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /AdvisorSearch/

        /// <summary>
        /// populates search advisor view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try
            {
                IEnumerable<employee> employee = db.employees;
                List<EmployeePOCO> employees = new List<EmployeePOCO>();
                List<String> EmployeeID = new List<string>();
                List<String> Roles = new List<string>();
                Roles.Add("Any");
                foreach(employee emp in employee)
                {
                    EmployeeID.Add(emp.employeeid);

                    EmployeePOCO poco = new EmployeePOCO()
                    {
                        EmployeeID = emp.employeeid,
                        fName = emp.fname,
                        lName = emp.lname,
                        PhoneNum = emp.phonenum,
                        Email = emp.email,
                        Faculty = emp.faculty,
                        Role = emp.role
                    };

                    employees.Add(poco);

                    Boolean flag = false;

                    foreach(String role in Roles)
                    {
                        if (emp.role == role)
                        {
                            flag = true;
                            break;
                        }
                    }

                    if (!flag)
                        Roles.Add(emp.role);
                }
                IEnumerable<faculty> faculty = db.faculties;
                List<String> facName = new List<string>();
                facName.Add("Any");
                foreach (faculty fac in faculty)
                {
                    facName.Add(fac.fname);
                }
                IndexAdvisorSearchModel model = new IndexAdvisorSearchModel() { _EmployeeID = EmployeeID, _employee = employees, _role = Roles, _faculty = facName };
                return View(model);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        /// <summary>
        /// post back for index just filters down the results based off of the search criteria
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(IndexAdvisorSearchModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IEnumerable<employee> employee = db.employees;
                    List<EmployeePOCO> employees = new List<EmployeePOCO>();
                    List<String> EmployeeID = new List<string>();
                    List<String> Roles = new List<string>();
                    Roles.Add("Any");
                    foreach (employee emp in employee)
                    {
                        EmployeeID.Add(emp.employeeid);

                        Boolean flag = false;

                        foreach (String role in Roles)
                        {
                            if (emp.role == role)
                            {
                                flag = true;
                                break;
                            }
                        }

                        if (!flag)
                            Roles.Add(emp.role);
                    }
                    IEnumerable<faculty> faculty = db.faculties;
                    List<String> facName = new List<string>();
                    facName.Add("Any");
                    foreach (faculty fac in faculty)
                    {
                        facName.Add(fac.fname);
                    }

                    if (model.fname != null)
                        employee = employee.Where(emp => emp.fname.Trim().ToUpper().Contains(model.fname.Trim().ToUpper()));
                    if (model.lname != null)
                        employee = employee.Where(emp => emp.lname.Trim().ToUpper().Contains(model.lname.Trim().ToUpper()));
                    if (model.EmployeeID != null)
                        employee = employee.Where(emp => emp.employeeid.Trim().ToUpper().Contains(model.EmployeeID.Trim().ToUpper()));
                    if (model.email != null)
                        employee = employee.Where(emp => emp.email.Trim().ToUpper().Contains(model.email.Trim().ToUpper()));
                    if (model.faculty != "Any")
                        employee = employee.Where(emp => emp.faculty.Trim().ToUpper().Contains(model.faculty.Trim().ToUpper()));
                    if (model.role != "Any")
                        employee = employee.Where(emp => emp.role.Trim().ToUpper().Contains(model.role.Trim().ToUpper()));

                    foreach (employee emp in employee)
                    {
                        EmployeePOCO poco = new EmployeePOCO()
                        {
                            EmployeeID = emp.employeeid,
                            fName = emp.fname,
                            lName = emp.lname,
                            PhoneNum = emp.phonenum,
                            Email = emp.email,
                            Faculty = emp.faculty,
                            Role = emp.role
                        };

                        employees.Add(poco);
                    }

                    model._employee = employees;
                    model._faculty = facName;
                    model._role = Roles;
                    model._EmployeeID = EmployeeID;
                }

                return View(model);

            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        /// <summary>
        /// just pushes the advisor details up to a view so it may be viewed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(String id)
        {
            try
            {
                employee model = db.employees.Single(emp => emp.employeeid == id);
                return View(model);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
