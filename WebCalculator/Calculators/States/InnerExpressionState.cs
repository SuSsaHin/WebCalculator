using System;

namespace WebCalculator.Calculators.States
{
	sealed class InnerExpressionState : IState
	{
		private readonly CalculatorContext innerContext;
		private int bracketsDiff = 1;

		public InnerExpressionState(char c, CalculatorContext context)
		{
			if (c != CalculatorParams.LeftBracket)
				throw new Exception("Unexpected inner expression state initializer");

			innerContext = new CalculatorContext(context.AllOperators);
		}

		public void ProcessChar(CalculatorContext context, char c)
		{
			var nextState = GetNext(c);

			if (nextState == null)
			{
				Process(c);
				return;
			}

			Complete(context);
			context.CurrentState = nextState;
		}

		private void Process(char c)
		{
			if (c == CalculatorParams.LeftBracket)
				bracketsDiff++;
			else if (c == CalculatorParams.RightBracket)
			{
				bracketsDiff--;

				if (bracketsDiff == 0)
					return;
			}

			innerContext.ProcessCharacter(c);
		}

		public void Complete(CalculatorContext context)
		{
			if (bracketsDiff != 0)
				throw new Exception("Lacking " + CalculatorParams.RightBracket);

			context.PushNumber(innerContext.GetAnswer());
		}

		private IState GetNext(char c)
		{
			if (bracketsDiff != 0) 
				return null;

			if (c == CalculatorParams.LeftBracket || c == CalculatorParams.RightBracket)
				throw new Exception("Unexpected " + c);

			if (Char.IsDigit(c))
				return new NumberState(c);

			return new OperatorState(c, 2);
		}
	}
}