using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace DiskInventory.Controllers
{
    public class DiskHasBorrowerController : Controller
    {
        private disk_inventorytwContext context { get; set; }
        public DiskHasBorrowerController(disk_inventorytwContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var diskhasborrowers = context.DiskHasBorrowers.
                Include(d => d.Cd).
                Include(b=> b.Borrower).ToList(); // add sort here later
            return View(diskhasborrowers);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Borrowers = context.Borrowers.OrderBy(b => b.BorrowerLname).ToList();
            ViewBag.Disks = context.Disks.OrderBy(d => d.CdName).ToList();
            DiskHasBorrower newcheckout = new DiskHasBorrower();
            newcheckout.BorrowedDate = DateTime.Today;
            return View("Edit", newcheckout);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Borrowers = context.Borrowers.OrderBy(b => b.BorrowerFname).ToList();
            ViewBag.Disks = context.Disks.OrderBy(d => d.CdName).ToList();
            var diskHasBorrower= context.DiskHasBorrowers.Find(id);
            return View(diskHasBorrower);
        }

        [HttpPost]
        public IActionResult Edit(DiskHasBorrower diskHasborrower)
        {
            if (ModelState.IsValid)
            {
                if (diskHasborrower.CdId == 0 && diskHasborrower.BorrowerId == 0)  
                {
                    context.DiskHasBorrowers.Add(diskHasborrower);
                }
                else                       
                {
                    context.DiskHasBorrowers.Update(diskHasborrower);
                }
                context.SaveChanges();
                return RedirectToAction("Index", "DiskHasBorrower");
            }
            else
            {
                ViewBag.Action = (diskHasborrower.DiskBorrower == 0) ? "Add" : "Edit";
                ViewBag.Borrowers = context.Borrowers.OrderBy(b => b.BorrowerFname).ToList();
                ViewBag.Disks = context.Disks.OrderBy(d => d.CdName).ToList();
                return View(diskHasborrower);
            }
        }

    }
}
