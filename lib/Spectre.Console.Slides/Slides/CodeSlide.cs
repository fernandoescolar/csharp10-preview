using Spectre.Console.Rendering;
using Spectre.Console.SyntaxHighlighter;

namespace Spectre.Console.Slides
{
	public class CodeSlide : Slide
	{
		public string Code { get; set; }

		public string Value { get; set; }

		protected override IRenderable GetContent()
			=> new Code(Value ?? Code ?? nameof(Code));
	}
}
