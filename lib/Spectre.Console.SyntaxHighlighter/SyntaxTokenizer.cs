using Spectre.Console.SyntaxHighlighter.Tokenization;
using System.Linq;

namespace Spectre.Console.SyntaxHighlighter
{
	public class SyntaxTokenizer : AbstractTokenizer
	{
		public SyntaxTokenizer(SyntaxDefinition definition) 
			: base(definition.Rules.Select(x => new TokenDefinition(x.Name, x.RegEx, x.WholeWord)))
		{
			IgnoreLineBreaks = definition.IgnoreLineBreaks;
			IgnoreWhitespaces = definition.IgnoreWhitespaces;
			AddSequenceTerminatorToken = false;
		}

		protected override string GetUnknownTokenType() => "Unknown";
	}
}
