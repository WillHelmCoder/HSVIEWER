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
    public class StagesAnalysisController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly 

        public StagesAnalysisController(ApplicationDbContext context)
        {

            _context = context;
        }

        // GET: StagesAnalysis
        public async Task<IActionResult> Index(string Id)
        {
            var isProcessed = await _context.StagesAnalysis.Where(w=>w.Stagename==Id).SingleOrDefaultAsync();
            if (isProcessed.StagesAnalysisId!=0) {
                return View(await _context.StagesAnalysis.ToListAsync());
            }
            else
            {
                
            }
            return View(await _context.StagesAnalysis.ToListAsync());

        }

        // GET: StagesAnalysis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stagesAnalysis = await _context.StagesAnalysis
                .FirstOrDefaultAsync(m => m.StagesAnalysisId == id);
            if (stagesAnalysis == null)
            {
                return NotFound();
            }

            return View(stagesAnalysis);
        }

        // GET: StagesAnalysis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StagesAnalysis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StagesAnalysisId,Stagename,DealsNumber,DealAverage,StageValue")] StagesAnalysis stagesAnalysis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stagesAnalysis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stagesAnalysis);
        }

        // GET: StagesAnalysis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stagesAnalysis = await _context.StagesAnalysis.FindAsync(id);
            if (stagesAnalysis == null)
            {
                return NotFound();
            }
            return View(stagesAnalysis);
        }

        // POST: StagesAnalysis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StagesAnalysisId,Stagename,DealsNumber,DealAverage,StageValue")] StagesAnalysis stagesAnalysis)
        {
            if (id != stagesAnalysis.StagesAnalysisId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stagesAnalysis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StagesAnalysisExists(stagesAnalysis.StagesAnalysisId))
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
            return View(stagesAnalysis);
        }

        // GET: StagesAnalysis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stagesAnalysis = await _context.StagesAnalysis
                .FirstOrDefaultAsync(m => m.StagesAnalysisId == id);
            if (stagesAnalysis == null)
            {
                return NotFound();
            }

            return View(stagesAnalysis);
        }

        // POST: StagesAnalysis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stagesAnalysis = await _context.StagesAnalysis.FindAsync(id);
            _context.StagesAnalysis.Remove(stagesAnalysis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StagesAnalysisExists(int id)
        {
            return _context.StagesAnalysis.Any(e => e.StagesAnalysisId == id);
        }
    }
}
