using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using Cascading_DropDownList_MVC.Models;
using System.Data;

namespace Cascading_DropDownList_MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            CascadingModel model = new CascadingModel();
            model.Countries = PopulateDropDown("SELECT distinct CountryId, CountryName FROM Salesrep", "CountryName", "CountryId");
            return View(model);
        }

        [HttpPost]
        public JsonResult AjaxMethod(string type, int value)
        {
            CascadingModel model = new CascadingModel();
            switch (type)
            {
                case "CountryId":
                    model.States = PopulateDropDown("SELECT StateId, StateName FROM Salesrep WHERE CountryId = " + value, "StateName", "StateId");
                    break;
                case "StateId":
                    model.Cities = PopulateDropDown("SELECT CityId, CityName FROM Salesrep  WHERE StateId = " + value, "CityName", "CityId");
                    break;
            }
            return Json(model);
        }

        [HttpPost]
        public ActionResult Index(int countryId, int stateId, int cityId)
        {
            CascadingModel model = new CascadingModel();
            model.Countries = PopulateDropDown("SELECT CountryId, CountryName FROM Salesrep", "CountryName", "CountryId");
            model.States = PopulateDropDown("SELECT StateId, StateName FROM Salesrep WHERE CountryId = " + countryId, "StateName", "StateId");
            model.Cities = PopulateDropDown("SELECT CityId, CityName FROM Salesrep WHERE StateId = " + stateId, "CityName", "CityID");
            model.Details = GetDetails("select Name,Pno from Salesrep where CountryId=" + countryId + "and StateId=" + stateId + "and CityId=" + cityId,"Name","Pno");
            return View(model);
        }

        private static List<SelectListItem> PopulateDropDown(string query, string textColumn, string valueColumn)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = sdr[textColumn].ToString(),
                                Value = sdr[valueColumn].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return items;
        }
        public static List<SelectListItem> GetDetails(string query,string pname,string ppno)
        {
            List<SelectListItem> pdetails = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            pdetails.Add(new SelectListItem
                            {
                                Text = sdr[pname].ToString(),
                                Value = sdr[ppno].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return pdetails;
        }

    }
}