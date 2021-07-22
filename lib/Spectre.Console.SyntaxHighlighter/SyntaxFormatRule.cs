namespace Spectre.Console.SyntaxHighlighter
{
	public class SyntaxFormatRule
	{
		public SyntaxFormatRule(string begin, string end)
		{
			Begin = begin;
			End = end;
		}

		public string Begin { get; }

		public string End { get; }

		public string Format(string str) => $"{Begin}{str}{End}";
	}
}
