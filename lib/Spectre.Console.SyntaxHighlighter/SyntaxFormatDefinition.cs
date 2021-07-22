using System.Collections.Generic;

namespace Spectre.Console.SyntaxHighlighter
{
	public abstract class SyntaxFormatDefinition
	{
		private readonly Dictionary<string, SyntaxFormatRule> _rules = new Dictionary<string, SyntaxFormatRule>();

		public SyntaxFormatDefinition()
		{
		}

		public Dictionary<string, SyntaxFormatRule> Rules { get => _rules; }

		public virtual string FormatText(string text)
		{
			return text;
		}

		protected void AddRule(string name, string begin, string end)
		{
			_rules.Add(name, new SyntaxFormatRule(begin, end));
		}
	}
}