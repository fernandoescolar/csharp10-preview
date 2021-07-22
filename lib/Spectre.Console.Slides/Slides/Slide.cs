using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spectre.Console.Slides
{
	public abstract class Slide
    {
        public string Title { get; set; }

        public bool Centered { get; set; }

        public Padding Padding { get; set; } = new Padding();

		public Color? Background { get; set; }

		public bool ShowHeader { get; set; } 

		public IRenderable Render()
		{
			var content = GetContent();
			if (Centered)
				return RenderCentered(content);

			return RederWithPadding(content);
		}

		protected abstract IRenderable GetContent();

		private IRenderable RenderCentered(IRenderable content)
		{
			var padding = Math.Max(1, (System.Console.WindowHeight - CountLines(content.GetSegments(AnsiConsole.Console))) / 2 - 3);
			var table = new Table()
								.Centered()
								.Border(TableBorder.None)
								.AddColumn(new TableColumn(new Text(Title ?? nameof(Title)).Centered()))
								.AddRow(new[] { new Panel(content)
														.PadTop(padding - 1)
														.PadBottom(padding - 1)
														.Border(BoxBorder.None) });

			if (!ShowHeader)
			{
				table = table.HideHeaders();
			}

			return table;
		}

		private int CountLines(IEnumerable<Segment> segments)
		{
			var counter = 0;
			foreach (var s in segments)
			{
				counter += s.Text.Count(x => x == '\n');
			}

			return counter;
		}

		private IRenderable RederWithPadding(IRenderable content)
		{
			if (ShowHeader)
			{
				var container = new Container();
				container.Add(new Markup(Title ?? nameof(Title)));
				container.Add(new Text("\n"));
				container.Add(content);
				
				content = container;
			}

			var padder = new Padder(content);
			if (Padding.Top > 0)
				padder = padder.PadTop(Padding.Top);
			if (Padding.Bottom > 0)
				padder = padder.PadBottom(Padding.Bottom);
			if (Padding.Left > 0)
				padder = padder.PadLeft(Padding.Left);
			if (Padding.Right > 0)
				padder = padder.PadRight(Padding.Right);
			
			return padder;
		}
	}
}
