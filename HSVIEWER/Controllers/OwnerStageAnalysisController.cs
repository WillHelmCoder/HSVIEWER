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
    public class OwnerStageAnalysisController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMainService _mainService;

        public OwnerStageAnalysisController(ApplicationDbContext context, IMainService mainService)
        {
            _context = context;
            _mainService = mainService;
        }

        // GET: OwnerStageAnalysis
        public async Task<IActionResult> Index(string pipe, string owner, Int32 wid)
        {
            var ownerName = _context.HsOwners.Where(w=>w.eMail==owner).FirstOrDefault().Name;
            Int32 isProcessed = await _context.OwnerStageAnalysis.Where(w => w.OwnerMail == owner).Where(w => w.PipeLineId == pipe).CountAsync();

            if (isProcessed != 0)
            {
                return View(await _context.OwnerStageAnalysis.Where(x => x.OwnerMail == owner).Where(w=>w.PipeLineId==pipe).ToListAsync());
            }
            else
            {
                await _mainService.SaveStageOwnerAnalysis(pipe, owner, wid); ;
            }
          

            return View(await _context.OwnerStageAnalysis.Where(x=>x.OwnerMail == owner).Where(w => w.PipeLineId == pipe).ToListAsync());
        }

        // GET: OwnerStageAnalysis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerStageAnalysis = await _context.OwnerStageAnalysis
                .FirstOrDefaultAsync(m => m.OwnerStageAnalysisId == id);
            if (ownerStageAnalysis == null)
            {
                return NotFound();
            }

            return View(ownerStageAnalysis);
        }

        // GET: OwnerStageAnalysis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OwnerStageAnalysis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OwnerStageAnalysisId,OwnerName,StageName,PipeLineId,DealsNumber,DealAverage,StageValue")] OwnerStageAnalysis ownerStageAnalysis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ownerStageAnalysis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ownerStageAnalysis);
        }

        // GET: OwnerStageAnalysis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerStageAnalysis = await _context.OwnerStageAnalysis.FindAsync(id);
            if (ownerStageAnalysis == null)
            {
                return NotFound();
            }
            return View(ownerStageAnalysis);
        }

        // POST: OwnerStageAnalysis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OwnerStageAnalysisId,OwnerName,StageName,PipeLineId,DealsNumber,DealAverage,StageValue")] OwnerStageAnalysis ownerStageAnalysis)
        {
            if (id != ownerStageAnalysis.OwnerStageAnalysisId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ownerStageAnalysis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnerStageAnalysisExists(ownerStageAnalysis.OwnerStageAnalysisId))
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
            return View(ownerStageAnalysis);
        }

        // GET: OwnerStageAnalysis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerStageAnalysis = await _context.OwnerStageAnalysis
                .FirstOrDefaultAsync(m => m.OwnerStageAnalysisId == id);
            if (ownerStageAnalysis == null)
            {
                return NotFound();
            }

            return View(ownerStageAnalysis);
        }

        // POST: OwnerStageAnalysis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ownerStageAnalysis = await _context.OwnerStageAnalysis.FindAsync(id);
            _context.OwnerStageAnalysis.Remove(ownerStageAnalysis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OwnerStageAnalysisExists(int id)
        {
            return _context.OwnerStageAnalysis.Any(e => e.OwnerStageAnalysisId == id);
        }
    }
}
