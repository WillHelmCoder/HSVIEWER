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
    public class SelectPipelineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMainService _mainService;

        public SelectPipelineController(ApplicationDbContext context, IMainService mainService)
        {
            _context = context;
            _mainService = mainService;
        }

        // GET: SelectPipeline
        public async Task<IActionResult> Index(Int32 id)
        {
            if (await _mainService.CheckIsProcessing() == true)
            {
                return Redirect("/home/processing");
            }
            var applicationDbContext = _context.Pipelines.Include(p => p.WorkOrder).Where(w=>w.WorkOrder.OwnerId==id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SelectPipeline/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pipeline = await _context.Pipelines
                .Include(p => p.WorkOrder)
                .FirstOrDefaultAsync(m => m.PipelineId == id);
            if (pipeline == null)
            {
                return NotFound();
            }

            return View(pipeline);
        }

        // GET: SelectPipeline/Create
        public IActionResult Create()
        {
            ViewData["WorkOrderId"] = new SelectList(_context.WorkOrder, "WorkOrderId", "WorkOrderId");
            return View();
        }

        // POST: SelectPipeline/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PipelineId,Name,WorkOrderId,HsPipeLineId")] Pipeline pipeline)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pipeline);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkOrderId"] = new SelectList(_context.WorkOrder, "WorkOrderId", "WorkOrderId", pipeline.WorkOrderId);
            return View(pipeline);
        }

        // GET: SelectPipeline/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pipeline = await _context.Pipelines.FindAsync(id);
            if (pipeline == null)
            {
                return NotFound();
            }
            ViewData["WorkOrderId"] = new SelectList(_context.WorkOrder, "WorkOrderId", "WorkOrderId", pipeline.WorkOrderId);
            return View(pipeline);
        }

        // POST: SelectPipeline/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PipelineId,Name,WorkOrderId,HsPipeLineId")] Pipeline pipeline)
        {
            if (id != pipeline.PipelineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pipeline);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PipelineExists(pipeline.PipelineId))
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
            ViewData["WorkOrderId"] = new SelectList(_context.WorkOrder, "WorkOrderId", "WorkOrderId", pipeline.WorkOrderId);
            return View(pipeline);
        }

        // GET: SelectPipeline/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pipeline = await _context.Pipelines
                .Include(p => p.WorkOrder)
                .FirstOrDefaultAsync(m => m.PipelineId == id);
            if (pipeline == null)
            {
                return NotFound();
            }

            return View(pipeline);
        }

        // POST: SelectPipeline/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pipeline = await _context.Pipelines.FindAsync(id);
            _context.Pipelines.Remove(pipeline);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PipelineExists(int id)
        {
            return _context.Pipelines.Any(e => e.PipelineId == id);
        }
    }
}
