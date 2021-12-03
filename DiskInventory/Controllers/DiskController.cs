using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;
using Microsoft.EntityFrameworkCore;

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
                    //context.Disks.Add(disk);
                    //@cdName char(60), @releaseDate date, @artistID int, @genreID int, @statusID int, @diskTypeID int
                    context.Database.ExecuteSqlRaw("execute sp_ins_disk @p0, @p1, @p2, @p3, @p4, @p5", parameters: new[] { disk.CdName, disk.ReleaseDate.ToString(), disk.ArtistId.ToString(), disk.GenreId.ToString(), disk.StatusId.ToString(), disk.DiskTypeId.ToString() });
                }
                else
                {
                    //context.Disks.Update(disk); 
                    //@cdID int, @cdName char(60), @releaseDate date, @artistID int, @genreID int, @statusID int, @diskTypeID int
                    context.Database.ExecuteSqlRaw("execute sp_upd_disk @p0, @p1, @p2, @p3, @p4, @p5, @p6", parameters: new[] { disk.CdId.ToString(), disk.CdName, disk.ReleaseDate.ToString(), disk.ArtistId.ToString(), disk.GenreId.ToString(), disk.StatusId.ToString(), disk.DiskTypeId.ToString() });
                }
                //context.SaveChanges();
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
            //context.Disks.Remove(disk);
            context.Database.ExecuteSqlRaw("execute sp_del_disk @p0", parameters: new[] { disk.CdId.ToString() });
            //context.SaveChanges();
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
            disk.ReleaseDate = DateTime.Today; // todays date default
            return View("Edit", disk);
        }

    }

}
