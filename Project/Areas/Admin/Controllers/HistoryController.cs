using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Hostory;
using Services.Hostory.Models;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HistoryController : Controller
    {
        private readonly IHistoryService _service;

     

        [HttpPost]
        public async Task<IActionResult> CreateHistory(HistoryViewModel model)
        {
            
            await _service.CreateHistory(model);
            return Redirect("admin/home");
        }

        [HttpGet]
        public async Task<IActionResult> CreateHistory()
        {
            return View();
        }
    }
}