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
    }
}
