
using System.Collections.Generic;
using System.Drawing;
using System.IO;

using PdfFileWriter; // https://www.codeproject.com/Articles/570682/PDF-File-Writer-Csharp-Class-Library-Version-1-27

namespace SyntaxSolutions.PdfBuilder
{
    public class PdfBuilder
    {
        private PdfDocument document;
        private PdfPage page;
        private PdfContents contents;
        private Dictionary<string, PdfFont> fontDictionary;
        private MemoryStream stream;

        private SizeD pageDimensions;
        private PointD pagePosition;

        private DocumentOptions documentOptions;
        private double scaleFactor;

        internal static double[] UnitInPoints = new double[]
        {
            1.0,			// Point
		    72.0,			// Inch
		    72.0 / 2.54,	// cm
		    72.0 / 25.4,	// mm
		    72.0 / 0.0254,  // meter
        };

        /// <summary>
        /// Get the current horizontal postion of the page cursor
        /// </summary>
        public double PagePositionX
        {
            get
            {
                return this.pagePosition.X;
            }

            set
            {
                this.pagePosition.X = value;
            }
        }

        /// <summary>
        /// Get the current vertical position of the page cursor
        /// </summary>
        public double PagePositionY
        {
            get
            {
                return this.pagePosition.Y;
            }

            set
            {
                this.pagePosition.Y = value;
            }
        }

        /// <summary>
        /// Create a new PdfBuilder
        /// </summary>
        public PdfBuilder()
        {
            var options = new DocumentOptions();
            this.init(options);
        }

        /// <summary>
        /// Create a new PdfBuilder
        /// </summary>
        /// <param name="options"></param>
        public PdfBuilder(DocumentOptions options)
        {
            this.init(options);
        }

        /// <summary>
        /// Set initial PdfBuilder object state
        /// </summary>
        /// <param name="options"></param>
        private void init(DocumentOptions options)
        {
            // use default option if none given 
            if (options == null)
            {
                options = new DocumentOptions();
            }

            this.documentOptions = options;
        }

        /// <summary>
        /// Open a new document 
        /// </summary>
        /// <param name="filePath"></param>
        public void Open()
        {
            this.pageDimensions = new SizeD();

            switch (this.documentOptions.PageSize)
            {
                case PageSize.A0:
                    this.pageDimensions.Width = 841;
                    this.pageDimensions.Height = 1188;
                    break;

                case PageSize.A1:
                    this.pageDimensions.Width = 549;
                    this.pageDimensions.Height = 841;
                    break;

                case PageSize.A2:
                    this.pageDimensions.Width = 420;
                    this.pageDimensions.Height = 594;
                    break;

                case PageSize.A3:
                    this.pageDimensions.Width = 297;
                    this.pageDimensions.Height = 420;
                    break;

                case PageSize.A4:
                    this.pageDimensions.Width = 210;
                    this.pageDimensions.Height = 297;
                    break;

                case PageSize.A5:
                    this.pageDimensions.Width = 148;
                    this.pageDimensions.Height = 210;
                    break;

                case PageSize.A6:
                    this.pageDimensions.Width = 105;
                    this.pageDimensions.Height = 148;
                    break;

                case PageSize.A7:
                    this.pageDimensions.Width = 74;
                    this.pageDimensions.Height = 105;
                    break;

                case PageSize.A8:
                    this.pageDimensions.Width = 52;
                    this.pageDimensions.Height = 74;
                    break;

                default:
                    this.pageDimensions.Width = 0;
                    this.pageDimensions.Height = 0;
                    break;
            }

            if (this.documentOptions.PageOrientation == PageOrientation.Landscape)
            {
                var width = this.pageDimensions.Width;
                var height = this.pageDimensions.Height;
                this.pageDimensions.Height = width;
                this.pageDimensions.Width = height;
            }

            // scale factor for converting point to mm (based on 72 dpi)
            this.scaleFactor = UnitInPoints[(int)UnitOfMeasure.mm];

            // create document 
            this.stream = new MemoryStream();
            this.document = new PdfDocument(this.pageDimensions.Width, this.pageDimensions.Height, UnitOfMeasure.mm, this.stream);

            this.fontDictionary = new Dictionary<string, PdfFont>();

            // build a new font dictionary for this document
            Dictionary<TextFontStyle, PdfFont> fontDictionary = new Dictionary<TextFontStyle, PdfFont>();
        }

        /// <summary>
        /// Return the document contents as an array of bytes 
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytes()
        {
            if (this.stream != null)
            {
                this.document.CreateFile();
                return this.stream.ToArray();
            }

            return null;
        }

        /// <summary>
        /// Close the document
        /// </summary>
        public void Close()
        {
            if (this.stream != null)
            {
                this.stream.Close();
                this.stream = null;
            }
        }

        /// <summary>
        /// Add a new page to the document
        /// </summary>
        public void NewPage()
        {
            this.page = new PdfPage(this.document);
            this.contents = new PdfContents(this.page);

            // set initial position to top left corner
            this.pagePosition = new PointD(this.documentOptions.MarginLeft, (this.pageDimensions.Height - this.documentOptions.MarginTop));
        }

        /// <summary>
        /// Begin a new line on the current page 
        /// </summary>
        /// <param name="lineHeight"></param>
        public void NewLine(double? lineHeight = null)
        {
            if (!lineHeight.HasValue)
            {
                lineHeight = (this.documentOptions.TextFontOptions.FontSize * 1.5) / this.scaleFactor;
            }

            this.pagePosition.X = this.documentOptions.MarginLeft;
            this.pagePosition.Y -= lineHeight.Value;
        }

