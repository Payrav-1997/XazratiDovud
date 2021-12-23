using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Hostory;

namespace Project.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IHistoryService _service;

        public  HistoryController(IHistoryService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetHistory()
        {
            return View(await _service.GetHistory());
        }
        
    }
}