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

        public  HistoryController(IHistoryService service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<IActionResult> CreateHistory(HistoryViewModel model)
        {
            await _service.CreateHistory(model);
            return Redirect("admin/History/GetHistory");
        }
        [HttpGet]
        public async Task<IActionResult> CreateHistory()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetHistory()
        {
            return View(await _service.GetHistory());
        }
    }
}