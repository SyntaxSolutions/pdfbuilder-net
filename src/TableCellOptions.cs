using System.Collections.Generic;
using System.Drawing;

namespace SyntaxSolutions.PdfBuilder
{
    /// <summary>
    /// Options for rendering a table row
    /// </summary>
    public class TableCellOptions
    {
        /// <summary>
        /// Get or set cell font options 
        /// </summary>
        public TextFontOptions FontOptions { get; set; }

        /// <summary>
        /// Get ot set text cell alignment
        /// </summary>
        public TextAlignment TextAlignment { get; set; }

        /// <summary>
        /// Get or set cell background color
        /// </summary>
        public Color BackgroundColor { get; set; }

        public double CellPadding { get; set; }

        /// <summary>
        /// Create a TextOptions with defaults
        /// </summary>
        public TableCellOptions()
        {
            this.FontOptions = new TextFontOptions();
            this.TextAlignment = TextAlignment.Left;
            this.BackgroundColor = Color.Transparent;
            this.CellPadding = 1.0;
        }


        /// <summary>
        /// Return a TableCellOptions with parameters to set specific properties
        /// </summary>
        /// <param name="FontFamily"></param>
        /// <param name="FontStyle"></param>
        /// <param name="FontWeight"></param>
        /// <param name="FontSize"></param>
        /// <param name="FontColor"></param>
        /// <param name="TextAlignment"></param>
        /// <param name="BackgroundColor"></param>
        /// <param name="CellPadding"></param>
        /// <returns></returns>
        public static TableCellOptions Set(
            TextFontFamily FontFamily = null,
            TextFontStyle? FontStyle = null,
            TextFontWeight? FontWeight = null,
            double? FontSize = null,
            Color? FontColor = null,
            TextAlignment? TextAlignment = null,
            Color? BackgroundColor = null,
            double? CellPadding = null
        )
        {
            var value = new TableCellOptions();

            if (FontFamily != null)
            {
                value.FontOptions.FontFamily = FontFamily;
            }

            if (FontStyle.HasValue)
            {
                value.FontOptions.FontStyle = FontStyle.Value;
            }

            if (FontWeight.HasValue)
            {
                value.FontOptions.FontWeight = FontWeight.Value;
            }

            if (FontSize.HasValue)
            {
                value.FontOptions.FontSize = FontSize.Value;
            }

            if (FontColor.HasValue)
            {
                value.FontOptions.FontColor = FontColor.Value;
            }

            if (TextAlignment.HasValue)
            {
                value.TextAlignment = TextAlignment.Value;
            }

            if (BackgroundColor.HasValue)
            {
                value.BackgroundColor = BackgroundColor.Value;
            }

            if (CellPadding.HasValue)
            {
                value.CellPadding = CellPadding.Value;
            }

            return value;
        }
    }
}
