using Spectre.Console.Rendering;
using System.Collections.Generic;

namespace Spectre.Console.SyntaxHighlighter
{
	public sealed class Code : Renderable, IAlignable, IOverflowable
    {
        private readonly Markup _inner;

        /// <inheritdoc/>
        public Justify? Alignment
        {
            get => _inner.Alignment;
            set => _inner.Alignment = value;
        }

        /// <inheritdoc/>
        public Overflow? Overflow
        {
            get => _inner.Overflow;
            set => _inner.Overflow = value;
        }

        /// <summary>
        /// Gets the character count.
        /// </summary>
        public int Length => _inner.Length;

        /// <summary>
        /// Gets the number of lines.
        /// </summary>
        public int Lines => _inner.Lines;

        /// <summary>
        /// Initializes a new instance of the <see cref="Code"/> class.
        /// </summary>
        /// <param name="code">The code text.</param>
        /// <param name="style">The style of the text.</param>
        public Code(string code)
        {
            var highlighter = new Highlighter(new CSharpDefinition(), new MarkupFormatDefinition());
            var markup = highlighter.Highlight(code);
			try
			{
                _inner = new Markup(markup);
            }
			catch (global::System.Exception ex)
			{
				throw ex;
			}
            
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
