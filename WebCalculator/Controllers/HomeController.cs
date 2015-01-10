using System.Web.Mvc;
using WebCalculator.Models;

namespace WebCalculator.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
	        var m = new CalculatorModel {Answer = 0.ToString()};
	        return View(m);
		}

		[HttpPost]
		public ActionResult Index(CalculatorModel m)
		{
			m.Answer = 1.ToString();
			return View(m);
		}
    }
}
