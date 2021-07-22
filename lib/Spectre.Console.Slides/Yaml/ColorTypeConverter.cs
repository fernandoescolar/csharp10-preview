using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Spectre.Console.Yaml
{
	internal sealed class ColorTypeConverter : IYamlTypeConverter
	{
		public bool Accepts(Type type)
			=> type.IsAssignableFrom(typeof(Color));

		public object ReadYaml(IParser parser, Type type)
		{
			var scalar = parser.Consume<Scalar>();
			var c = System.Drawing.Color.FromArgb(int.Parse(scalar.Value.Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
			return new Color(c.R, c.G, c.B);
		}

		public void WriteYaml(IEmitter emitter, object value, Type type)
		{
			var c = (Color)value;
			emitter.Emit(new Scalar(c.ToHex()));
		}
	}
}
