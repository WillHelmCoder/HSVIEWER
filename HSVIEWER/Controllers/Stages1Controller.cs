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
        private readonly IMainService _mainService;

        public Stages1Controller(ApplicationDbContext context, IMainService mainService)
        {
            _context = context;
            _mainService = mainService;
        }

        // GET: Stages1
        public async Task<IActionResult> Index(Int32 WId, string pipe, int workOrderId)
        {
            pipe = "1703125";
            try
            {
                await _mainService.SaveStageAnalysis(pipe,workOrderId);

                await _mainService.SaveOwnerAnalysis(workOrderId);

                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> ShowGraph()
        {
            var wo = await _mainService.GetWorkOrders();

            var graph = new GraphModel();
            
            
            foreach(var woitem in wo)
            {
                var osa = await _mainService.GetOwnerStageAnalysis(woitem.WorkOrderId);
                var groupedosa = osa.GroupBy(x => x.OwnerName).Select(x => new { owner = x.Key, Amount = x.Sum(y => y.DealsNumber) }).ToList();

                
                graph.OwnersGrap.Add(new WorkOrderBar
                {
                    Owners = groupedosa.Select(x => x.owner).ToList(),
                    Amounts = groupedosa.Select(x => x.Amount).ToList()
                });

                var sa = await _mainService.GetStagesAnalysis(woitem.WorkOrderId);
                var groupedsa = sa.GroupBy(x => x.Stagename).Select(x => new { owner = x.Key, Amount = x.Sum(y => y.DealsNumber) }).ToList();

                graph.OwnersGrap.Add(new WorkOrderBar
                {
                    Owners = groupedsa.Select(x => x.owner).ToList(),
                    Amounts = groupedsa.Select(x => x.Amount).ToList()
                });

            }

            return View(graph);
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
