using Spectre.Console.Rendering;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Spectre.Console.Slides
{
	internal sealed class Container : Renderable, ICollection<IRenderable>
	{
		private readonly IList<IRenderable> _list;

		public Container()
		{
			_list = new List<IRenderable>();
		}

		public int Count => _list.Count;

		public bool IsReadOnly => _list.IsReadOnly;

		public void Add(IRenderable item)
		{
			_list.Add(item);
		}

		public void Clear()
		{
			_list.Clear();
		}

		public bool Contains(IRenderable item)
		{
			return _list.Contains(item);
		}

		public void CopyTo(IRenderable[] array, int arrayIndex)
		{
			_list.CopyTo(array, arrayIndex);
		}

		public bool Remove(IRenderable item)
		{
			return _list.Remove(item);
		}

		public IEnumerator<IRenderable> GetEnumerator()
		{
			return _list.GetEnumerator();
		}

		/// <inheritdoc/>
		protected override IEnumerable<Segment> Render(RenderContext context, int maxWidth)
		{
			return _list.SelectMany(x => x.Render(context, maxWidth));
		}

		/// <inheritdoc/>
		protected override Measurement Measure(RenderContext context, int maxWidth)
		{
			var measurements = _list.Select(x => x.Measure(context, maxWidth));
			return new Measurement(measurements.Min(x => x.Min), measurements.Max(x => x.Max));	
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
