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
                string returnedDate = diskHasborrower.ReturnedDate.ToString();
                returnedDate = (returnedDate == "") ? null : diskHasborrower.ReturnedDate.ToString();
                if (diskHasborrower.DiskBorrower == 0)  
                {
                    //context.DiskHasBorrowers.Add(diskHasborrower);
                    context.Database.ExecuteSqlRaw("execute sp_ins_diskHasBorrower @p0, @p1, @p2, @p3", parameters: new[] { diskHasborrower.BorrowerId.ToString(), diskHasborrower.CdId.ToString(), diskHasborrower.BorrowedDate.ToString(), returnedDate});
                }
                else                       
                {
                    // context.DiskHasBorrowers.Update(diskHasborrower);
                    // 	@diskBorrower int, @borrowerID int, @cdID int, @borrowedDate date, @returnedDate date = NULL
                    context.Database.ExecuteSqlRaw("execute sp_upd_diskHasBorrower @p0, @p1, @p2, @p3, @p4", parameters: new[] { diskHasborrower.DiskBorrower.ToString(), diskHasborrower.BorrowerId.ToString(), diskHasborrower.CdId.ToString(), diskHasborrower.BorrowedDate.ToString(), returnedDate });
                }
                //context.SaveChanges();
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
