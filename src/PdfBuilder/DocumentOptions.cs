using System;
using System.Drawing;

namespace SyntaxSolutions.PdfBuilder
{
    public class DocumentOptions
    {
        public PageSize PageSize { get; set; }
        public PageOrientation PageOrientation { get; set; }

        public double MarginLeft { get; set; }
        public double MarginTop { get; set; }
        public double MarginRight { get; set; }
        public double MarginBottom { get; set; }

        public TextFontOptions TitleFontOptions { get; set; }
        public TextFontOptions HeadingFontOptions { get; set; }
        public TextFontOptions TextFontOptions { get; set; }

        /// <summary>
        /// Create a DocumentOptions with defaults
        /// </summary>
        public DocumentOptions()
        {
            // default document settings
            this.PageSize = PageSize.A4;
            this.PageOrientation = PageOrientation.Portrait;

            this.MarginLeft = 10; // mm
            this.MarginTop = 10; // mm
            this.MarginRight = 10; // mm
            this.MarginBottom = 10; // mm

            // default font for titles
            this.TitleFontOptions = new TextFontOptions()
            {
                FontFamily = TextFontFamily.TimesNewRoman,
                FontStyle = TextFontStyle.Normal,
                FontWeight = TextFontWeight.Normal,
                FontSize = 28, // points
                FontColor = Color.Black
            };

            // default font for headings
            this.HeadingFontOptions = new TextFontOptions()
            {
                FontFamily = TextFontFamily.TimesNewRoman,
                FontStyle = TextFontStyle.Normal,
                FontWeight = TextFontWeight.Normal,
                FontSize = 16, // points
                FontColor = Color.Black
            };

            // default font for text/pragraphs
            this.TextFontOptions = new TextFontOptions()
            {
                FontFamily = TextFontFamily.TimesNewRoman,
                FontStyle = TextFontStyle.Normal,
                FontWeight = TextFontWeight.Normal,
                FontSize = 12, // points
                FontColor = Color.Black
            };
        }
    }
}
