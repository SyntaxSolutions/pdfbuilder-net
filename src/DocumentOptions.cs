using System;
using System.Drawing;

namespace SyntaxSolutions.PdfBuilder
{
    /// <summary>
    /// DocumentOptions
    /// </summary>
    public class DocumentOptions
    {
        private TextFontFamily defaultFontFamily;

        /// <summary>
        /// Page size
        /// </summary>
        public PageSize PageSize { get; set; }

        /// <summary>
        /// Page orientation
        /// </summary>
        public PageOrientation PageOrientation { get; set; }

        /// <summary>
        /// Size of left margin in millimetres
        /// </summary>
        public double MarginLeft { get; set; }

        /// <summary>
        /// Size of top margin in millimetres
        /// </summary>
        public double MarginTop { get; set; }

        /// <summary>
        /// Size of right margin in millimetres
        /// </summary>
        public double MarginRight { get; set; }

        /// <summary>
        /// Size of bottom margin in millimetres
        /// </summary>
        public double MarginBottom { get; set; }

        /// <summary>
        /// Default Font Family 
        /// </summary>
        public TextFontFamily DefaultFontFamily 
        {
            get
            {
                return this.defaultFontFamily;
            }
            set
            {
                this.defaultFontFamily = value;

                if (this.TitleFontOptions != null)
                    this.TitleFontOptions.FontFamily = this.defaultFontFamily;

                if (this.HeadingFontOptions != null)
                    this.HeadingFontOptions.FontFamily = this.defaultFontFamily;

                if (this.TextFontOptions != null)
                    this.TextFontOptions.FontFamily = this.defaultFontFamily;

                if (this.ParagraphFontOptions != null)
                    this.ParagraphFontOptions.FontFamily = this.defaultFontFamily;
            }
        }

        /// <summary>
        /// TextFontOptions for document titles
        /// </summary>
        public TextFontOptions TitleFontOptions { get; set; }

        /// <summary>
        /// TextFontOptions for document headings 
        /// </summary>
        public TextFontOptions HeadingFontOptions { get; set; }

        /// <summary>
        /// TextFontOptions for document text 
        /// </summary>
        public TextFontOptions TextFontOptions { get; set; }

        /// <summary>
        /// TextFontOptions for document paragraphs 
        /// </summary>
        public TextFontOptions ParagraphFontOptions { get; set; }

        /// <summary>
        /// Create a DocumentOptions with defaults
        /// </summary>
        public DocumentOptions()
        {
            // default document settings
            this.PageSize = PageSize.A4;
            this.PageOrientation = PageOrientation.Portrait;
            this.DefaultFontFamily = TextFontFamily.TimesNewRoman;

            this.MarginLeft = 10; // mm
            this.MarginTop = 10; // mm
            this.MarginRight = 10; // mm
            this.MarginBottom = 10; // mm

            // default font for titles
            this.TitleFontOptions = new TextFontOptions()
            {
                FontFamily = this.DefaultFontFamily,
                FontStyle = TextFontStyle.Normal,
                FontWeight = TextFontWeight.Normal,
                FontSize = 28, // points
                FontColor = Color.Black
            };

            // default font for headings
            this.HeadingFontOptions = new TextFontOptions()
            {
                FontFamily = this.DefaultFontFamily,
                FontStyle = TextFontStyle.Normal,
                FontWeight = TextFontWeight.Normal,
                FontSize = 16, // points
                FontColor = Color.Black
            };

            // default font for text
            this.TextFontOptions = new TextFontOptions()
            {
                FontFamily = this.DefaultFontFamily,
                FontStyle = TextFontStyle.Normal,
                FontWeight = TextFontWeight.Normal,
                FontSize = 12, // points
                FontColor = Color.Black
            };

            // default font for paragraphs
            this.ParagraphFontOptions = new TextFontOptions()
            {
                FontFamily = this.DefaultFontFamily,
                FontStyle = TextFontStyle.Normal,
                FontWeight = TextFontWeight.Normal,
                FontSize = 12, // points
                FontColor = Color.Black
            };
        }
    }
}
