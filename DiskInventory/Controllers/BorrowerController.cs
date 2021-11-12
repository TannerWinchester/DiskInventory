using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;

namespace DiskInventory.Controllers
{
    public class BorrowerController : Controller
    {
        private disk_inventorytwContext context { get; set; }
        public BorrowerController(disk_inventorytwContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            List<Borrower> borrower = context.Borrowers.OrderBy(b => b.BorrowerId).ThenBy(b => b.BorrowerFname).ThenBy(b => b.BorrowerLname).ThenBy(b => b.BorrowerPhoneNum).ToList();
            return View(borrower);
        }
    }
}
