using HMS.Models;
using Microsoft.AspNet.Identity;
using NHibernate;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using HMS.Util;

namespace HMS.Controllers
{
    public class PatientController : Controller
    {
        ISession session = Database.OpenSession();
        // GET: Patient

        public ActionResult Index(int? i, string search)
        {
            add_patients s = new add_patients();
            var use = session.Query<UsersModel>().FirstOrDefault();
            var patient = session.Query<DoctorsModel>().FirstOrDefault();
            var mod = session.Query<PatientsModel>().Where(x => x.P_egn.Contains(search) || search == null).ToList();

            // var contact = session.Query<ViewModel>().Where(x => x.id_doctor == Convert.ToInt32(Session["id_pat_dr"]));
            //  return View(contact.ToList());
            return View(mod);




        }
        public ActionResult Add_index()
        {

           

            var contact =  session.Query<ViewModel>();
            return View(contact.ToList());
        }

        public ActionResult IndexAll(int? i, string search)
        {

            var use = session.Query<UsersModel>().FirstOrDefault();
            var patient = session.Query<DoctorsModel>().FirstOrDefault();
            var mod = session.Query<PatientsModel>().Where(x => x.P_egn.Contains(search) || search == null).ToList().ToPagedList(i ?? 1, 10);


            return View(mod);

        }

        public ActionResult IndexUser()
        {

            var pre = session.Query<UsersModel>().Where(b => b.Id_pat_dr == Convert.ToInt32(Session["id_pat"])).ToList();


            return PartialView("IndexUser", pre);

        }

        // GET: Patient/Details/5
        public ActionResult Details(int id, CC model)
        {

              PatientsModel pat = new PatientsModel();
              PrescriptionModel pre = new PrescriptionModel();

              pat = session.Query<PatientsModel>().Where(b => b.Id_p == id)
              .FirstOrDefault();
              Session["id_pat"] = pat.Id_p;
              Session["P_family_name"] = pat.P_family_name;
              Session["P_first_name"] = pat.P_first_name;
              Session["P_gender"] = pat.P_gender;
              Session["P_phone"] = pat.P_phone;
              return View(pat);

          /*  ViewModel pat = new ViewModel();
            PrescriptionModel pre = new PrescriptionModel();

            pat = session.Query<ViewModel>().Where(b => b.Id_p == id)
            .FirstOrDefault();
            Session["id_pat"] = pat.Id_p;
            Session["P_family_name"] = pat.P_family_name;
            Session["P_first_name"] = pat.P_first_name;
            Session["P_gender"] = pat.P_gender;
            Session["P_phone"] = pat.P_phone;
            return View(pat); */



        }

        // GET: Patient/Create


        // POST: Patient/Create
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Create(CC model)
        {
            try
            {
                ISession session = Database.OpenSession();
                session.BeginTransaction();
               PatientsModel pat = model.PatientModel;
                pat.P_datetime = DateTime.Now.Date;
                session.Save(pat);
                UsersModel us = model.UserModel;
                us.Id_pat_dr = pat.Id_p;
                us.U_user_level = "Patient";
                us.U_password = Hashing.HashPaswword(us.U_password);
                session.Save(us);
                session.Transaction.Commit();

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                return View();
            }


        }

        // GET: Patient/Edit/5
        public ActionResult Edit(int id)
        {
            

                PatientsModel pat = new PatientsModel();

                pat = session.Query<PatientsModel>().Where(b => b.Id_p == id).FirstOrDefault();


                ViewBag.SubmitAction = "Save";
                return View(pat);
            
        }

        // POST: Patient/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            
                PatientsModel pat = new PatientsModel();
                pat.Id_p = id;
                pat.Id_doc = Convert.ToInt32(Session["Id_d"]);
               // pat.Id_doc = id;
                pat.P_address = collection["P_address"].ToString();
                pat.P_blood_group = collection["P_blood_group"].ToString();
                pat.P_egn = collection["P_egn"].ToString();
                pat.P_family_name = collection["P_family_name"].ToString();
                pat.P_first_name = collection["P_first_name"].ToString();
                pat.P_gender = collection["P_gender"].ToString();
                pat.P_mid_name = collection["P_mid_name"].ToString();
                pat.P_phone = collection["P_phone"].ToString();
                pat.P_datetime = Convert.ToDateTime(collection["P_datetime"].ToString());
            pat.email_pat = collection["email_pat"].ToString();
            /*  add_patients add = new add_patients();
              add.id_patient = pat.Id_p;
              add.id_doctor = Convert.ToInt32(Session["Id_d"]);*/

            using (ITransaction transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(pat);
               // session.SaveOrUpdate(add);
                transaction.Commit();
                }
          //  Session["id_patients_add"] = add.id_patient;
           // Session["id_doctors_add"] = add.id_doctor;
            return RedirectToAction("Index");
            
        }

        // GET: Patient/Delete/5
        public ActionResult Delete(int id)
        {
            PatientsModel pat = new PatientsModel();

            pat = session.Query<PatientsModel>().Where(b => b.Id_p == id).FirstOrDefault();

            ViewBag.SubmitAction = "Confirm delete";
            return View("Delete", pat);
        }

        // POST: Patient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            PatientsModel pat = session.Get<PatientsModel>(id);

            using (ITransaction trans = session.BeginTransaction())
            {
                session.Delete(pat);
                trans.Commit();
            }

            return RedirectToAction("Index");
        }
    }
}
