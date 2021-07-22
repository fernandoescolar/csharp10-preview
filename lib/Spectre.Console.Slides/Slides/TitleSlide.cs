using Spectre.Console.Rendering;
using SpectreColor = Spectre.Console.Color;

namespace Spectre.Console.Slides
{
	public class TitleSlide : Slide
	{
		public string Value { get; set; }

		public SpectreColor? Color { get; set; }

		public Justify? Alignment { get; set; }

		protected override IRenderable GetContent()
		{
			var title = new FigletText(Value ?? Title).Color(Color ?? SpectreColor.Magenta1);
			if (Alignment.HasValue)
			{
				title.Alignment = Alignment;
			}

			return title;
		}
	}
}
