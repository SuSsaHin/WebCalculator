namespace WebCalculator.Calculators
{
	public class Calculator
	{
		private readonly CalculatorContext calculatorContext;

		public double Calculate(string expression)
		{
			calculatorContext.Clear();
			foreach (var c in expression)
			{
				calculatorContext.ProcessCharacter(c);
			}

			return calculatorContext.GetAnswer();
		}

		public Calculator(IOperators operators)
		{
			calculatorContext = new CalculatorContext(operators);
		}
	}
}
