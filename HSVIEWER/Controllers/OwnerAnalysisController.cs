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
    public class OwnerAnalysisController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMainService _mainService;

        public OwnerAnalysisController(ApplicationDbContext context, IMainService mainService)
        {
            _context = context;
            _mainService = mainService;
        }

        // GET: OwnerAnalysis
        public async Task<IActionResult> Index(string Id, Int32 wid)
        {
            if (await _mainService.CheckIsProcessing() == true)
            {
                return Redirect("/home/processing");
            }

            Int32 isProcessed = await _context.OwnerAnalysis.Where(w => w.OwnerName == Id).CountAsync();

            if (isProcessed != 0)
            {
                return View(await _context.OwnerAnalysis.ToListAsync());
            }
            else
            {
                await _mainService.SaveStageAnalysis(Id, wid);
            }
            


            return View(await _context.OwnerAnalysis.ToListAsync());
        }

        // GET: OwnerAnalysis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerAnalysis = await _context.OwnerAnalysis
                .FirstOrDefaultAsync(m => m.OwnerAnalysisId == id);
            if (ownerAnalysis == null)
            {
                return NotFound();
            }

            return View(ownerAnalysis);
        }

        // GET: OwnerAnalysis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OwnerAnalysis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OwnerAnalysisId,OwnerName,DealsNumber,DealAverage,OwnerPipelineValue")] OwnerAnalysis ownerAnalysis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ownerAnalysis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ownerAnalysis);
        }

        // GET: OwnerAnalysis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerAnalysis = await _context.OwnerAnalysis.FindAsync(id);
            if (ownerAnalysis == null)
            {
                return NotFound();
            }
            return View(ownerAnalysis);
        }

        // POST: OwnerAnalysis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OwnerAnalysisId,OwnerName,DealsNumber,DealAverage,OwnerPipelineValue")] OwnerAnalysis ownerAnalysis)
        {
            if (id != ownerAnalysis.OwnerAnalysisId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ownerAnalysis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnerAnalysisExists(ownerAnalysis.OwnerAnalysisId))
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
            return View(ownerAnalysis);
        }

        // GET: OwnerAnalysis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerAnalysis = await _context.OwnerAnalysis
                .FirstOrDefaultAsync(m => m.OwnerAnalysisId == id);
            if (ownerAnalysis == null)
            {
                return NotFound();
            }

            return View(ownerAnalysis);
        }

        // POST: OwnerAnalysis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ownerAnalysis = await _context.OwnerAnalysis.FindAsync(id);
            _context.OwnerAnalysis.Remove(ownerAnalysis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OwnerAnalysisExists(int id)
        {
            return _context.OwnerAnalysis.Any(e => e.OwnerAnalysisId == id);
        }
    }
}
