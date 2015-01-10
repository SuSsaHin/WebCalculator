using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using WebCalculator.Calculators;
using Assert = NUnit.Framework.Assert;
using ExpectedExceptionAttribute = NUnit.Framework.ExpectedExceptionAttribute;

namespace Tests
{
	[TestClass]
	public class Tests
	{
		private PluginsOperators operators;
		private Calculator calculator;

		[TestFixtureSetUp]
		public void Setup()
		{
			operators = new PluginsOperators();
			calculator = new Calculator(operators);
		}

		[TestCase("55", 55)]
		[TestCase("10", 10)]
		[TestCase("1.0", 1)]
		[TestCase("-1.0", -1)]
		[TestCase("1+2+3+5", 1 + 2 + 3 + 5)]
		[TestCase("1+2*3+5", 1 + 2 * 3 + 5)]
		[TestCase("1+2*(3+5)", 1 + 2 * (3 + 5))]
		[TestCase("1+2*(3*6+(-5))", 1 + 2 * (3 * 6 + (-5)))]
		[TestCase("1+(2*3)+5", 1 + (2 * 3) + 5)]
		[TestCase("1+(4/2*3)+5-5", 1 + (4 / 2 * 3) + 5 - 5)]
		[TestCase("-2^4", -16)]
		[TestCase(".-2^4", 16)]
		public void TestCalculating(string input, double result)
		{
			double calculated = calculator.Calculate(input);
			Assert.That(Math.Abs(calculated - result) < 0.0001);
		}

		[TestCase("55.55.55")]
		[TestCase("25+()")]
		[TestCase("25+(")]
		[TestCase(")25")]
		[TestCase("25+")]
		[TestCase("")]
		[ExpectedException(typeof(Exception))]
		public void TestBadInput(string input)
		{
			calculator.Calculate(input);
		}

		[TestCase("TestLib.dll", "2+(sign 1)", 3)]
		public void TestDll(string dllName, string input, double result)
		{
			operators.AddPlugin(dllName);

			double calculated = calculator.Calculate(input);
			Assert.That(Math.Abs(calculated - result) < 0.0001);
		}
	}
}
