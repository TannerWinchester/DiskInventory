﻿using Microsoft.AspNetCore.Mvc;
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
            List<Borrower> borrower = context.Borrowers.OrderBy(b => b.BorrowerLname).ThenBy(b => b.BorrowerFname).ThenBy(b => b.BorrowerId).ThenBy(b => b.BorrowerPhoneNum).ToList();
            return View(borrower);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Borrower());
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var borrower = context.Borrowers.Find(id);
            return View(borrower);
        }

        [HttpPost]
        public IActionResult Edit(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                if (borrower.BorrowerId == 0)   // means add the borrower
                {
                    context.Borrowers.Add(borrower);
                }
                else                       // means update the borrower
                {
                    context.Borrowers.Update(borrower);
                }
                context.SaveChanges();
                return RedirectToAction("Index", "Borrower");
            }
            else
            {
                ViewBag.Action = (borrower.BorrowerId == 0) ? "Add" : "Edit";
                return View(borrower);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var borrower = context.Borrowers.Find(id);
            return View(borrower);
        }
        [HttpPost]
        public IActionResult Delete(Borrower borrower)
        {
            context.Borrowers.Remove(borrower);
            context.SaveChanges();
            return RedirectToAction("index", "Borrower");
        }
    }
}
