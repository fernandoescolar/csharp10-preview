using System.Collections;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace Spectre.Console.Yaml
{
	internal sealed class ParsingEventBuffer : IParser, IEnumerable<ParsingEvent>
	{
		private readonly LinkedList<ParsingEvent> _buffer;

		private LinkedListNode<ParsingEvent> _current;

		public ParsingEventBuffer(LinkedList<ParsingEvent> events)
		{
			_buffer = events;
			_current = events.First;
		}

		public ParsingEvent Current => _current?.Value;

		public bool MoveNext()
		{
			_current = _current.Next;
			return _current != null;
		}

		public void Reset()
		{
			_current = _buffer.First;
		}

		public IEnumerator<ParsingEvent> GetEnumerator()
		{
			return _buffer.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
