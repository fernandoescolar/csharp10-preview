using Spectre.Console.SyntaxHighlighter.Tokenization;
using System.Collections.Generic;

namespace Spectre.Console.SyntaxHighlighter
{
    public class CSharpDefinition : SyntaxDefinition
	{
		private readonly List<string> _references = new List<string>();
		private readonly List<string> _calls = new List<string>();

		public CSharpDefinition() : base("c#")
		{
			AddRule("Space", "^(\\r|\\n|\\t|\\s)");
			AddRule("Comment", "^//.*\n?");
			AddRule("Comment", "^/\\*.*\\*/");
			AddRule("Operator", "^(\\+|-|=|%|\\*|/|&|\\||\\.|,|\\?|\\:)");
			AddRule("Separator", "^({|}|\\(|\\)|\\[|\\]|<|>|;)");
			AddRule("Character", "^'[^']*'");
			AddRule("String", "^[@$]*\"[^\"]*\"");
			AddRule("Pragma", "^(#if|#else|#elif|#endif|#define|#undef|#warning|#error|#line|#region|#endregion|#pragma)", true);
			AddRule("Namespace", "^(namespace|using|global)", true);
			AddRule("Keyword", "^(as|is|new|sizeof|typeof|nameof|true|false|stackalloc|explicit|implicit|operator|base|this|null|default|object|string)", true);
			AddRule("MethodParameter", "^(params|ref|out)", true);
			AddRule("Statement", "^(if|else|switch|case|do|for|foreach|init|in|while|break|continue|goto|return|try|throw|catch|finally|checked|unchecked|fixed|lock|where|await|async|get|set)", true);
			AddRule("Modifier", "^(internal|private|protected|public|abstract|const|event|extern|override|readonly|sealed|static|unsafe|virtual|volatile|required)", true);
			AddRule("ValueType", "^(void|bool|byte|char|decimal|double|enum|float|int|long|sbyte|short|uint|ulong|ushort|var|value|field)", true);
			AddRule("ReferenceType", "^(interface|class|delegate|record|struct)", true);
			AddRule("StandardType", "^(Task|Attribute|Controller|ControllerBase|Delegate)", true);
			AddRule("Integer", "^\\d+");
			AddRule("Integer", "^0x[a-fA-F0-9]+");
			AddRule("Float", "^\\d+\\.\\d+");
		}

		public override void PostProcess(Token prev, Token current, Token next)
		{
			if (prev != null && prev.TokenType == "ReferenceType" && current.TokenType == "Unknown")
			{
				_references.Add(current.Value);
			}

			if (_references.Contains(current.Value) && current.TokenType == "Unknown")
			{
				current.TokenType = "Reference";
			}

			if (current.TokenType == "Unknown" && next != null && next.Value == "(")
			{
				current.TokenType = "Call";
				_calls.Add(current.Value);
			}

			if (_calls.Contains(current.Value) && current.TokenType == "Unknown")
			{
				current.TokenType = "Call";
			}

			base.PostProcess(prev, current, next);
		}
	}
}
