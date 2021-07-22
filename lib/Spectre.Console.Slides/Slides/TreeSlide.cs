using Spectre.Console.Rendering;
using System.Collections.Generic;

namespace Spectre.Console.Slides
{
	public class TreeSlide : Slide
	{
		public string Value { get; set; }

		public string Tree { get; set; }

		public IEnumerable<TreeSlideItem> Items { get; set; }

		protected override IRenderable GetContent()
		{
			var tree = new Tree(Value ?? Tree ?? Title).Guide(TreeGuide.BoldLine);
			AddChildren(tree, Items);
			return tree;
		}

		private void AddChildren(IHasTreeNodes node, IEnumerable<TreeSlideItem> items)
		{
			if (items == null) return;

			foreach (var item in items)
			{
				var n = node.AddNode(item.Item);
				AddChildren(n, item.Items);
			}
		}
	}
}
