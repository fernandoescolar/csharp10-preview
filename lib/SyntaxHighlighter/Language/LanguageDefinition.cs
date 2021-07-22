using SyntaxHighlighter.Tokenization;
using System.Collections.Generic;

namespace SyntaxHighlighter
{
	public class LanguageDefinition : ILanguageDefinition
	{
		private readonly List<TokenizationRule> _rules = new List<TokenizationRule>();

		private readonly List<SyntaxRule> _tokenRules = new List<SyntaxRule>();

		public LanguageDefinition(string name)
		{
			Name = name;
		}

		public string Name { get; }

		public bool IgnoreLineBreaks { get; protected set; } = false;

		public bool IgnoreWhitespaces { get; protected set; } = false;

		public IEnumerable<TokenizationRule> SyntaxRules { get => _rules; }

		public IEnumerable<SyntaxRule> TokenRules { get => _tokenRules; }

		public virtual void PostProcess(Token prev, Token current, Token next)
		{
		}

		protected void AddRule(string name, string regEx, bool wholeWord = false)
		{
			_rules.Add(new TokenizationRule(name, regEx, wholeWord));
		}

	}
}
