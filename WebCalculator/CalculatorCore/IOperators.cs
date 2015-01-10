using OperatorsLibrary;

namespace WebCalculator.CalculatorCore
{
	public interface IOperators
	{
		IOperator Get(string text, int dimension);
	}
}
