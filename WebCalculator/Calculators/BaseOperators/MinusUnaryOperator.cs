using OperatorsLibrary;

namespace WebCalculator.Calculators.BaseOperators
{
	class MinusUnaryOperator : UnaryOperator
	{
		public MinusUnaryOperator()
			: base(1, "-")
		{}

		protected override double Execute(double arg)
		{
			return -arg;
		}
	}
}
