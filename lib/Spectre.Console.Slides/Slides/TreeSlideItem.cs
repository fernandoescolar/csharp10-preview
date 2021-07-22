using System.Collections.Generic;

namespace Spectre.Console.Slides
{
	public class TreeSlideItem
	{ 
		public string Item { get; set; }

		public IEnumerable<TreeSlideItem> Items { get; set; }
	}
}
