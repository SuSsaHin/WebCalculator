﻿using OperatorsLibrary;

namespace WebCalculator.Calculators
{
	public interface IOperators
	{
		IOperator Get(string text, uint dimension);
	}
}
