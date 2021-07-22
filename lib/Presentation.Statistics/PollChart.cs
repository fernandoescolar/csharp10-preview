using Spectre.Console;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Statistics
{
	public class PollChart : Renderable
	{
		private readonly Tree _inner;

		private PollChart(Tree inner)
		{
			_inner = inner;
		}

		public static async Task<PollChart> CreateAsync()
		{
			var poll = new Poll();
			var results = await poll.GetResultsAsync();
			var questions = new[] { "Global Using", "Constant interpolated strings", "Record structs", "Static abstract members in interfaces", "File scoped namespaces", "Lambda improvements", "Allow Generic Attributes", "Deconstruction improvements", "List patterns", "Property-Scoped Fields", "Required Properties" };
			var tree = new Tree("Poll").Guide(TreeGuide.BoldLine);
			for (var i = 0; i < questions.Length; i++)
			{
				var r = results.FirstOrDefault(x => x.Question == i);
				if (r != null)
				{
					var label = new Markup("[underline bold]" + questions[i] + "[/]").Centered();
					tree.AddNode(label);

					var chart = new BreakdownChart()
										.ShowPercentage()
										.Width(Console.WindowWidth - 10)
										.AddItem("Crazy", r.Dislikes, Color.Yellow)
										.AddItem("Hot", r.Likes, Color.Magenta1);

					tree.AddNode(chart);
				}
			}

			return new PollChart(tree);
		}

		/// <inheritdoc/>
		protected override Measurement Measure(RenderContext context, int maxWidth)
		{
			return ((IRenderable)_inner).Measure(context, maxWidth);
		}

		/// <inheritdoc/>
		protected override IEnumerable<Segment> Render(RenderContext context, int maxWidth)
		{
			return ((IRenderable)_inner).Render(context, maxWidth);
		}
	}
}
