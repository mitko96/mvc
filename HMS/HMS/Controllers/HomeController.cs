using HMS.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (ISession session = Database.OpenSession())

            {
               /* var spec = " ";
                var city = " ";
                Session["D_spec_admin"] = spec;
                Session["doc_city_admin"] = city;*/
                var ass = session.Query<DoctorsModel>().Where(x=>x.Id_d == Convert.ToInt32(Session["id_pat_dr"])).FirstOrDefault();
                if (Convert.ToString(Session["U_user_level"]) != "Admin")
                {
                    Session["doc_city"] = ass.D_city;
                    Session["D_first_name"] = ass.D_first_name;
                    Session["D_family_name"] = ass.D_family_name;
                    Session["D_phone"] = ass.D_phone;
                    Session["Id_d"] = ass.Id_d;
                    Session["D_spec"] = ass.D_spec;
                    ViewData["doctors"] = session.Query<DoctorsModel>().Where(x => x.D_city == Convert.ToString(Session["doc_city"]) && x.D_spec == Convert.ToString(Session["D_spec"])).Count();
                    ViewData["patients"] = session.Query<PatientsModel>().Where(c => c.Id_doc == Convert.ToInt32(Session["id_pat_dr"])).Count();
                }
                return View();
            }
        }

        public ActionResult Cal()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            using (ISession session = Database.OpenSession())
            {
                var events = session.Query<CalendarModel>().Where(x => x.Fk_doc == Convert.ToInt32(Session["id_pat_dr"])).ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public JsonResult SaveEvent(CalendarModel e)
        {
            var status = false;
            using (ISession session = Database.OpenSession())
            {
                if (e.EventID > 0)
                {
                    //Update the event
                    var v = session.Query<CalendarModel>().Where(a => a.EventID == e.EventID).FirstOrDefault();
                    if (v != null)
                    { 
                        
                        v.Fk_doc = e.Fk_doc;
                        v.Subject = e.Subject;
                        v.Start_date = e.Start_date;
                        v.End_date = e.End_date;
                        v.Description = e.Description;
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            session.SaveOrUpdate(v);
                            transaction.Commit();
                        }
                    }
                }
                else
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.SaveOrUpdate(e);
                        transaction.Commit();
                    }
                }
                
                status = true;
                
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            using (ISession session = Database.OpenSession())
            {
                var v = session.Query<CalendarModel>().Where(a => a.EventID == eventID).FirstOrDefault();
                if (v != null)
                {
                    using (ITransaction trans = session.BeginTransaction())
                    {
                        session.Delete(v);
                        trans.Commit();
                    }
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}