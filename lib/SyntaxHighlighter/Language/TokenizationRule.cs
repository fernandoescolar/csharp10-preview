namespace SyntaxHighlighter
{
	public record TokenizationRule(string Name, string RegEx, bool WholeWord = false);

	public record SyntaxRule(string Name, string PrevType = default, string PrevValue = default, string CurrentType = default, string CurrentValue = default,  string NextType = default, string NextValue = default);
}
