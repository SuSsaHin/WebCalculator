using WebCalculator.CalculatorCore;

namespace WebCalculator.Models
{
	public class CalculatorModel
	{
		private readonly PluginsOperators operators = new PluginsOperators();

		public string GetAnswer(string expression)
		{
			var calculator = new Calculator(operators);

			return calculator.Calculate(expression).ToString(CalculatorParams.CultureInfo);
		}

		public string Input;

		public string Answer;
	}
}