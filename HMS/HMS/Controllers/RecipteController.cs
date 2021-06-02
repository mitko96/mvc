using HMS.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class RecipteController : Controller
    {
        ISession session = Database.OpenSession();
        // GET: Recipte
        public ActionResult Index()
        {
            var pre = session.Query<RecipteModel>().Where(b => b.Fk_pats == Convert.ToInt32(Session["id_pat"])).OrderBy(x => x.End_date_recipe).ToList();


            return PartialView("Index", pre);
        }
        public ActionResult View(int id, CC model)
        {

            RecipteModel prescription = model.reciptemodel;
            prescription = session.Query<RecipteModel>().Single(c => c.Fk_pats == id);
           // Session["name_recipe"] = prescription.Name_recipe;
            Session["start_date_recipe"] = prescription.Start_date_recipe;
            //Session["end_date_recipe"] = prescription.End_date_recipe;
            Session["number_protocol"] = prescription.Number_protocol;
            //Session["firstDd"] = prescription.Recipe;
            Session["secondDd"] = prescription.Recipe_type_;
            Session["ssecondDd"] = prescription.Recipe_typeof;
            Session["description_recipe"] = prescription.Description_recipe;
            return View(prescription);
        }
        private List<RecipteModel> GetFileList()
        {
            List<RecipteModel> DetList = new List<RecipteModel>();


            DetList = session.Query<RecipteModel>().ToList();
            return DetList;
        }
        // GET: Recipte/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Recipte/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dropdown()
        {
            // add your code after post method is done
            var selectedOption = Request["eventId"];
            return Content(selectedOption);
        }

        [HttpGet]
        public JsonResult GetDropdownList(int? value)
        {
            List<string> yourdata = new List<string>();
            List<string> yourdata1 = new List<string>();
            if (value == 1)
            {
                yourdata.Add("Бяла рецепта");
                yourdata.Add("Зелена рецепта");
                yourdata.Add("Жълта рецепта");
                return Json(new { data = yourdata }, JsonRequestBehavior.AllowGet);
            }
           else if (value == 2)
            {
                yourdata.RemoveAt(0);
                yourdata1.Add("Нормална рецепта");
                yourdata1.Add("Хронична рецепта");
                return Json(new { data = yourdata1 }, JsonRequestBehavior.AllowGet);
            }
            else if(value == 3)
            {
                yourdata.Add("Бяла рецепта");
                yourdata.Add("Зелена рецепта");
                yourdata.Add("Жълта рецепта");
                return Json(new { data = yourdata }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
            }




        }
        // POST: Recipte/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
          
                string ds = " ";

                DateTime ct = DateTime.Now.Date;
                DateTime future = ct.AddMonths(5);
                RecipteModel rec = new RecipteModel();
                rec.Name_recipe = collection["name_recipe"].ToString();
                rec.Number_protocol = collection["number_protocol"].ToString();
                rec.Start_date_recipe = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                rec.Recipe = collection["firstDd"].ToString();
                if (collection["secondDd"] == null)
                {
                    rec.Recipe_type_ = ds;
                }
                else
                {
                    rec.Recipe_type_ = collection["secondDd"].ToString();
                }

                rec.End_date_recipe = rec.Start_date_recipe.AddMonths(6);
                if (collection["ssecondDd"] == null)
                {
                    rec.Recipe_typeof = ds;
                }
                else
                {
                    rec.Recipe_typeof = collection["ssecondDd"].ToString();
                }
                if (collection["firstDd"].ToString() == "Рецепта" && collection["secondDd"].ToString() == "Бяла рецепта" && collection["ssecondDd"].ToString() == "Еднократна")
                {
                    rec.End_date_recipe = DateTime.Now.Date.AddMonths(6);
                }
                else if (collection["firstDd"].ToString() == "Рецепта" && collection["secondDd"].ToString() == "Бяла рецепта" && collection["ssecondDd"].ToString() == "Многократна")
                {
                    rec.End_date_recipe = DateTime.Now.Date.AddMonths(6);
                }
                else if (collection["firstDd"].ToString() == "Рецепта" && collection["secondDd"].ToString() == "Зелена рецепта")
                {
                    rec.End_date_recipe = DateTime.Now.Date.AddDays(7);
                }
                else if (collection["firstDd"].ToString() == "Рецепта" && collection["secondDd"].ToString() == "Жълта рецепта")
                {
                    rec.End_date_recipe = DateTime.Now.Date.AddDays(7);
                }
                else if (collection["firstDd"].ToString() == "По здравна каса" && collection["secondDd"].ToString() == "Нормална рецепта")
                {
                    rec.End_date_recipe = DateTime.Now.Date.AddMonths(1);
                }
                else if (collection["firstDd"].ToString() == "По здравна каса" && collection["secondDd"].ToString() == "Хронична рецепта")
                {
                    rec.End_date_recipe = DateTime.Now.Date.AddMonths(3);
                }
                else if (collection["firstDd"].ToString() == "По здравна каса" && collection["secondDd"].ToString() == "Хронична рецепта" && collection["ssecondDd"].ToString() == "А отрязък")
                {
                    rec.End_date_recipe = DateTime.Now.Date.AddDays(15);
                }
                else if (collection["firstDd"].ToString() == "По здравна каса" && collection["secondDd"].ToString() == "Хронична рецепта" && collection["ssecondDd"].ToString() == "Б отрязък")
                {
                    rec.End_date_recipe = DateTime.Now.Date.AddDays(45);
                }
                else if (collection["firstDd"].ToString() == "По здравна каса" && collection["secondDd"].ToString() == "Хронична рецепта" && collection["ssecondDd"].ToString() == "С отрязък")
                {
                    rec.End_date_recipe = DateTime.Now.Date.AddDays(75);
                }
                rec.Fk_pats = Convert.ToInt32(Session["id_pat"]);
                rec.Description_recipe = collection["description_recipe"].ToString();
                session.Save(rec);


                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(rec);
                    transaction.Commit();
                }
            
            return RedirectToAction("Index", "Patient");
        }

        // GET: Recipte/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Recipte/Edit/5
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

        // GET: Recipte/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Recipte/Delete/5
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
