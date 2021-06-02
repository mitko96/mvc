using HMS.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using HMS.Util;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace HMS.Controllers
{
    public class DoctorsController : Controller
    {
        // GET: Doctors
        public ActionResult Index(int? i, string search)
        {


            using (ISession session = Database.OpenSession())

            {
               
                var doctor = session.Query<DoctorsModel>().Where(x=>x.D_family_name.Contains(search) || x.D_specialty.Contains(search) || search==null && x.D_city == Convert.ToString(Session["doc_city"]) && x.D_spec== Convert.ToString(Session["d_spec"])).ToList().ToPagedList(i ?? 1,15);
                if(Convert.ToString(Session["U_user_level"]) == "Admin")
                    {
                    var sd = session.Query<DoctorsModel>().Where(x => x.D_family_name.Contains(search) || x.D_specialty.Contains(search) || search == null).ToList().ToPagedList(i ?? 1, 15);
                    return View(sd);

                }
                    return View(doctor);

            }
        }
        ISession session = Database.OpenSession();
        public ActionResult IndexUser()
        {
           
                var pre = session.Query<UsersModel>().Where(b => b.Id_pat_dr == Convert.ToInt32(Session["user"])).ToList();


                return PartialView("IndexUser", pre);
            
        }
        // GET: Doctors/Details/5
        public ActionResult Details(int id)
        {
            using (ISession session = Database.OpenSession())

            {

                DoctorsModel doc = new DoctorsModel();


                doc = session.Query<DoctorsModel>().Where(b => b.Id_d == id)
                    .FirstOrDefault();
                Session["user"] = doc.Id_d;
                return View(doc);

            }
        }

        // GET: Doctors/Create

        public string MD5Encryption(string encryptionText)
       {

            // We have created an instance of the MD5CryptoServiceProvider class.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //We converted the data as a parameter to a byte array.
            byte[] array = Encoding.UTF8.GetBytes(encryptionText);
            //We have calculated the hash of the array.
            array = md5.ComputeHash(array);
            //We created a StringBuilder object to store hashed data.
            StringBuilder sb = new StringBuilder();
            //We have converted each byte from string into string type.

            foreach (byte ba in array)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }

            //We returned the hexadecimal string.
            return sb.ToString();
        }

    



    [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Create(CC model, HttpPostedFileBase image1)
        {
            try
            {
                ISession session = Database.OpenSession();
                session.BeginTransaction();
                DoctorsModel pat = model.DocModel;
                if (Convert.ToString(Session["U_user_level"]) != "Admin")
                {
                    
                    pat.D_city = Convert.ToString(Session["doc_city"]);
                    pat.D_spec = Convert.ToString(Session["D_spec"]);
                    if (image1 != null)
                    {
                        pat.D_picture = new byte[image1.ContentLength];
                        image1.InputStream.Read(pat.D_picture, 0, image1.ContentLength);
                    }

                    session.Save(pat);
                }
                else
                {

                    // Doc.D_city = Convert.ToString(Session["doc_city"]);
                    // pat.D_spec = Convert.ToString(Session["D_spec"]);
                    if (image1 != null)
                    {
                        pat.D_picture = new byte[image1.ContentLength];
                        image1.InputStream.Read(pat.D_picture, 0, image1.ContentLength);
                    }

                    session.Save(pat);
                }
            
                UsersModel us = model.UserModel;
                us.Id_pat_dr = pat.Id_d;
                us.U_user_level = "Doctor";
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

        
        // POST: Doctors/Create


        // GET: Patient/Edit/5

        public ActionResult Edit(int id)
        {
            using (ISession session = Database.OpenSession())

            {

                DoctorsModel doc = new DoctorsModel();

                doc = session.Query<DoctorsModel>().Where(b => b.Id_d == id).FirstOrDefault();


                ViewBag.SubmitAction = "Save";
                return View(doc);
            }
        }



        // POST: Patient/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, HttpPostedFileBase image1, FormCollection collection)
        {
            using (ISession session = Database.OpenSession())

            {
                try
                {

                    DoctorsModel db = new DoctorsModel();
                    db.Id_d = id;
                    db.D_city = collection["D_city"].ToString();
                    db.D_egn = collection["D_egn"].ToString();
                    db.D_email = collection["D_email"].ToString();
                    db.D_family_name = collection["D_family_name"].ToString();
                    db.D_first_name = collection["D_first_name"].ToString();
                    db.D_gender = collection["D_gender"].ToString();
                    db.D_mid_name = collection["D_mid_name"].ToString();
                    db.D_phone = collection["D_phone"].ToString();
                    db.D_specialty = collection["D_specialty"].ToString();
                    db.D_room = collection["D_room"].ToString();
                    db.D_floor = collection["D_floor"].ToString();
                    db.D_spec = collection["D_spec"].ToString();

                    if (image1 != null)
                    {
                        db.D_picture = new byte[image1.ContentLength];
                        image1.InputStream.Read(db.D_picture, 0, image1.ContentLength);

                    }

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.SaveOrUpdate(db);
                        transaction.Commit();
                    }

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
        }

        public ActionResult EditProfile()
        {


            using (ISession session = Database.OpenSession())

            {

                var doctor = session.Query<DoctorsModel>().Where(x => x.Id_d == Convert.ToInt32(Session["id_pat_dr"])).ToList();

                return View(doctor);

            }
        }
        // GET: Doctors/Delete/5
        public ActionResult Delete(int id)
        {
            using (ISession session = Database.OpenSession())

            {
                DoctorsModel doc = new DoctorsModel();

                doc = session.Query<DoctorsModel>().Where(b => b.Id_d == id).FirstOrDefault();

                ViewBag.SubmitAction = "Confirm delete";
                return View("Delete", doc);
            }
        }

        // POST: Patient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            using (ISession session = Database.OpenSession())

            {
                DoctorsModel doc = session.Get<DoctorsModel>(id);

                using (ITransaction trans = session.BeginTransaction())
                {
                    session.Delete(doc);
                    trans.Commit();
                }

                return RedirectToAction("Index");
            }
        }
    }
}
