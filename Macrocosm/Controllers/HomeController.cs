using Macrocosm.Filter;
using Macrocosm.Models;
using Macrocosm.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Macrocosm.Controllers
{
    [ServiceFilter(typeof(CommonFilter))]
    public class HomeController : Controller,IMvcControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;
        public HomeController(ILogger<HomeController> logger, IMovieService movieService):base()
        {
            _logger = logger;
            _movieService = movieService;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //跳转到404错误页
            if (Response.StatusCode == 404)
            {
                return View("/Views/Shared/NoFound.cshtml");
            }
            return View();
        }

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> List()
        {
            var model = await _movieService.GetRedisModelAsync();
            return View(model);
        }
        public IActionResult About()
        {
            return View();
        }

        public Task<IActionResult> GetSingle()
        {
            throw new System.NotImplementedException();
        }

        public Task<IActionResult> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
