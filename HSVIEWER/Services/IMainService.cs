using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSVIEWER.Services
{
    public interface IMainService
    {
        Task<List<Stage>> GetAllStages(string model);
        Task<List<Owner>> GetAllOwners();

        Task SaveOwnerAnalysis(int workOrderId);

        Task<List<Deal>> GetDealsInStage(string stage, Int32 wid);
        
        Task SaveStageAnalysis(string pipe, Int32 wid);

        Task SaveStageOwnerAnalysis(string pipe, string owner, Int32 wid);
        Task <bool> CheckIsProcessing();

        Task<List<WorkOrder>> GetWorkOrders();

        Task<List<WorkOrder>> GetWorkOrdersByOwner(int ownerId);
        Task<List<OwnerStageAnalysis>> GetOwnerStageAnalysis(int id, string pipelineId);

        Task<List<StagesAnalysis>> GetStagesAnalysis(int workOrder, string pipelineId);

    }
}
