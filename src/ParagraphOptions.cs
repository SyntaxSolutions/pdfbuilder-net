using System.Drawing;

namespace SyntaxSolutions.PdfBuilder
{
    /// <summary>
    /// Options for rendering text 
    /// </summary>
    public class ParagraphOptions
    {
        public TextFontOptions FontOptions { get; set; }
        public TextAlignment TextAlignment { get; set; }

        /// <summary>
        /// Create a TextOptions with defaults
        /// </summary>
        public ParagraphOptions()
        {
            this.FontOptions = new TextFontOptions()
            {
                FontFamily = TextFontFamily.TimesNewRoman,
                FontStyle = TextFontStyle.Normal,
                FontWeight = TextFontWeight.Normal,
                FontSize = 12, // points
                FontColor = Color.Black
            };

            this.TextAlignment = TextAlignment.Left;
        }

        /// <summary>
        /// Return a ParagraphOptions with parameters to set specific properties
        /// </summary>
        /// <param name="FontFamily"></param>
        /// <param name="FontStyle"></param>
        /// <param name="FontWeight"></param>
        /// <param name="FontSize"></param>
        /// <param name="Color"></param>
        /// <returns></returns>
        public static ParagraphOptions Set(
            TextFontFamily FontFamily = null,
            TextFontStyle? FontStyle = null,
            TextFontWeight? FontWeight = null,
            double? FontSize = null,
            Color? FontColor = null,
            TextAlignment? TextAlignment = null
        )
        {
            var value = new ParagraphOptions();

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

            return value;
        }
    }
}
