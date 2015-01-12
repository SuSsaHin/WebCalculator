using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BaseOperators;
using OperatorsLibrary;

namespace WebCalculator.Calculators
{
	public class PluginsOperators : IOperators
	{
		private readonly Dictionary<string, Dictionary<string, IOperator>> operators = new Dictionary<string, Dictionary<string, IOperator>>();

		private const string baseName = "base";

		private static string GetKey(string operatorText, int operatorDimension)
		{
			return operatorText + " " + operatorDimension;
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

			operators.Add(baseName, baseOperators);
		}

		public IOperator Get(string text, int dimension)
		{
			var key = GetKey(text, dimension);

			IOperator result = null;
			if (operators.Any(set => set.Value.TryGetValue(key, out result)))
			{
				return result;
			}
			throw new Exception("Unknown operator: " + key);
		}

		public Dictionary<string, List<string>> GetList()
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

		private void AddPlugin(Assembly plugin, string dllName)
		{
			var newTypes =
				plugin.GetTypes().Where(t => t.GetInterfaces().Any(inter => inter.Name == typeof (IOperator).Name)).ToList();
			var newOperators = new Dictionary<string, IOperator>();

			foreach (var type in newTypes)
			{
				var added = (IOperator) Activator.CreateInstance(type);
				var addedKey = GetKey(added.Text, added.Dimension);

				foreach (var set in operators)
				{
					if (set.Value.ContainsKey(addedKey))
						throw new Exception(set.Key + " contains operator " + addedKey);
				}

				newOperators.Add(addedKey, added);
			}

			operators.Add(dllName, newOperators);
		}

		public bool DeletePlugin(string pluginName)
		{
			if (pluginName == baseName)
				return false;

			return operators.Remove(pluginName);
		}
	}
}
