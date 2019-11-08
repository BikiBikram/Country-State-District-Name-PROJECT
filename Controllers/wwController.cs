using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using w.Models;


namespace w.Controllers
{
	public class wwController : Controller
	{
		wwwEntities db = new wwwEntities();
		// GET: ww
		public ActionResult Listcountry()
		{
			return View(db.COUNTRies.ToList());
		}

		[HttpGet]
		public ActionResult CreateCountry()
		{
			return View();
		}
		[HttpPost]
		public ActionResult CreateCountry(COUNTRY dist)
		{
			if(ModelState.IsValid)
			{
				db.COUNTRies.Add(dist);
				db.SaveChanges();
			}
			return View();
		}
		

		
		public JsonResult GetListState(int? cid)
		{
			db.Configuration.ProxyCreationEnabled = false;
			//ViewBag.CID = new SelectList(db.COUNTRies.ToList(), "CID", "CNAME");

		   List<STATE> selectlist = db.STATEs.Where(x => x.CID == cid).ToList();

			return Json(selectlist, JsonRequestBehavior.AllowGet);
		}
		[HttpGet]
		public ActionResult Addstate()
		{
			ViewBag.CID = new SelectList(db.COUNTRies.ToList(), "CID", "CNAME");

			return View();
		}

		[HttpPost]
		public ActionResult Addstate(STATE sts)
		{
			if (ModelState.IsValid)
			{
				db.STATEs.Add(sts);
				if (db.SaveChanges() > 0)
				{
					ViewBag.msg = "States added";
					ModelState.Clear();
				}
				else
				{
					ViewBag.msg = "states not added";
					ModelState.Clear();

				}

			}
			ViewBag.CID = new SelectList(db.COUNTRies.ToList(), "CID", "CNAME");

			return View();
		}
		
		[HttpGet]
		public ActionResult ListState()
		{
			ViewBag.CID = new SelectList(db.COUNTRies.ToList(), "CID", "CNAME");

			return View(db.STATEs.ToList());
		}


		public ActionResult AddDist()
		{
			ViewBag.SID = new SelectList(db.STATEs.ToList(), "SID", "SNAME");
			ViewBag.CID = new SelectList(db.COUNTRies.ToList(), "CID", "CNAME");

			return View();
		}
		[HttpPost]
		public ActionResult AddDist(DIST dst)
		{
			if (ModelState.IsValid)
			{
				db.DISTs.Add(dst);
				if (db.SaveChanges() > 0)
				{
					ViewBag.msg = "Dist added";
					ModelState.Clear();
				}
				else
				{
					ViewBag.msg = "Dist not added";
					ModelState.Clear();

				}

			}
			ViewBag.SID = new SelectList(db.STATEs.ToList(), "SID", "SNAME");
			ViewBag.CID = new SelectList(db.COUNTRies.ToList(), "CID", "CNAME");

			return View();
		}

		public ActionResult ListDist()
		{
			
			return View(db.STATEs.ToList());

		}

		public ActionResult AllList()
		{


			wwwEntities db = new wwwEntities();
			List<COUNTRY> countryname = db.COUNTRies.ToList();
			List<STATE> statename = db.STATEs.ToList();
			List<DIST> distname = db.DISTs.ToList();

			var jointable = (
							/*
											from country in countryname 

											join state in statename on country.CID equals state.CID into table1
											from state in table1.DefaultIfEmpty()

											join dist in distname on state.SID equals dist.DID into table2
											from dist in table2.DefaultIfEmpty()

				*/
							from c in countryname
							join s in statename on c.CID equals s.CID 
							
							join d in distname on s.SID equals d.SID 
							


								select new Multipleclass { countrydetails = c, statedetails = s ,distdetails=d }).ToList();


			  

			           return View(jointable.ToList());
		
		}
		
	}
}