using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;

namespace DiskInventory.Controllers
{
    public class DiskController : Controller
    { 
        private disk_inventorytwContext context { get; set; }
        public DiskController(disk_inventorytwContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            List<Disk> disk = context.Disks.OrderBy(d => d.CdId).ThenBy(d => d.CdName).ThenBy(d => d.ReleaseDate).ThenBy(d => d.ArtistId).ThenBy(d => d.GenreId).ThenBy(d => d.StatusId).ThenBy(d => d.DiskTypeId).ToList();
            return View(disk);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.DiskTypes = context.DiskTypes.OrderBy(t => t.Description).ToList();
            ViewBag.Statuses = context.Statuses.OrderBy(s => s.Description).ToList();
            ViewBag.GenreTypes = context.GenreTypes.OrderBy(g => g.Description).ToList();
            ViewBag.ArtistIds = context.Artists.OrderBy(d => d.ArtistId).ToList();
            var disk = context.Disks.Find(id);
            return View(disk);
        }
        [HttpPost]
        public IActionResult Edit(Disk disk)
        {
            if (ModelState.IsValid)
            {
                if (disk.CdId == 0)
                {
                    context.Disks.Add(disk);
                }
                else
                {
                    context.Disks.Update(disk); 
                }
                context.SaveChanges();
                return RedirectToAction("Index", "Disk");
            }
            else
            {
                ViewBag.Action = (disk.CdId == 0) ? "Add" : "Edit";
                ViewBag.DiskTypes = context.DiskTypes.OrderBy(t => t.Description).ToList();
                ViewBag.GenreTypes = context.GenreTypes.OrderBy(g => g.Description).ToList();
                ViewBag.Statuses = context.Statuses.OrderBy(s => s.Description).ToList();
                ViewBag.ArtistIds = context.Artists.OrderBy(d => d.ArtistId).ToList();
                return View(disk);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var disk = context.Disks.Find(id);
            return View(disk);
        }
        [HttpPost]
        public IActionResult Delete(Disk disk)
        {
            context.Disks.Remove(disk);
            context.SaveChanges();
            return RedirectToAction("Index", "Disk");
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.DiskTypes = context.DiskTypes.OrderBy(t => t.Description).ToList();
            ViewBag.Statuses = context.Statuses.OrderBy(s => s.Description).ToList();
            ViewBag.GenreTypes = context.GenreTypes.OrderBy(g => g.Description).ToList();
            ViewBag.ArtistIds = context.Artists.OrderBy(d => d.ArtistId).ToList();
            Disk disk = new Disk();
            return View("Edit", disk);
        }

    }

}
