using FBChecklist.Common;
using FBChecklist.Exceptions;
using FBChecklist.Services;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace FBChecklist.Controllers
{
    public class FlexcubeController : Controller
    {
        private AppEntities db = new AppEntities();

        private DisksService disksService;


        public FlexcubeController(DisksService disksService)
        {
            this.disksService = disksService;

        }
        public FlexcubeController() : this(new DisksService())
        {
            //the framework calls this
        }

        public ActionResult CheckTimeLevel()
        {        

            return View();
        }



        // GET: Flexcube
        public ActionResult Index()
        {
            return View();
        }

        // GET: Flexcube/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Flexcube/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Flexcube/Create
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

        // GET: Flexcube/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Flexcube/Edit/5
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

        // GET: Flexcube/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Flexcube/Delete/5
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
