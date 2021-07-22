using Spectre.Console.Slides;
using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Spectre.Console.Yaml
{
	internal sealed class SlideNodeDeserializer : INodeDeserializer
	{
		private static readonly Dictionary<string, Type> _mappings = new Dictionary<string, Type>
		{
			{ "code", typeof(CodeSlide) },
			{ "image", typeof(ImageSlide) },
			{ "markup", typeof(MarkupSlide) },
			{ "title", typeof(TitleSlide) },
			{ "tree", typeof(TreeSlide) }
		};

		private Dictionary<string, List<ParsingEvent>> _anchors = new Dictionary<string, List<ParsingEvent>>();

		public bool Deserialize(IParser reader, Type expectedType, Func<IParser, Type, object> nestedObjectDeserializer, out object value)
		{
			if (typeof(Slide) == expectedType && reader.Accept<MappingStart>(out var mapping))
			{
				var parser = ReadNestedMapping(reader);
				var first = parser.FirstOrDefault(x => x is Scalar) as Scalar;
				if (first != null && _mappings.ContainsKey(first.Value))
				{
					value = nestedObjectDeserializer(parser, _mappings[first.Value]);
				}
				else
				{
					value = null;
				}

				return true;
			}

			value = null;
			return false;
		}

		private ParsingEventBuffer ReadNestedMapping(IParser reader)
		{
			var result = new LinkedList<ParsingEvent>();
			result.AddLast(reader.Consume<MappingStart>());

			var depth = 0;
			var anchorStack = new Stack<(string, int)>();
			do
			{
				var next = reader.Consume<ParsingEvent>();

				if (next is AnchorAlias a && _anchors.ContainsKey(a.Value.Value))
				{
					depth += next.NestingIncrease;
					result.AddLast(new Scalar(string.Empty));
					foreach (var e in _anchors[a.Value.Value])
					{
						result.AddLast(e);
					}
				}
				else
				{
					depth += next.NestingIncrease;
					result.AddLast(next);
				}

				if (anchorStack.Count > 0)
				{
					(var anchor, var d) = anchorStack.Peek();
					if (d > depth)
					{
						anchorStack.Pop();
					}
					else
					{
						_anchors[anchor].Add(next);
					}
				}

				if (next is Scalar s && !s.Anchor.IsEmpty)
				{
					anchorStack.Push((s.Anchor.Value, depth));
					_anchors[s.Anchor.Value] = new List<ParsingEvent>();
				}
				
			} while (depth >= 0);

			return new ParsingEventBuffer(result);
		}
	}
}
