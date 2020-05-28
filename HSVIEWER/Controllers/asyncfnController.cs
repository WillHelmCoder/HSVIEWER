using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HSVIEWER.Data;
using HSVIEWER.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HSVIEWER.Controllers
{
    public class asyncfnController : Controller
    {
        private readonly MainService _mainService;

        public asyncfnController(MainService mainService)
        {
                        _mainService = mainService;
        }
        // GET: asyncfn
        public async Task<bool> StageAnalyzeAsync(string Id)
        {
            try
            {
                await _mainService.SaveStageAnalysis(Id);
                return true;
            }
            catch (Exception e) { 
            
            }
            return false;           

            
        }
        
      
    }
}