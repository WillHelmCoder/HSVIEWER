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
    public class Stages1Controller : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly MainService _mainService;

        public Stages1Controller(ApplicationDbContext context, MainService mainService)
        {
            _context = context;
            _mainService = mainService;
        }

        // GET: Stages1
        public async Task<IActionResult> Index(Int32 WId, string pipe = "1703125")
        {
          
            await _mainService.SaveStageAnalysis(pipe);



            var allOwners = await _context.HsOwners.Where(w => w.WorkOrderId == 7).ToListAsync();

            foreach (var item in allOwners)
            {
                var deals = await _context.Deals.Where(w => w.HsOwnerId == item.HsId).ToListAsync();
                var totalDeals = deals.Count();
                var suma = deals.Sum(x => float.Parse(x.Amount));
                var average = 0.0;
                if (totalDeals==0 || suma==0) {
                     average = suma / totalDeals;
                }
                else
                {
                    average = 0;
                }
                
                
               _context.OwnerAnalysis.Add(new OwnerAnalysis { DealsNumber = totalDeals, OwnerPipelineValue = suma, DealAverage = average, OwnerName = item.Name });
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Onwneranalysis"); 
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
