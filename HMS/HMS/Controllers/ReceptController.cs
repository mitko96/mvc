using HMS.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class ReceptController : Controller
    {
        ISession session = Database.OpenSession();
        // GET: Recept
        public ActionResult Index()
        {
            var pre = session.Query<RecipteModel>().Where(b => b.Fk_pats == Convert.ToInt32(Session["id_pat"])).OrderBy(x => x.End_date_recipe).ToList();


            return PartialView("Index", pre);
        }

        public ActionResult View(int id, CC model)
        {

            RecipteModel prescription = model.reciptemodel;
            prescription = session.Query<RecipteModel>().Single(c => c.Id_r == id);
            Session["name_recipe"] = prescription.Name_recipe;
            Session["number_protocol"] = prescription.Number_protocol;
            Session["firstDd"] = prescription.Recipe;
            Session["secondDd"] = prescription.Recipe_type_;
            Session["ssecondDd"] = prescription.Recipe_typeof;
            Session["description_recipe"] = prescription.Description_recipe;
            Session["Start_date_recipe"] = prescription.Start_date_recipe;
            Session["End_date_recipe"] = prescription.End_date_recipe;
            return View(prescription);
        }
        private List<RecipteModel> GetFileList()
        {
            List<RecipteModel> DetList = new List<RecipteModel>();


            DetList = session.Query<RecipteModel>().ToList();
            return DetList;
        }
        // GET: Recept/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Recept/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recept/Create
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

        // GET: Recept/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Recept/Edit/5
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

        // GET: Recept/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Recept/Delete/5
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
