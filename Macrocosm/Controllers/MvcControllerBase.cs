using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Macrocosm.Controllers
{
    public interface IMvcControllerBase
    {
        Task<IActionResult> GetSingle();
        Task<IActionResult> GetAll();
    }
}
