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
        Task<List<Deal>> GetDealsInStage(string stage);
        Task SaveStageAnalysis(string pipe);
        Task SaveOwnerAnalysis(int workOrderId);
        


    }
}
