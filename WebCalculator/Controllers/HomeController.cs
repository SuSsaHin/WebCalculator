using System;
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
	        var m = new CalculatorModel {Answer = 0.ToString()};
	        return View(m);
		}

		[HttpPost]
		public ActionResult Index(CalculatorModel m)
		{
			if (ModelState.IsValid)
			{
				m.Answer = GetAnswer(m.Operators, m.Input);
			}

			return View(m);
		}
    }
}
