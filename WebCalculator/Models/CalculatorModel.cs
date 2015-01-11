using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebCalculator.Calculators;

namespace WebCalculator.Models
{
	public class CalculatorModel
	{
		public readonly PluginsOperators Operators = new PluginsOperators();

		public List<string> Plugins { get; set; }

		public List<string> SelectedOperators { get; set; }

		//[Required(ErrorMessage = "Выберите плагин")]
		[Display(Name = "Плагин")]
		public string SelectedPlugin { get; set; }

		[Required(ErrorMessage = "Пустое выражение")]
		[Display(Name = "Введите выражение ")]
		public string InputExpression { get; set; }

		public string Result { get; set; }
	}
}