using System.ComponentModel.DataAnnotations;
using WebCalculator.Calculators;

namespace WebCalculator.Models
{
	public class CalculatorModel
	{
		public readonly PluginsOperators Operators = new PluginsOperators();

		[Required(ErrorMessage = "Пустое выражение")]
		[Display(Name = "Введите выражение")]
		public string Input { get; set; }

		public string Answer { get; set; }
	}
}