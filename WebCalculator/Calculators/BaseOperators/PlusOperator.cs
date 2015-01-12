using OperatorsLibrary;

namespace WebCalculator.Calculators.BaseOperators
{
	class PlusOperator : BinaryOperator
	{
		public PlusOperator()
			: base(1, "+")
		{}

		protected override double Execute(double arg1, double arg2)
		{
			return arg1 + arg2;
		}
	}
}
