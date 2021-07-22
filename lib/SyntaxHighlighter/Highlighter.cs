using SyntaxHighlighter.Tokenization;
using System.Text;

namespace SyntaxHighlighter
{
	public class Highlighter
	{
		private readonly LanguageDefinition _syntax;
		private readonly FormatDefinition _format;

		public Highlighter(LanguageDefinition syntax, FormatDefinition format)
		{
			_syntax = syntax;
			_format = format;
		}

		public string Highlight(string code)
		{
			var sb = new StringBuilder();
			var tokenizer = new SyntaxTokenizer(_syntax);
			var tokens = tokenizer.Tokenize(code);
			Token prev, current = null, next = null;

			foreach (var token in tokens)
			{
				prev = current;
				current = next;
				next = token;

				if (current == default) continue;
				HighlightStep(sb, prev, current, next);
			}

			prev = current;
			current = next;
			next = null;
			HighlightStep(sb, prev, current, next);

			return sb.ToString();
		}

		private void HighlightStep(StringBuilder sb, Token prev, Token current, Token next)
		{
			_syntax.PostProcess(prev, current, next);
			if (_format.Rules.ContainsKey(current.TokenType))
			{
				sb.Append(_format.Rules[current.TokenType].Begin);
				sb.Append(_format.FormatText(current.Value));
				sb.Append(_format.Rules[current.TokenType].End);
			}
			else
			{
				sb.Append(_format.FormatText(current.Value));
			}
		}
	}
}
