using System.Threading.Tasks;
using Domain.Constats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
       
        
        [HttpGet]
        public async Task<IActionResult> Privacy()
        {
            return  View();
        }
        
    }
}