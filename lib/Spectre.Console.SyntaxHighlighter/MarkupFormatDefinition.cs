namespace Spectre.Console.SyntaxHighlighter
{
	public class MarkupFormatDefinition : SyntaxFormatDefinition
	{
		public MarkupFormatDefinition()
		{
			AddRule("Comment", "[#808080]", "[/]");
			AddRule("Namespace", "[magenta]", "[/]");
			AddRule("Keyword", "[magenta]", "[/]");
			AddRule("MethodParameter", "[magenta]", "[/]");
			AddRule("Statement", "[magenta]", "[/]");
			AddRule("Modifier", "[magenta]", "[/]");
			AddRule("ValueType", "[magenta]", "[/]");
			AddRule("ReferenceType", "[magenta]", "[/]");
			AddRule("Reference", "[yellow]", "[/]");
			AddRule("Call", "[cyan]", "[/]");
			AddRule("StandardType", "[cyan]", "[/]");
			AddRule("Operator", "[white bold]", "[/]");
			AddRule("Separator", "[white bold]", "[/]");
			AddRule("Integer", "[cyan]", "[/]");
			AddRule("Float", "[cyan]", "[/]");
			AddRule("Character", "[green]", "[/]");
			AddRule("String", "[green]", "[/]");
			AddRule("Pragma", "[magenta]", "[/]");
			AddRule("Unknown", "[#e3e0e0]", "[/]");
		}

		public override string FormatText(string text)
		{
			return base.FormatText(text).Replace("[", "[[").Replace("]", "]]");
		}
	}
}
