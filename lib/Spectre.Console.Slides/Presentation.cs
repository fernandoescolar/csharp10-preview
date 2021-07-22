using Spectre.Console.Slides;
using System;
using System.Collections.Generic;

namespace Spectre.Console
{
	public class Presentation
	{
		public string Title { get; set; }
		
		public DateTime? Date { get; set; }

		public Color? Background { get; set; }

		public IEnumerable<Slide> Slides { get; set; }
	}
}
