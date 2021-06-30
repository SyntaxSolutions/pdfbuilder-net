using System.Drawing;

namespace SyntaxSolutions.PdfBuilder
{
    /// <summary>
    /// options for font rendering
    /// </summary>
    public class TextFontOptions
    {
        public TextFontFamily FontFamily { get; set; }
        public TextFontStyle FontStyle { get; set; }
        public TextFontWeight FontWeight { get; set; }
        public double FontSize { get; set; }
        public Color FontColor { get; set; }

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
