using HMS.Models;
using HMS.Util;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HMS.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UsersModel u)
        {

            using (ISession session = Database.OpenSession())
            {



                
                        var v = session.Query<UsersModel>().Where(a => a.U_username == u.U_username).FirstOrDefault();
                /* if (v == null)
         {
             ModelState.AddModelError("", "Invalid username or password");
         }
         else
         {
                     if (Hashing.ValidatePassword(u.U_password, v.U_password))
                     {
                         Session["Id_u"] = v.Id_u;
                         Session["id_pat_dr"] = v.Id_pat_dr;
                         Session["U_username"] = u.U_username;
                         Session["U_user_level"] = v.U_user_level;
                         return RedirectToAction("Index", "Home");
                     }
         }

         return View();*/
                if (u.U_username == null && u.U_password == null)
                {
                    ModelState.AddModelError("", "Въведете потребителско име и парола");
                }
                
                else if (u.U_username == null && u.U_password != null)
                {
                    ModelState.AddModelError("", "Въведете потребителско име");
                }
                else if (u.U_username != null && u.U_password == null)
                {
                    ModelState.AddModelError("", "Въведете парола");
                }
                if ((v != null && u.U_password != null))
                {
                    if (Hashing.ValidatePassword(u.U_password, v.U_password))
                    {
                        Session["Id_u"] = v.Id_u;
                        Session["id_pat_dr"] = v.Id_pat_dr;
                        Session["U_username"] = u.U_username;
                        Session["U_user_level"] = v.U_user_level;
                        return RedirectToAction("Index", "Home");
                    }
                    

                }

                return View();
            }
        }


        //public ActionResult Login()
        //{
        //    return View();
        //}



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(string email, string password)
        //{
        //    using (ISession session = Database.OpenSession())
        //    {
        //        if (ModelState.IsValid)
        //        {



        //            var data = session.Query<UsersModel>().Where(s => s.U_username.Equals(email) && s.U_password.Equals(password)).ToList();
        //            if (data.Count() > 0)
        //            {
        //                //add session
        //                Session["Email"] = data.FirstOrDefault().U_username;
        //                Session["idUser"] = data.FirstOrDefault().Id_u;
        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                ViewBag.error = "Login failed";
        //                return RedirectToAction("Login");
        //            }
        //        }
        //        return View();
        //    }
        //}


        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Users");

        }
        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
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

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
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

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
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