        /// <summary>
        /// Add title text at the current page's vertical position
        /// </summary>
        /// <param name="text"></param>
        public void AddTitle(string text)
        {
            var options = new TextOptions()
            {
                FontOptions = this.documentOptions.TitleFontOptions
            };

            this.pagePosition.X = this.documentOptions.MarginLeft;
            this.AddText(text, options);
            this.NewLine();
        }

        /// <summary>
        /// Add heading text at the current page's vertical position
        /// </summary>
        /// <param name="text"></param>
        public void AddHeading(string text)
        {
            var options = new TextOptions()
            {
                FontOptions = this.documentOptions.HeadingFontOptions
            };

            this.pagePosition.X = this.documentOptions.MarginLeft;
            this.AddText(text, options);
            this.NewLine();
        }

        /// <summary>
        /// Add text at the current page's vertical and horizontal position. 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="options"></param>
        public void AddText(string text, TextOptions options = null)
        {
            // default options
            if (options == null)
            {
                options = new TextOptions()
                {
                    FontOptions = this.documentOptions.TextFontOptions
                };
            }

            var pdfFont = this.getFont(options.FontOptions);
            var textWidth = this.contents.DrawText(pdfFont, options.FontOptions.FontSize, this.pagePosition.X, this.pagePosition.Y, TextJustify.Left, DrawStyle.Normal, options.FontOptions.FontColor, text);
            this.pagePosition.X += textWidth;
        }

        /// <summary>
        /// Add paragraph of text at the current page's vertical position
        /// </summary>
        /// <param name="text"></param>
        /// <param name="options"></param>
        public void AddParagraph(string text, TextOptions options = null)
        {
            // default options
            if (options == null)
            {
                options = new TextOptions()
                {
                    FontOptions = this.documentOptions.TextFontOptions
                };
            }

            this.pagePosition.X = this.documentOptions.MarginLeft;
            double width = this.pageDimensions.Width - (this.documentOptions.MarginLeft + this.documentOptions.MarginRight);

            var pdfFont = this.getFont(options.FontOptions);
            TextBox textBox = new TextBox(width, 0, 0.5);
            textBox.AddText(pdfFont, options.FontOptions.FontSize, DrawStyle.Normal, options.FontOptions.FontColor, text, (AnnotAction)null);

            // initial Y position
            double positionY = this.pagePosition.Y + (options.FontOptions.FontSize / this.scaleFactor);  

            this.contents.DrawText(this.documentOptions.MarginLeft, ref positionY, this.documentOptions.MarginBottom, 0, 0.15, 0, TextBoxJustify.Left, textBox);

            this.pagePosition.Y = positionY;
            this.NewLine();
        }


        /// <summary>
        /// Add an image to the current page with a specified  width
        /// </summary>
        /// <param name="path"></param>
        /// <param name="width"></param>
        /// <param name="options"></param>
        public void AddImage(string path, double width, ImageOptions options = null)
        {
            // default options
            if (options == null)
            {
                options = new ImageOptions()
                {
                    PositionX = this.PagePositionX,
                    PositionY = this.PagePositionY
                };
            }

            PdfImage pdfImage = new PdfImage(this.document);
            pdfImage.Resolution = options.Resolution;
            pdfImage.ImageQuality = options.Quality;
            pdfImage.LoadImage(path);

            this.contents.SaveGraphicsState();

            // Height is calculated from width as per aspect ratio
            double height = width * pdfImage.HeightPix / pdfImage.WidthPix;

            SizeD newSize = pdfImage.ImageSize(width, height);
            var positionX = options.PositionX;
            var positionY = options.PositionY - newSize.Height;

            this.contents.DrawImage(pdfImage, positionX, positionY, newSize.Width, newSize.Height);
            this.contents.RestoreGraphicsState();
            this.pagePosition.Y = positionY;
        }

        /// <summary>
        /// Get the PdfFont from the from the current documents font dictionary
        /// </summary>
        /// <param name="fontFamily"></param>
        /// <param name="fontStyle"></param>
        /// <param name="fontWeight"></param>
        /// <returns></returns>
        private PdfFont getFont(TextFontOptions fontOptions)
        {
            string fontKey = fontOptions.FontFamily.Value.Replace(" ", string.Empty).ToUpper();
            System.Drawing.FontStyle drawingFontStyle;
            switch (fontOptions.FontStyle)
            {
                case TextFontStyle.Normal:
                    fontKey += "-R";
                    drawingFontStyle = System.Drawing.FontStyle.Regular;
                    if (fontOptions.FontWeight == TextFontWeight.Bold)
                    {
                        drawingFontStyle = System.Drawing.FontStyle.Bold;
                        fontKey += "-B";
                    }
                    break;

                case TextFontStyle.Italic:
                    fontKey += "-I";
                    drawingFontStyle = System.Drawing.FontStyle.Italic;
                    if (fontOptions.FontWeight == TextFontWeight.Bold)
                    {
                        drawingFontStyle = System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic;
                        fontKey += "-B";
                    }
                    break;

                default:
                    drawingFontStyle = System.Drawing.FontStyle.Regular;
                    fontKey += "-R";
                    break;
            }

            if (!this.fontDictionary.ContainsKey(fontKey))
            {
                var pdfFont = PdfFont.CreatePdfFont(this.document, fontOptions.FontFamily.Value, drawingFontStyle, true);
                fontDictionary.Add(fontKey, pdfFont);
            }

            return this.fontDictionary[fontKey];
        }

    }
}
