using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCalculator.Calculators;
using WebCalculator.Models;

namespace WebCalculator.Controllers
{
    public class HomeController : Controller
    {
		private static string GetAnswer(IOperators operators, string expression)
		{
			string answer;
			try
			{
				var calculator = new Calculator(operators);
				answer = calculator.Calculate(expression).ToString(CalculatorParams.CultureInfo);
			}
			catch (Exception ex)
			{
				answer = ex.Message;
			}
			
			return answer;
		}

        [HttpGet]
        public ActionResult Index()
        {
	        var m = new CalculatorModel {Result = ""};
	        //m.Plugins = m.Operators.GetList().Select(set => new SelectListItem(){Text = set.Key, Value = set.Key}).ToList();
			m.Plugins = m.Operators.GetList().Select(set => set.Key).ToList();
			m.Plugins.Add("2");
			m.Plugins.Add("3");
			m.Plugins.Add("4");

	        return View(m);
		}

		[HttpPost]
		public ActionResult Index(CalculatorModel m)
		{
			if (ModelState.IsValid)
			{
				m.Result = GetAnswer(m.Operators, m.InputExpression);
			}
			m.Plugins = m.Operators.GetList().Select(set => set.Key).ToList();

			return View(m);
		}

		[HttpPost]
		public ActionResult CalculatorResult(CalculatorModel m)
		{
			if (ModelState.IsValid)
			{
				m.Result = GetAnswer(m.Operators, m.InputExpression);
			}

			return PartialView(m);
		}

		[HttpPost]
		public ActionResult UploadFile(HttpPostedFileWrapper qqfile)
		{
			return Json(new { result = "ok", success = true });
		}
    }
}
