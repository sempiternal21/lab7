using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using lab7.Models;

namespace lab7.Controllers
{
    public class HomeController : Controller
	{
		private MusiciansDBContext db;
		public HomeController(MusiciansDBContext DB)
		{
			db = DB;
		}
		public IActionResult MusiciansPage()
		{
			// Готовим данные для представления
			ViewData["Title"] = "Музыканты";
			var musicians = db.Musicians.ToList();
			// Передаем управление "Представлению"
			return View("MusiciansPage", musicians);
		}
		public IActionResult BandsPage()
		{
			ViewData["Title"] = "Группы";
			var bands = db.Bands.ToList();
			// Передаем управление "Представлению"
			return View("BandsPage", bands);
		}
		public IActionResult InstrumentsPage()
		{
			// Готовим данные для представления
			ViewData["Title"] = "Инструменты";
			var instruments = db.Instruments.ToList();
			// Передаем управление "Представлению"
			return View("InstrumentsPage", instruments);
		}
		[HttpGet]
		public IActionResult CreateMusician()
		{
			Musician musician = new Musician();
			return View(musician);
		}
		[HttpPost]
		public IActionResult CreateMusician(Musician player)
		{
			if (ModelState.IsValid)
			{
				try
				{
					db.Musicians.Add(player);
					db.SaveChanges();
					return MusiciansPage();
				}
				catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
				{
					return View("InsertError", e.InnerException.Message == null ? e.Message : e.InnerException.Message);
				}

			}
			else 
				return View();
		}
		public IActionResult DeleteMusician(Musician musician)
		{
			try
			{
				db.Remove(musician);
				db.SaveChanges();
			}
			catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
			{
				return View("InsertError", e.InnerException.Message == null ? e.Message : e.InnerException.Message);
			}
			return MusiciansPage();
		}
		public IActionResult EditMusician(Musician musician)
		{
			return View("EditMusician", musician);
		}

		[HttpGet]
		public IActionResult CreateBand()
		{
			Band band = new Band();
			return View(band);
		}
		[HttpPost]
		public IActionResult CreateBand(Band band)
		{
			if (ModelState.IsValid)
			{
				try
				{
					db.Bands.Add(band);
					db.SaveChanges();
					return BandsPage();
				}
				catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
				{
					return View("InsertError", e.InnerException.Message == null ? e.Message : e.InnerException.Message);
				}
				catch (InvalidOperationException e)
				{
					return View("InsertError", e.Message);
				}

			}
			else
				return View();
		}
		public IActionResult DeleteBand(Band band)
		{
			try
			{
				db.Remove(band);
				db.SaveChanges();
			}
			catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
			{
				return View("InsertError", e.InnerException.Message == null ? e.Message : e.InnerException.Message);
			}
			return BandsPage();
		}
		[HttpGet]
		public IActionResult EditMusiciansBand(string bandName)
		{
			return View("EditBand", db.Bands.First(p => p.Name == bandName));
		}
		[HttpGet]
		public IActionResult EditBand(Band band)
		{
			return View("EditBand", band);
		}
		[HttpPost]
		public IActionResult EditBandPost(Band band)
		{
			try
			{
				db.Update(band);
				db.SaveChanges();
				return BandsPage();
			}
			catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
			{
				return View("InsertError", e.InnerException == null ? e.Message : e.InnerException.Message); ;
			}
			return View("EditBand", band);
		}

		[HttpGet]
		public IActionResult CreateInstrument()
		{
			Instrument instrument = new Instrument();
			return View(instrument);
		}
		[HttpPost]
		public IActionResult CreateInstrument(Instrument instrument)
		{
			if (ModelState.IsValid)
			{
				try
				{
					db.Instruments.Add(instrument);
					db.SaveChanges();
					return InstrumentsPage();
				}
				catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
				{
					return View("InsertError", e.InnerException.Message == null ? e.Message : e.InnerException.Message);
				}
				catch (InvalidOperationException e)
				{
					return View("InsertError", e.Message);
				}

			}
			else
				return View();
		}
		public IActionResult DeleteInstrument(Instrument instrument)
		{
			try
			{
				db.Remove(instrument);
				db.SaveChanges();
			}
			catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
			{
				return View("InsertError", e.InnerException.Message == null ? e.Message : e.InnerException.Message);
			}
			return InstrumentsPage();
		}
		[HttpGet]
		public IActionResult EditMusiciansInstrument(string instrumentName)
		{
			return View("EditInstrument", db.Instruments.First(p => p.Name == instrumentName));
		}
		[HttpGet]
		public IActionResult EditInstrument(Instrument instrument)
		{
			return View("EditInstrument", instrument);
		}
		[HttpPost]
		public IActionResult EditInstrumentPost(Instrument instrument)
		{
			try
			{
				db.Instruments.Update(instrument);
				db.SaveChanges();
				return InstrumentsPage();
			}
			catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
			{
				return View("InsertError", e.InnerException == null ? e.Message : e.InnerException.Message); ;
			}
		}

		public IActionResult Metrica()
		{
			var instr = db.Instruments.ToList();
			var musicians = db.Musicians.ToList();
			ViewData["CountInstr"] = instr.Count();
			ViewData["CountMusicans"] = musicians.Count();
			ViewData["Count"] = instr.Count() + musicians.Count();

			var GrCountry = musicians.GroupBy(x => x.Band).ToList();
			ViewData["GrCountry"] = GrCountry;

			var GrInstr = musicians.GroupBy(x => x.Instrument).ToList();
			ViewData["GrInstr"] = GrInstr;

			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
