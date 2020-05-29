using Entities.Models;
using HSVIEWER.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSVIEWER.Services
{
    public class MainService : IMainService
    {
        protected ApplicationDbContext _context;
        public MainService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveStageAnalysis(string pipe, Int32 wid)
        {
            var model = new List<StagesAnalysis>();
            var allStages = await GetAllStages(pipe);

            foreach (var item in allStages)
            {
                var deals = await GetDealsInStage(item.HsStageId, wid);
                var totalDeals = deals.Count();
                long suma = deals.Sum(x => long.Parse(x.Amount));

                var average = 0.0;
                if (totalDeals != 0 && suma != 0)
                {
                    average = suma / totalDeals;
                }
                else
                {
                    average = 0;
                }

                var newStageAnalysis = new StagesAnalysis { DealsNumber = totalDeals, StageValue = suma, DealAverage = average, Stagename = item.StageName, PipelineId = pipe };
                model.Add(newStageAnalysis);

            }

            await _context.StagesAnalysis.AddRangeAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Stage>> GetAllStages(string pipeline)
        {
            return await _context.Stages.Where(x => x.HsPipelineId.Equals(pipeline)).ToListAsync();
        }

        public async Task<List<Deal>> GetDealsInStage(string stage, Int32 wid)
        {
            return await _context.Deals.Where(x => x.HsStageId.Equals(stage)).ToListAsync();
        }

        public async Task<HsOwner> GetOwnerNameByOwnerId(string id)
        {
            return await _context.HsOwners.Where(x => x.eMail == id).FirstOrDefaultAsync();
        }

        public async Task<List<Deal>> GetDealsInStagebyOwner(string stage, string owner, Int32 wid)
        {
            return await _context.Deals.Where(x => x.HsStageId.Equals(stage)).Where(w => w.HsOwnerId == owner).Where(w => w.WorkOrderId == wid).ToListAsync();
        }

        public async Task<List<Deal>> GetDealsInOwner(string owner)
        {
            return await _context.Deals.Where(x => x.HsOwnerId.Equals(owner)).ToListAsync();
        }

        public async Task<List<HsOwner>> GetOwnersWorkOrder(int workOrderId)
        {
            return await _context.HsOwners.Where(w => w.WorkOrderId == workOrderId).ToListAsync();
        }


        public async Task<bool> CheckIsProcessing()
        {
            var x = await _context.Owners.Where(w => w.OwnerId == 1).SingleOrDefaultAsync();
            if (x.Isprocessing == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public async Task SaveOwnerAnalysis(int wid)
        {
            var allOwners = await GetOwnersWorkOrder(wid);
            var listToInsert = new List<OwnerAnalysis>();
            foreach (var item in allOwners)
            {
                var deals = await GetDealsInOwner(item.HsId);
                var totalDeals = deals.Count();
                var suma = deals.Sum(x => float.Parse(x.Amount));
                var average = 0.0;
                if (totalDeals != 0 || suma != 0)
                {
                    average = suma / totalDeals;
                }
                else
                {
                    average = 0;
                }

                listToInsert.Add(new OwnerAnalysis { DealsNumber = totalDeals, OwnerPipelineValue = suma, DealAverage = average, OwnerName = item.Name, WorkOrderId = wid });


            }
            await _context.OwnerAnalysis.AddRangeAsync(listToInsert);
            await _context.SaveChangesAsync();
        }


        public async Task SaveStageOwnerAnalysis(string pipe, string owner, Int32 wid)
        {
            var model = new List<OwnerStageAnalysis>();
            var allStages = await GetAllStages(pipe);

            foreach (var item in allStages)
            {
                var deals = await GetDealsInStagebyOwner(item.HsStageId, owner, wid);
                var totalDeals = deals.Count();
                long suma = deals.Sum(x => long.Parse(x.Amount));
                var average = 0.0;
                if (totalDeals != 0 || suma != 0) { average = suma / totalDeals; }
                else
                {
                    average = 0;

                }

                var ownerName = await GetOwnerNameByOwnerId(owner);

                var newOwnerStageAnalysis = new OwnerStageAnalysis
                { DealsNumber = totalDeals, StageValue = suma, DealAverage = average, StageName = item.StageName, PipeLineId = pipe, OwnerName = ownerName.Name, WorkOrderId = wid, OwnerMail = owner };
                model.Add(newOwnerStageAnalysis);

            }

            await _context.OwnerStageAnalysis.AddRangeAsync(model);
            await _context.SaveChangesAsync();
        }

    }
}