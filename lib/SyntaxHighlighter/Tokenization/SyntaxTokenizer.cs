using System.Linq;

namespace SyntaxHighlighter.Tokenization
{
	public class SyntaxTokenizer : AbstractTokenizer
	{
		public SyntaxTokenizer(LanguageDefinition definition) 
			: base(definition.SyntaxRules.Select(x => new TokenDefinition(x.Name, x.RegEx, x.WholeWord)))
		{
			IgnoreLineBreaks = definition.IgnoreLineBreaks;
			IgnoreWhitespaces = definition.IgnoreWhitespaces;
			AddSequenceTerminatorToken = false;
		}

		protected override string GetUnknownTokenType() => "Unknown";
	}
}
