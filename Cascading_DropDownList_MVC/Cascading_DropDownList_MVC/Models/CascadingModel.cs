using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cascading_DropDownList_MVC.Models
{
    public class CascadingModel
    {
        public CascadingModel()
        {
            this.Countries = new List<SelectListItem>();
            this.States = new List<SelectListItem>();
            this.Cities = new List<SelectListItem>();
            this.Details = new List<SelectListItem>();
        }

        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> States { get; set; }
        public List<SelectListItem> Cities { get; set; }

        public List<SelectListItem> Details { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }

        public string Name { get; set; }
        public string Pno { get; set; }
    }
}