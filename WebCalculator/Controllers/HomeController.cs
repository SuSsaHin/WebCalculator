using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
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
	        var operators = Operators.GetList();
	        var m = new CalculatorModel { Result = "", Operators = new CalculatorModel.PluginsModel { Plugins = operators.Keys.ToList()} };

	        var first = m.Operators.Plugins.FirstOrDefault();
	        m.Operators.Operators = first == null ? new List<string>() : operators[first];

	        return View(m);
		}

		[HttpGet]
		public ActionResult PluginsListPart()
		{
			var operators = Operators.GetList();
			var m = new CalculatorModel.PluginsModel { Plugins = operators.Keys.ToList()};
			m.Operators = operators[m.Plugins.First()];
			return PartialView(m);
		}

		[HttpPost]
		public ActionResult OperatorsListPart(string selected)
		{
			var operators = Operators.GetList()[selected];
			return PartialView(operators);
		}

		[HttpPost]
		public string CalculatorResult(CalculatorModel m)
		{
			if (ModelState.IsValid)
			{
				return GetAnswer(m.InputExpression);
			}

			return "";
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

			return "Success";
		}

		[HttpPost]
		public void DeletePlugin(string deleted)
		{
			Operators.DeletePlugin(deleted);
		}

		[HttpGet]
		public FilePathResult GetFile()
		{
			const string path = @"\libs\OperatorsLibrary.dll";
			return File(path, "application/x-msdos-program", Path.GetFileName(path));
		}

	    [HttpGet]
	    public ActionResult Help()
	    {
		    return View();
	    }
    }
}
