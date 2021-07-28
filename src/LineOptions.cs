using System.Drawing;

namespace SyntaxSolutions.PdfBuilder
{
    /// <summary>
    /// Options for rendering a line
    /// </summary>
    public class LineOptions
    {
        /// <summary>
        /// Get or set the line width
        /// </summary>
        public double LineWidth { get; set; }

        /// <summary>
        /// Get or set the line color
        /// </summary>
        public Color LineColor { get; set; }

        /// <summary>
        /// Create a LineOptions with defaults
        /// </summary>
        public LineOptions()
        {
            this.LineWidth = 1.0;
            this.LineColor = Color.Black;
        }

        /// <summary>
        /// Return a LineOptions with parameters to set specific properties
        /// </summary>
        /// <param name="LineWidth"></param>
        /// <param name="LineColor"></param>
        /// <returns></returns>
        public static LineOptions Set (
            double? LineWidth = null,
            Color? LineColor = null
        )
        {
            var value = new LineOptions();

            if (LineWidth.HasValue)
            {
                value.LineWidth = LineWidth.Value;
            }

            if (LineColor.HasValue)
            {
                value.LineColor = LineColor.Value;
            }

            return value;
        }
    }
}
