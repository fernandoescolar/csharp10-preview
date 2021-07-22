using Spectre.Console.Slides;
using Spectre.Console.Yaml;
using System;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Spectre.Console
{
	public class SlideShow
	{
		private readonly Presentation _presentation;
		private int _index = 0;

		public SlideShow(Presentation presentation)
		{
			_presentation = presentation;
		}

		public Slide Current { get => _presentation?.Slides.Skip(_index).FirstOrDefault(); }

		public void Prev()
		{
			_index = Math.Max(0, _index - 1);
		}

		public void Next()
		{
			_index = Math.Min(_presentation.Slides.Count() - 1, _index + 1);
		}

		public void Finish()
		{
			_index = 0;
			AnsiConsole.Reset();
			AnsiConsole.Clear();
		}

		public void Render()
		{
			AnsiConsole.Background = Current.Background ?? _presentation.Background ?? AnsiConsole.Background;
			AnsiConsole.Clear();
			RenderProgressBar();
			RenderCurrentSlide();
			RenderNavigation();
		}

		private void RenderProgressBar()
		{
			AnsiConsole.Progress()
								   .AutoRefresh(false)
								   .AutoClear(false)
								   .HideCompleted(false)
								   .Columns(new ProgressColumn[] { new ProgressBarColumn { Width = System.Console.WindowWidth } })
								   .Start(ctx => ctx.AddTask("slides", false, _presentation.Slides.Count()).Increment(_index + 1));
		}

		private void RenderCurrentSlide()
		{
			AnsiConsole.Render(Current.Render());
		}

		private void RenderNavigation()
		{
			var prevColor = _index > 0 ? "gray" : "black";
			var nextColor = _index < _presentation.Slides.Count() - 1 ? "gray" : "black";
			var total = _presentation.Slides.Count().ToString("00");
			var current = (_index + 1).ToString("00");
			var sb = new StringBuilder();
			sb.Append("[gray]ESC[/]");
			sb.Append(" ");
			sb.Append($"[{prevColor}]<[/]");
			sb.Append(" ");
			sb.Append(current);
			sb.Append("/");
			sb.Append(total);
			sb.Append(" ");
			sb.Append($"[{nextColor}]>[/]");

			System.Console.CursorTop = System.Console.WindowHeight - 1;
			AnsiConsole.Render(new Markup(sb.ToString()).Alignment(Justify.Right));
		}

		public static void Start(string filepath)
		{
			AnsiConsole.Markup("Are you [underline red]ready[/]?");
			System.Console.ReadLine();

			var presentation = Load(filepath);
			if (presentation == null) return;

			var show = new SlideShow(presentation);
			var exit = false;
			while (!exit)
			{
				show.Render();

				var option = System.Console.ReadKey(false).Key;
				if (option == ConsoleKey.LeftArrow)
				{
					show.Prev();
				}
				if (option == ConsoleKey.RightArrow || option == ConsoleKey.Enter)
				{
					show.Next();
				}
				if (option == ConsoleKey.Escape)
				{
					exit = true;
				}
			}

			show.Finish();
		}

		private static Presentation Load(string filepath)
		{
			if (!File.Exists(filepath)) return default;

			var folder = Path.GetDirectoryName(filepath);
			var deserializer = new DeserializerBuilder()
										.WithNamingConvention(CamelCaseNamingConvention.Instance)
										.WithNodeDeserializer(new SlideNodeDeserializer())
										.WithTypeConverter(new ColorTypeConverter())
										.Build();

			var presentation = deserializer.Deserialize<Presentation>(File.ReadAllText(filepath));
			presentation.Slides.OfType<ImageSlide>().ToList().ForEach(x => x.Src = Path.Combine(folder, x.Src));

			return presentation;
		}
	}
}
