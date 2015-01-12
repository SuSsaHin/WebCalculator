using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OperatorsLibrary;
using WebCalculator.Calculators.BaseOperators;

namespace WebCalculator.Calculators
{
	public class PluginsOperators : IOperators
	{
		private readonly Dictionary<string, Dictionary<string, IOperator>> operators = new Dictionary<string, Dictionary<string, IOperator>>();

		private const string baseSetName = "base";

		private static string GetKey(string operatorText, uint operatorDimension)
		{
			return operatorText + " " + operatorDimension;
		}

		private void ValidateOperator(IOperator oper)
		{
			if (string.IsNullOrEmpty(oper.Text))
				throw new Exception("Found operators with empty text");

			if (oper.Text.First() == CalculatorParams.DecimalSeparator)
				throw new Exception("Operator '" + oper.Text + "' starts with " + CalculatorParams.DecimalSeparator);

			foreach (var c in oper.Text)
			{
				if (c == ' ' || c == CalculatorParams.LeftBracket || c == CalculatorParams.RightBracket)
					throw new Exception("Operator '" + oper.Text + "' contains " + c);
			}

			var key = GetKey(oper.Text, oper.Dimension);

			foreach (var set in operators)
			{
				if (set.Value.ContainsKey(key))
					throw new Exception(set.Key + " contains operator " + key);
			}
		}

		private void AddPlugin(Assembly plugin, string dllName)
		{
			var newTypes =
				plugin.GetTypes().Where(t => t.GetInterfaces().Any(inter => inter.Name == typeof(IOperator).Name) && !t.IsAbstract).ToList();

			if (!newTypes.Any())
				throw new Exception("File doesn't contains operators");

			var newOperators = new Dictionary<string, IOperator>();

			foreach (var type in newTypes)
			{
				var added = (IOperator)Activator.CreateInstance(type);
				ValidateOperator(added);

				newOperators.Add(GetKey(added.Text, added.Dimension), added);
			}

			operators.Add(dllName, newOperators);
		}

		public PluginsOperators()
		{
			var operatorsList = new List<IOperator>
			{
				new PlusOperator(),
				new DivisionOperator(),
				new MultiplyOperator(),
				new MinusOperator(),
				new MinusUnaryOperator(),
			};

			var baseOperators = operatorsList.ToDictionary(op => GetKey(op.Text, op.Dimension));

			operators.Add(baseSetName, baseOperators);
		}

		public IOperator Get(string text, uint dimension)
		{
			var key = GetKey(text, dimension);

			IOperator result = null;
			if (operators.Any(set => set.Value.TryGetValue(key, out result)))
			{
				return result;
			}
			throw new Exception("Unknown operator: " + key);
		}

		public Dictionary<string, List<string>> GetNamesList()
		{
			return operators.ToDictionary(set => set.Key, set => set.Value.Select(op => op.Key).ToList());
		}

		public void AddPlugin(byte[] pluginFile, string dllName)
		{
			dllName = dllName.ToLower();

			if (pluginFile == null || pluginFile.Length == 0)
				throw new Exception("Empty plugin file");

			if (operators.ContainsKey(dllName))
				throw new Exception("Dll " + dllName + " was always loaded");

			var plugin = Assembly.Load(pluginFile);
			AddPlugin(plugin, dllName);
		}

		public bool DeletePlugin(string pluginName)
		{
			if (pluginName == baseSetName)
				return false;

			return operators.Remove(pluginName);
		}
	}
}
