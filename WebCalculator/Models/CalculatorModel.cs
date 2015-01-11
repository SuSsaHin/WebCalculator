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

		public OperatorsModel Operators { get; set; }
		
		public class OperatorsModel
		{
			public List<string> Plugins { get; set; }

			//[Required(ErrorMessage = "Выберите плагин")]
			[Display(Name = "Плагин")]
			public string SelectedPlugin { get; set; }
		}
	}
}