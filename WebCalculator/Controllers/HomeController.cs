using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using WebCalculator.Calculators;
using WebCalculator.Models;

namespace WebCalculator.Controllers
{
    public class HomeController : Controller
    {
		private PluginsOperators Operators { get { return (PluginsOperators) Session["Operators"]; } }
		private string GetAnswer(string expression)
		{
			string answer;
			try
			{
				var calculator = new Calculator(Operators);
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
			var m = new CalculatorModel { Result = "", Operators = new CalculatorModel.OperatorsModel { Plugins = Operators.GetList().Select(set => set.Key).ToList() } };

	        return View(m);
		}

		[HttpPost]
		public ActionResult CalculatorResult(CalculatorModel m)
		{
			if (ModelState.IsValid)
			{
				m.Result = GetAnswer(m.InputExpression);
			}

			return PartialView(m);
		}

		[HttpGet]
		public ActionResult PluginsList()
		{
			var m = new CalculatorModel.OperatorsModel { Plugins = Operators.GetList().Select(set => set.Key).ToList() };
			return PartialView(m);
		}

		[HttpPost]
		public string Upload()
		{
			try
			{
				var file = Request.Files["file0"];
				if (Path.GetExtension(file.FileName).ToLower() != ".dll")
					return "Bad file format";

				var buffer = new byte[file.ContentLength];
				file.InputStream.Read(buffer, 0, file.ContentLength);
				Operators.AddPlugin(buffer, Path.GetFileNameWithoutExtension(file.FileName));
			}
			catch (Exception e)
			{
				return e.Message;
			}

			return "File successfully loaded";
		}
    }
}
