using Entities.Models;
using HSVIEWER.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSVIEWER.Services
{
    public class MainService
    {
        protected ApplicationDbContext _context;
        public MainService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveStageAnalysis(string pipe)
        {
            var model = new List<StagesAnalysis>();
            var allStages = await GetAllStages(pipe);

            foreach (var item in allStages)
            {
                var deals = await GetDealsInStage(item.HsStageId);
                var totalDeals = deals.Count();
                var suma = deals.Sum(x => x.Amount);
                var average = suma / totalDeals;

                var newStageAnalysis = new StagesAnalysis { DealsNumber = totalDeals, StageValue = suma, DealAverage = average, Stagename = item.StageName };

                _context.StagesAnalysis.Add(newStageAnalysis);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<Stage>> GetAllStages(string pipeline)
        {
            return await _context.Stages.Where(x => x.HsPipelineId.Equals(pipeline)).ToListAsync();
        }

        public async Task<List<Deal>> GetDealsInStage(string stage)
        {
            return await _context.Deals.Where(x => x.HsStageId.Equals(stage)).ToListAsync();
        }

    }
}
