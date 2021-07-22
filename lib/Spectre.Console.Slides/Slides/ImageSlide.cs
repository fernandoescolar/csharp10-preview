using Spectre.Console.Rendering;

namespace Spectre.Console.Slides
{
	public class ImageSlide : Slide
	{
		public string Image { get => Title; set => Title = value; }

		public string Value { get => Title; set => Title = value; }

		public string Src { get; set; }

		protected override IRenderable GetContent()
			=> new CanvasImage(Src);
	}
}
