using System.Collections.Generic;

namespace SyntaxHighlighter
{
	public abstract class FormatDefinition
	{
		private readonly Dictionary<string, FormatRule> _rules = new Dictionary<string, FormatRule>();

		public FormatDefinition(string name)
		{
			Name = name;
		}

		public string Name { get; }

		public Dictionary<string, FormatRule> Rules { get => _rules; }


		public virtual string FormatText(string text)
		{
			return text;
		}

		protected void AddRule(string name, string begin, string end)
		{
			_rules.Add(name, new FormatRule(begin, end));
		}
	}
}