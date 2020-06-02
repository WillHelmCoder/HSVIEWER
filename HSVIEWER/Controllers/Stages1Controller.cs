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
                await _mainService.SaveStageAnalysis(pipe, workOrderId);

                await _mainService.SaveOwnerAnalysis(workOrderId);

                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> ShowGraph(string pipelineId)
        {
            var wo = await _mainService.GetWorkOrders();
            var listGraph = new MainGraphModel();
            var owners = await _mainService.GetAllOwners();


            foreach (var owner in owners)
            {
                var woo = await _mainService.GetWorkOrdersByOwner(owner.OwnerId);
                var graph = new OwnerGraphModel();

                foreach (var woitem in wo)
                {

                    var osa = await _mainService.GetOwnerStageAnalysis(woitem.WorkOrderId, pipelineId);
                    var getOwners = osa.Select(x => x.OwnerName).Distinct().ToList();
                    var stages = osa.Select(x => x.StageName).Distinct().ToList();

                    foreach (var stage in stages)
                    {
                        if (!graph.OwnersGrap.Any(x => x.Label.Equals(stage)))
                        {
                            var dataList = new List<int>();
                            dataList.Add(osa.Where(x => x.StageName.Equals(stage)).Sum(x => x.DealsNumber));
                            graph.OwnersGrap.Add(new WorkOrderBar
                            {
                                Label = stage,
                                Data = dataList
                            });
                        }
                        else
                        {
                            graph.OwnersGrap.SingleOrDefault(x => x.Label.Equals(stage)).Data.Add(osa.Where(x => x.StageName.Equals(stage)).Sum(x => x.DealsNumber));
                        }
                    }
                }

                listGraph.OwnersGraphs.Add(graph);
            }

            foreach (var item in wo) {
                var sa = await _mainService.GetStagesAnalysis(item.WorkOrderId, pipelineId);
                var stages = sa.Select(x => x.Stagename).Distinct().ToList();
                var graph = new WorkOrderBar();

                foreach (var stage in stages) {
                    if (listGraph.StagesGraph.Any(x => x.Label.Equals(stage)))
                    {
                        var dataList = new List<int>();
                        dataList.Add(sa.Where(x => x.Stagename.Equals(stage)).Sum(x => x.DealsNumber));
                        listGraph.StagesGraph.Add(new WorkOrderBar
                        {
                            Label = stage,
                            Data = dataList
                        });
                    }
                    else { 
                        listGraph.StagesGraph.SingleOrDefault(x => x.Label.Equals(stage)).Data.Add(sa.Where(x => x.Stagename.Equals(stage)).Sum(x => x.DealsNumber));
                    }
                        
                }
            }


            return View(listGraph);
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
