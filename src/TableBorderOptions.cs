using System.Drawing;

namespace SyntaxSolutions.PdfBuilder
{
    /// <summary>
    /// options for table border rendering
    /// </summary>
    public class TableBorderOptions
    {
        /// <summary>
        /// Get or set table border width
        /// </summary>
        public double BorderWidth { get; set; }

        /// <summary>
        /// Get or set table border  color
        /// </summary>
        public Color BorderColor { get; set; }

        /// <summary>
        /// TableBorderOptions contructor
        /// </summary>
        public TableBorderOptions()
        {
            this.BorderWidth = 0.8;
            this.BorderColor = Color.Black;
        }
    }
}
