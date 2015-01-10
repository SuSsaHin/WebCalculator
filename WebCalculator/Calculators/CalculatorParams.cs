using System.Globalization;

namespace WebCalculator.Calculators
{
	public static class CalculatorParams
	{
		public const char DecimalSeparator = '.';
		public const char LeftBracket = '(';
		public const char RightBracket = ')';

		public static CultureInfo CultureInfo
		{
			get
			{
				return new CultureInfo("en-US")
				{
					NumberFormat = { NumberDecimalSeparator = DecimalSeparator.ToString() }
				};
			}
		}
	}
}
