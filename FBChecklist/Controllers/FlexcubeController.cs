using FBChecklist.Common;
using FBChecklist.Exceptions;
using FBChecklist.Models;
using FBChecklist.Services;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Management;
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
            //Fetch Connection String from Web.config    
            var FCUBS = ConfigurationManager.ConnectionStrings[1];

            var writable = typeof(ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            writable.SetValue(FCUBS, false);

            //Replace Conn string 
            var username = disksService.GetSuperUsername(Helpers.parameters.Fcubs);
            var password = disksService.GetSuperUserPassword(Helpers.parameters.Fcubs);
            var database = disksService.GetAuthority(Helpers.parameters.Fcubs);

            FCUBS.ConnectionString = "user id=" + username + ";password=" + password + ";data source=" + database + "";
            var connstring = FCUBS.ConnectionString;

            List<Branch> branches = new List<Branch>();


            try
            {
                using (OracleConnection connp = new OracleConnection(connstring))
                {
                    connp.Open();
                    OracleCommand cmd1 = new OracleCommand("select * from STTM_CORE_BRANCH_STATUS", connp);

                    OracleDataReader dr1 = cmd1.ExecuteReader();


                    while (dr1.Read() && dr1.HasRows)
                    {
                        var model = new Branch();

                        model.TimeLevel = Convert.ToInt32(dr1["TIME_LEVEL"]);
                        model.EOCStage = Convert.ToString(dr1["EOC_STAGE"]);
                        model.BranchCode = Convert.ToInt32(dr1["BRANCH_CODE"]);
                        branches.Add(model);

                        foreach (var branch in branches)
                        {
                            using (SqlConnection conn = new SqlConnection(Helpers.DatabaseConnect))
                            {

                                conn.Open();
                                SqlCommand cmdp = new SqlCommand("SaveBranchStatus", conn);
                                cmdp.CommandType = CommandType.StoredProcedure;
                                cmdp.Parameters.AddWithValue("@BranchCode", branch.BranchCode);
                                cmdp.Parameters.AddWithValue("@TimeLevel", branch.TimeLevel);
                                cmdp.Parameters.AddWithValue("@EOCStage", branch.EOCStage);
                                cmdp.Parameters.AddWithValue("@RunDate", DateTime.Now);
                                cmdp.Parameters.AddWithValue("@ApplicationId", Helpers.parameters.Fcubs);
                                cmdp.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }

                    connp.Close();
                }
            }

            catch (Exception ex)
            {
                ExceptionLogger.SendErrorToText(ex);
            }




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
