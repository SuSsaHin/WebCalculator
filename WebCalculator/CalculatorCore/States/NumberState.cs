using System;

namespace WebCalculator.CalculatorCore.States
{
	sealed class NumberState : IState 
	{
		private string number;
		private bool hasDot;

		public NumberState(char c)
		{
			Process(c);
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
			number += c;
		}

		public void Complete(CalculatorContext context)
		{
			context.PushNumber(Double.Parse(number, CalculatorParams.CultureInfo));
		}

		private IState GetNext(char c)
		{
			if (Char.IsDigit(c))
				return null;

			if (c == CalculatorParams.DecimalSeparator)
			{
				if (hasDot)
					throw new Exception("Unexpected " + CalculatorParams.DecimalSeparator);

				hasDot = true;
				return null;
			}

			if (c == CalculatorParams.RightBracket || c == CalculatorParams.LeftBracket)
				throw new Exception("Unexpected " + c);

			return new OperatorState(c, 2);
		}
	}
}
