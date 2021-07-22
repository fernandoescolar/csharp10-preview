using Spectre.Console.SyntaxHighlighter.Tokenization;
using System.Collections.Generic;

namespace Spectre.Console.SyntaxHighlighter
{
	public class SyntaxDefinition
	{
		private readonly List<SyntaxRule> _rules = new List<SyntaxRule>();

		public SyntaxDefinition(string name)
		{
			Name = name;
		}

		public string Name { get; }

		public bool IgnoreLineBreaks { get; protected set; } = false;

		public bool IgnoreWhitespaces { get; protected set; } = false;

		public IEnumerable<SyntaxRule> Rules { get => _rules; }

		public virtual void PostProcess(Token prev, Token current, Token next)
		{ 
		}

		protected void AddRule(string name, string regEx, bool wholeWord = false)
		{
			_rules.Add(new SyntaxRule(name, regEx, wholeWord));
		}

	}
}
