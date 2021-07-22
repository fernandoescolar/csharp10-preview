using Spectre.Console.Rendering;

namespace Spectre.Console.Slides
{
	public class MarkupSlide : Slide
	{
		public string Markup { get; set; }

		public string Value { get; set; }

		public Justify? Alignment { get; set; }

		protected override IRenderable GetContent()
		{
			var markup = new Markup(Value ?? Markup ?? nameof(Markup));
			if (Alignment.HasValue)
			{
				markup.Alignment = Alignment;
			}

			return markup;
		}
	}
}
