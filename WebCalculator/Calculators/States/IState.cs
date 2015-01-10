namespace WebCalculator.Calculators.States
{
	interface IState
	{
		void ProcessChar(CalculatorContext context, char c);
		void Complete(CalculatorContext context);
	}
}
