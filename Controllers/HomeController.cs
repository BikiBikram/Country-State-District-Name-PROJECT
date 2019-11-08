using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using w.Models;

namespace w.Controllers
{
	public class HomeController : Controller
	{
		wwwEntities db = new wwwEntities();
		public ActionResult ListOfState()
		{
			return View();
		}
		[HttpGet]
		public ActionResult Addingstate()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Addingstate(STATE sts)
		{
			if(ModelState.IsValid)
			{
				db.STATEs.Add(sts);
				db.SaveChanges();
			}
				
			return View();
		}


	}
}