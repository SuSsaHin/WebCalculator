using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebCalculator.Models
{
	public class CalculatorModel
	{
		[Required(ErrorMessage = "Пустое выражение")]
		[Display(Name = "Введите выражение ")]
		public string InputExpression { get; set; }

		public string Result { get; set; }

		public PluginsModel Plugins { get; set; }
		
		public class PluginsModel
		{
			public List<string> Plugins { get; set; }

			public List<string> Operators { get; set; }
		}
	}
}