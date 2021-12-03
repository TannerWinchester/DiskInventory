using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DiskInventory.Models;

//      Name:            Date:          Description
// Tanner Winchester   12/6/2021        added stored procs
//
//


namespace DiskInventory.Controllers
{
    public class ArtistController : Controller
    {
        private disk_inventorytwContext context { get; set; }
        public ArtistController(disk_inventorytwContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            List<Artist> artists = context.Artists.OrderBy(a => a.ArtistName).ToList();
            return View(artists);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.Description).ToList();
            return View("Edit", new Artist());
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.Description).ToList();
            var artist = context.Artists.Find(id);
            return View(artist);
        }
        [HttpPost]
        public IActionResult Edit(Artist artist)
        {
            if (ModelState.IsValid)
            {
                if (artist.ArtistId == 0)   // means add the artist
                {
                    //context.Artists.Add(artist);
                    context.Database.ExecuteSqlRaw("execute sp_ins_artist @p0, @p1", parameters: new[] { artist.ArtistName, artist.ArtistTypeId.ToString() });
                }
                else                       // means update the artist
                {
                    //context.Artists.Update(artist);
                    context.Database.ExecuteSqlRaw("execute sp_upd_artist @p0, @p1, @p2", parameters: new[] { artist.ArtistId.ToString(), artist.ArtistName, artist.ArtistTypeId.ToString() });
                }
                //context.SaveChanges();
                return RedirectToAction("Index", "Artist");
            }
            else
            {
                ViewBag.Action = (artist.ArtistId == 0) ? "Add" : "Edit";
                ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.Description).ToList();
                return View(artist);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var artist = context.Artists.Find(id);
            return View(artist);
        }
        [HttpPost]
        public IActionResult Delete(Artist artist)
        {
            //context.Artists.Remove(artist);
            //context.SaveChanges();
            context.Database.ExecuteSqlRaw("execute sp_del_artist @p0", parameters: new[] { artist.ArtistId.ToString() });
            return RedirectToAction("index", "Artist");
        }
    }
}
