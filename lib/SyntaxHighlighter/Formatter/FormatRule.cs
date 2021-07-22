namespace SyntaxHighlighter
{
	public class FormatRule
	{
		public FormatRule(string begin, string end)
		{
			Begin = begin;
			End = end;
		}

		public string Begin { get; }

		public string End { get; }

		public string Format(string str) => $"{Begin}{str}{End}";
	}
}
