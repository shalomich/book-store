using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Store.Controllers
{
    [ApiController]
    [Area("store")]
    public abstract class StoreController : Controller
    {
    }
}
