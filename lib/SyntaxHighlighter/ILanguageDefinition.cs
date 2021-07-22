using System.Collections.Generic;

namespace SyntaxHighlighter
{
	public interface ILanguageDefinition
	{
		bool IgnoreLineBreaks { get; }
		bool IgnoreWhitespaces { get; }
		string Name { get; }
		IEnumerable<TokenizationRule> SyntaxRules { get; }
		IEnumerable<SyntaxRule> TokenRules { get; }
	}
}