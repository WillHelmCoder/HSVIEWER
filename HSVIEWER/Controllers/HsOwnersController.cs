using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using HSVIEWER.Data;

namespace HSVIEWER.Controllers
{
    public class HsOwnersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HsOwnersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HsOwners
        public async Task<IActionResult> Index(string Id)
        {
            ViewData["pipe"]  = Id;
            return View(await _context.HsOwners.ToListAsync());
        }

        // GET: HsOwners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hsOwner = await _context.HsOwners
                .FirstOrDefaultAsync(m => m.HsOwnerId == id);
            if (hsOwner == null)
            {
                return NotFound();
            }

            return View(hsOwner);
        }

        // GET: HsOwners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HsOwners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HsOwnerId,eMail,Name,WorkOrderId")] HsOwner hsOwner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hsOwner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hsOwner);
        }

        // GET: HsOwners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hsOwner = await _context.HsOwners.FindAsync(id);
            if (hsOwner == null)
            {
                return NotFound();
            }
            return View(hsOwner);
        }

        // POST: HsOwners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HsOwnerId,eMail,Name,WorkOrderId")] HsOwner hsOwner)
        {
            if (id != hsOwner.HsOwnerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hsOwner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HsOwnerExists(hsOwner.HsOwnerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hsOwner);
        }

        // GET: HsOwners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hsOwner = await _context.HsOwners
                .FirstOrDefaultAsync(m => m.HsOwnerId == id);
            if (hsOwner == null)
            {
                return NotFound();
            }

            return View(hsOwner);
        }

        // POST: HsOwners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hsOwner = await _context.HsOwners.FindAsync(id);
            _context.HsOwners.Remove(hsOwner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HsOwnerExists(int id)
        {
            return _context.HsOwners.Any(e => e.HsOwnerId == id);
        }
    }
}
