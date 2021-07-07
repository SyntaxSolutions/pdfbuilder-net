using System.Drawing;

namespace SyntaxSolutions.PdfBuilder
{
    /// <summary>
    /// Options for rendering a line
    /// </summary>
    public class LineOptions
    {
        /*
        /// <summary>
        /// Get ot set the line start position
        /// </summary>
        public Position PositionStart { get; set; }

        /// <summary>
        /// Get ot set the line end position
        /// </summary>
        public Position PositionEnd { get; set; }
        */

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
            //this.PositionStart = new Position(0, 0);
            //this.PositionEnd = new Position(0, 0);
            this.LineWidth = 1.0;
            this.LineColor = Color.Black;
        }

        /// <summary>
        /// Return a LineOptions with parameters to set specific properties
        /// </summary>
        /// <param name="PositionStart"></param>
        /// <param name="PositionEnd"></param>
        /// <param name="LineWidth"></param>
        /// <param name="LineColor"></param>
        /// <returns></returns>
        public static LineOptions Set (
            //Position PositionStart = null,
            //Position PositionEnd = null,
            double? LineWidth = null,
            Color? LineColor = null
        )
        {
            var value = new LineOptions();

            /*
            if (PositionStart != null)
            {
                value.PositionStart = PositionStart;
            }

            if (PositionEnd != null)
            {
                value.PositionEnd = PositionEnd;
            }
            */

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
