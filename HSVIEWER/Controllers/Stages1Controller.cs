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
    public class Stages1Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Stages1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Stages1
        public async Task<IActionResult> Index(Int32 WId, string pipe = "1703125")
        {
            var model = new List<StagesAnalysis>();
            var allStages = await _context.Stages.Where(w => w.HsPipelineId == pipe).ToListAsync();

            foreach (var item in allStages) {

                var deals = await _context.Deals.Where(w => w.HsStageId == item.HsStageId).ToListAsync();
                var totalDeals = deals.Count();
                var suma = deals.Sum(x=>x.Amount);
                var average = suma / totalDeals;

              //  model.Add(new StagesAnalysis { DealsNumber= totalDeals, StageValue=suma, DealAverage= average, Stagename=item.StageName });
                _context.StagesAnalysis.Add(new StagesAnalysis { DealsNumber = totalDeals, StageValue = suma, DealAverage = average, Stagename = item.StageName });
                await _context.SaveChangesAsync();
            }
         

            return RedirectToAction("Index", "StageAnalysis"); 
        }

        // GET: Stages1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage = await _context.Stages
                .FirstOrDefaultAsync(m => m.StageId == id);
            if (stage == null)
            {
                return NotFound();
            }

            return View(stage);
        }

        // GET: Stages1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stages1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StageId,HsStageId,StageName,Pipeline,HsPipelineId,Forecast")] Stage stage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stage);
        }

        // GET: Stages1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage = await _context.Stages.FindAsync(id);
            if (stage == null)
            {
                return NotFound();
            }
            return View(stage);
        }

        // POST: Stages1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StageId,HsStageId,StageName,Pipeline,HsPipelineId,Forecast")] Stage stage)
        {
            if (id != stage.StageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StageExists(stage.StageId))
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
            return View(stage);
        }

        // GET: Stages1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage = await _context.Stages
                .FirstOrDefaultAsync(m => m.StageId == id);
            if (stage == null)
            {
                return NotFound();
            }

            return View(stage);
        }

        // POST: Stages1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stage = await _context.Stages.FindAsync(id);
            _context.Stages.Remove(stage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StageExists(int id)
        {
            return _context.Stages.Any(e => e.StageId == id);
        }
    }
}
