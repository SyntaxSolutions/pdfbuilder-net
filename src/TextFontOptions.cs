using System.Drawing;

namespace SyntaxSolutions.PdfBuilder
{
    /// <summary>
    /// Options for font rendering
    /// </summary>
    public class TextFontOptions
    {
        /// <summary>
        /// FontFamily
        /// </summary>
        public TextFontFamily FontFamily { get; set; }

        /// <summary>
        /// FontStyle
        /// </summary>
        public TextFontStyle FontStyle { get; set; }

        /// <summary>
        /// FontWeight
        /// </summary>
        public TextFontWeight FontWeight { get; set; }

        /// <summary>
        /// FontSize
        /// </summary>
        public double FontSize { get; set; }

        /// <summary>
        /// FontColor
        /// </summary>
        public Color FontColor { get; set; }

        /// <summary>
        /// TextFontOptions
        /// </summary>
        public TextFontOptions()
        {
            this.FontFamily = TextFontFamily.TimesNewRoman;
            this.FontStyle = TextFontStyle.Normal;
            this.FontWeight = TextFontWeight.Normal;
            this.FontSize = 12; // points
            this.FontColor = Color.Black;
        }
    }
}
