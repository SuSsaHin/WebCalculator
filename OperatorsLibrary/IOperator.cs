using System.Collections.Generic;

namespace OperatorsLibrary
{
	public interface IOperator
	{
		uint Priority { get; }
		uint Dimension { get; }
		string Text { get; }
		double Execute(List<double> args);
	}
}
