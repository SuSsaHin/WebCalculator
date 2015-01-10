namespace WebCalculator.CalculatorCore.States
{
	interface IState
	{
		void ProcessChar(CalculatorContext context, char c);
		void Complete(CalculatorContext context);
	}
}
