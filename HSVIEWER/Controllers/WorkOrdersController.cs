using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using HSVIEWER.Data;
using HSVIEWER.Services;

namespace HSVIEWER.Controllers
{
    public class WorkOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMainService _mainService;

        public WorkOrdersController(ApplicationDbContext context, IMainService mainService)
        {
            _context = context;
            _mainService = mainService;
        }

        // GET: WorkOrders
        public async Task<IActionResult> Index()
        {

            if (await _mainService.CheckIsProcessing() == true)
            {
                return Redirect("/home/processing");
            }
            var applicationDbContext = _context.WorkOrder.Include(w => w.Owner);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: WorkOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _context.WorkOrder
                .Include(w => w.Owner)
                .FirstOrDefaultAsync(m => m.WorkOrderId == id);
            if (workOrder == null)
            {
                return NotFound();
            }

            return View(workOrder);
        }

        // GET: WorkOrders/Create
        public IActionResult Create()
        {
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerId");
            return View();
        }

        // POST: WorkOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkOrderId,WorkOrderDate,OwnerId")] WorkOrder workOrder)
        {
            if (ModelState.IsValid)
            {
                workOrder.WorkOrderDate = DateTime.Now;
                
                _context.Add(workOrder);
                await _context.SaveChangesAsync();

                return Redirect("/home/processing");
            }
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerId", workOrder.OwnerId);
            return View(workOrder);
        }

        // GET: WorkOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _context.WorkOrder.FindAsync(id);
            if (workOrder == null)
            {
                return NotFound();
            }
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerId", workOrder.OwnerId);
            return View(workOrder);
        }

        // POST: WorkOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkOrderId,WorkOrderDate,OwnerId")] WorkOrder workOrder)
        {
            if (id != workOrder.WorkOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkOrderExists(workOrder.WorkOrderId))
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
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerId", workOrder.OwnerId);
            return View(workOrder);
        }

        // GET: WorkOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _context.WorkOrder
                .Include(w => w.Owner)
                .FirstOrDefaultAsync(m => m.WorkOrderId == id);
            if (workOrder == null)
            {
                return NotFound();
            }

            return View(workOrder);
        }

        // POST: WorkOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workOrder = await _context.WorkOrder.FindAsync(id);
            _context.WorkOrder.Remove(workOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkOrderExists(int id)
        {
            return _context.WorkOrder.Any(e => e.WorkOrderId == id);
        }
    }
}
