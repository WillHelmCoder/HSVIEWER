using Entities.Models;
using HSVIEWER.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSVIEWER.Services
{
    public class MainService:IMainService
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
                long suma = deals.Sum(x => long.Parse(x.Amount));
                var average = suma / totalDeals;

                var newStageAnalysis = new StagesAnalysis { DealsNumber = totalDeals, StageValue = suma, DealAverage = average, Stagename = item.StageName };
                model.Add(newStageAnalysis);

            }

            await _context.StagesAnalysis.AddRangeAsync(model);
            await _context.SaveChangesAsync();
        }
        public async Task SaveOwnerAnalysis(int workOrderId)
        {
            var allOwners = await GetOwnersWorkOrder(workOrderId);
            var listToInsert = new List<OwnerAnalysis>();
            foreach (var item in allOwners)
            {
                var deals = await GetDealsInOwner(item.HsId);
                var totalDeals = deals.Count();
                var suma = deals.Sum(x => float.Parse(x.Amount));
                var average = 0.0;
                if (totalDeals == 0 || suma == 0)
                {
                    average = suma / totalDeals;
                }
                else
                {
                    average = 0;
                }

                listToInsert.Add(new OwnerAnalysis { DealsNumber = totalDeals, OwnerPipelineValue = suma, DealAverage = average, OwnerName = item.Name });


            }
            await _context.OwnerAnalysis.AddRangeAsync(listToInsert);
            await _context.SaveChangesAsync();
        }
        public async Task<List<WorkOrder>> GetAllWorkOrders()
        {
            var wo = await _context.WorkOrder.ToListAsync();
            return wo;
        }

        public async Task<List<Stage>> GetAllStages(string pipeline)
        {
            return await _context.Stages.Where(x => x.HsPipelineId.Equals(pipeline)).ToListAsync();
        }

        public async Task<List<Deal>> GetDealsInStage(string stage)
        {
            return await _context.Deals.Where(x => x.HsStageId.Equals(stage)).ToListAsync();
        }

        public async Task<List<Deal>> GetDealsInOwner(string owner)
        {
            return await _context.Deals.Where(x => x.HsOwnerId.Equals(owner)).ToListAsync();
        }

        public async Task<List<HsOwner>> GetOwnersWorkOrder(int workOrderId)
        {
            return await _context.HsOwners.Where(w => w.WorkOrderId == workOrderId).ToListAsync();
        }

        

        
    }
}
