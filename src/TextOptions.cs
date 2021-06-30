using System.Drawing;

namespace SyntaxSolutions.PdfBuilder
{
    /// <summary>
    /// Options for rendering text 
    /// </summary>
    public class TextOptions
    {
        public TextFontOptions FontOptions { get; set; }

        /// <summary>
        /// Create a TextOptions with defaults
        /// </summary>
        public TextOptions()
        {
            this.FontOptions = new TextFontOptions();
        }

        /// <summary>
        /// Return a TextOptions with parameters to set specific properties
        /// </summary>
        /// <param name="FontFamily"></param>
        /// <param name="FontStyle"></param>
        /// <param name="FontWeight"></param>
        /// <param name="FontSize"></param>
        /// <param name="Color"></param>
        /// <returns></returns>
        public static TextOptions Set(
            TextFontFamily FontFamily = null,
            TextFontStyle? FontStyle = null,
            TextFontWeight? FontWeight = null,
            double? FontSize = null,
            Color? FontColor = null
        )
        {
            var value = new TextOptions();

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

            return value;
        }
    }
}
