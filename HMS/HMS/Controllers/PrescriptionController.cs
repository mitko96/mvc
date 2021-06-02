using HMS.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class PrescriptionController : Controller
    {
        ISession session = Database.OpenSession();
        // GET: Prescription
        public ActionResult Index()
        {
            var pre = session.Query<PrescriptionModel>().Where(b=>b.Id_patient == Convert.ToInt32(Session["id_pat"])).OrderBy(x => x.Date_pre).ToList();


            return PartialView("Index", pre);
        }

        // GET: Prescription/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult View(int id, CC model)
        {

            PrescriptionModel prescription = model.PresModel;
             prescription = session.Query<PrescriptionModel>().Single(c => c.Id_prescription == id);
            Session["medicial_test"] = prescription.medicial_test;
            Session["Medicine"] = prescription.Medicine;
            Session["result"] = prescription.result;
            Session["Duration"] = prescription.Duration;
            Session["Date_pre"] = prescription.Date_pre;
            return View(prescription);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]  
            public FileResult DownLoadFile(int id)
        {


            List<PrescriptionModel> ObjFiles = GetFileList();

            var FileById = (from FC in ObjFiles
                            where FC.Id_prescription.Equals(id)
                            select new { FC.Medicine,FC.file_name, FC.File }).ToList().FirstOrDefault();

            return File(FileById.File, FileById.file_name, FileById.file_name);

        }

        private List<PrescriptionModel> GetFileList()
        {
            List<PrescriptionModel> DetList = new List<PrescriptionModel>();


            DetList = session.Query<PrescriptionModel>().ToList();
            return DetList;
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, HttpPostedFileBase file)
        {
          
                PrescriptionModel pat = new PrescriptionModel();
                pat.Medicine = collection["Medicine"].ToString();
                pat.Duration = collection["Duration"].ToString();
                pat.Date_pre = DateTime.Now.Date;
                pat.Id_doctors = Convert.ToInt32(Session["Id_d"]);
                pat.Id_patient = Convert.ToInt32(Session["id_pat"]);
                pat.medicial_test = collection["medicial_test"].ToString();
                pat.result = collection["result"].ToString();
                if (file != null)
                {
                    byte[] bytes;
                    using (BinaryReader br = new BinaryReader(file.InputStream))
                    {
                        bytes = br.ReadBytes(file.ContentLength);
                    }

                    pat.file_name = Path.GetFileName(file.FileName);
                    pat.File = bytes;
                }
                session.Save(pat);


                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(pat);
                    transaction.Commit();
                }
            
            return RedirectToAction("Index", "Patient");

        }


        // GET: Prescription/Create


        // GET: Prescription/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Prescription/Edit/5
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

        // GET: Prescription/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Prescription/Delete/5
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
