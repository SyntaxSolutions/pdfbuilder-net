using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

using PdfFileWriter; // https://www.codeproject.com/Articles/570682/PDF-File-Writer-Csharp-Class-Library-Version-1-27
using Helpers = SyntaxSolutions.PdfBuilder.Helper;

namespace SyntaxSolutions.PdfBuilder
{
    /// <summary>
    /// A simple library used to generate PDF files
    /// </summary>
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

        /// <summary>
        /// Get the page width
        /// </summary>
        public double PageWidth
        {
            get
            {
                return this.pageDimensions.Width;
            }
        }

        /// <summary>
        /// Get the page height
        /// </summary>
        public double PageHeight
        {
            get
            {
                return this.pageDimensions.Height;
            }
        }

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
        /// Get the current page count
        /// </summary>
        public int PageCount
        {
            get
            {
                return this.document.PageCount;
            }
        }

        /// <summary>
        /// Occurs when a new page is created 
        /// </summary>
        public event PageHeaderEventHandler PageHeader;

        /// <summary>
        /// Occurs when a new page is created 
        /// </summary>
        public event PageFooterEventHandler PageFooter;

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
        public void Open()
        {
            // determine the page dimensions based on the specified DocumentOptions 
            double width = PageSizeCalc.Width(this.documentOptions.PageSize);
            double height = PageSizeCalc.Height(this.documentOptions.PageSize);

            this.pageDimensions = new SizeD(width, height);

            if (this.documentOptions.PageOrientation == PageOrientation.Landscape)
            {
                this.pageDimensions.Height = width;
                this.pageDimensions.Width = height;
            }

            // create document 
            this.stream = new MemoryStream();
            this.document = new PdfDocument(this.pageDimensions.Width, this.pageDimensions.Height, UnitOfMeasure.mm, this.stream);

            // build a new font dictionary for this document
            this.fontDictionary = new Dictionary<string, PdfFont>();
        }

        /// <summary>
        /// Return the document contents as an array of bytes 
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytes()
        {
            if (this.document != null && this.stream != null)
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
            this.document.Dispose();
            this.document = null; 

            if (this.stream != null)
            {
                this.stream.Close();
                this.stream = null;
            }
        }

        /// <summary>
        /// Add a new page to the document and reset the PagePosition
        /// </summary>
        public void NewPage()
        {
            this.checkBuilderState();

            this.page = new PdfPage(this.document);
            this.contents = new PdfContents(this.page);

            // set initial position to top left corner
            this.pagePosition = new PointD(this.documentOptions.MarginLeft, (this.pageDimensions.Height - this.documentOptions.MarginTop));

            // invoke any page header and footer event handlers 
            this.PageHeader?.Invoke(this, new PageHeaderEventArgs());
            this.PageFooter?.Invoke(this, new PageFooterEventArgs());
        }

        /// <summary>
        /// Begin a new line on the current page 
        /// </summary>
        /// <param name="lineHeight">Line height in mm</param>
        public void NewLine(double? lineHeight = null)
        {
            this.checkBuilderState();

            double height = (this.documentOptions.TextFontOptions.FontSize * 1.5) / this.document.ScaleFactor; ; 
            if (lineHeight.HasValue)
            {
                height = lineHeight.Value;
            }

            this.pagePosition.X = this.documentOptions.MarginLeft;
            this.pagePosition.Y -= height;
        }

        /// <summary>
        /// Add title text at the current page's vertical position
        /// </summary>
        /// <param name="text"></param>
        public void AddTitle(string text)
        {
            this.checkBuilderState();

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
            this.checkBuilderState();

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
            this.checkBuilderState();

            if (options == null)
            {
                options = new TextOptions()
                {
                    FontOptions = this.documentOptions.TextFontOptions
                };
            }

            var pdfFont = this.getPdfFont(options.FontOptions);
            var textWidth = this.contents.DrawText(pdfFont, options.FontOptions.FontSize, this.pagePosition.X, this.pagePosition.Y, TextJustify.Left, DrawStyle.Normal, options.FontOptions.FontColor, text);
            this.pagePosition.X += textWidth;
        }

        /// <summary>
        /// Add paragraph of text at the current page's vertical position
        /// </summary>
        /// <param name="text"></param>
        /// <param name="options"></param>
        public void AddParagraph(string text, ParagraphOptions options = null)
        {
            this.checkBuilderState();

            if (options == null)
            {
                options = new ParagraphOptions()
                {
                    FontOptions = this.documentOptions.ParagraphFontOptions
                };
            }

            // convert TextAlignment to TextBoxJustify
            var texBoxJustify = Helpers.Convert.ToTextBoxJustify(options.TextAlignment);

            this.pagePosition.X = this.documentOptions.MarginLeft;
            double width = this.pageDimensions.Width - (this.documentOptions.MarginLeft + this.documentOptions.MarginRight);

            var pdfFont = this.getPdfFont(options.FontOptions);
            TextBox textBox = new TextBox(width, 0, 0.5);
            textBox.AddText(pdfFont, options.FontOptions.FontSize, DrawStyle.Normal, options.FontOptions.FontColor, text, (AnnotAction)null);

            // initial Y position
            double positionY = this.pagePosition.Y + (options.FontOptions.FontSize / this.document.ScaleFactor);  

            this.contents.DrawText(this.documentOptions.MarginLeft, ref positionY, this.documentOptions.MarginBottom, 0, 0.15, 0, texBoxJustify, textBox);

            this.pagePosition.Y = positionY;
            this.NewLine();
        }

        /// <summary>
        /// Add an image to the current page with a specified width
        /// </summary>
        /// <param name="path">Image file path</param>
        /// <param name="width">Target width for image in mm</param>
        /// <param name="options">ImageOptions</param>
        public void AddImage(string path, double width, ImageOptions options = null)
        {
            this.checkBuilderState();

            if (options == null)
            {
                options = new ImageOptions()
                {
                    PositionX = this.PagePositionX,
                    PositionY = this.PagePositionY
                };
            }

            // load the image from size and determine it new pixel dimensions based on relative resolutions 
            var srcBitmap = new Bitmap(path);
            double srcBitmapAspectRatio = srcBitmap.Size.Height / (srcBitmap.Size.Width * 1.0);
            double widthInInches = width / 25.4;
            int widthInPixels = Convert.ToInt32(Math.Round(widthInInches * srcBitmap.HorizontalResolution));
            int heightInPixels = Convert.ToInt32(Math.Round(widthInPixels * srcBitmapAspectRatio));

            // resize the source image and load into pdf
            var targetBitmap = Helpers.Image.Resize(srcBitmap, widthInPixels, heightInPixels, Color.White);

            PdfImage pdfImage = new PdfImage(this.document);
            pdfImage.Resolution = options.Resolution;
            pdfImage.ImageQuality = options.Quality;
            pdfImage.LoadImage(targetBitmap);

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
        /// Add a table to the current page
        /// </summary>
        /// <param name="table"></param>
        /// <param name="options"></param>
        public void AddTable(Table table, TableOptions options = null)
        {
            this.checkBuilderState();

            if (options == null)
            {
                options = new TableOptions();
            }

            PdfTable pdfTable = new PdfTable(this.page, this.contents);

            // calculate table area relative to the page dimensions and current page position 
            double left = this.documentOptions.MarginLeft;
            double bottom = this.documentOptions.MarginBottom;
            double right = (this.page.Width / this.page.ScaleFactor) - this.documentOptions.MarginRight;
            double top = this.pagePosition.Y;
            pdfTable.TableArea = new PdfRectangle(left, bottom, right, top);

            // set table columns widths 
            if (options.ColumnWidths == null)
            {
                options.ColumnWidths = new List<double>();
                foreach (var cell in table.HeaderRow.Cells)
                {
                    // by default all column widths will be same size 
                    options.ColumnWidths.Add(1.0);
                }
            }
            pdfTable.SetColumnWidth(options.ColumnWidths.ToArray());

            // set border widths and colors 
            double borderHeaderWidth = options.BorderHeader.BorderWidth / this.document.ScaleFactor;
            double borderTopWidth = options.BorderTop.BorderWidth / this.document.ScaleFactor;
            double borderBottomWidth = options.BorderBottom.BorderWidth / this.document.ScaleFactor;
            double borderHorizontalWidth = options.BorderHorizontal.BorderWidth / this.document.ScaleFactor;
            double borderVerticalWidth = options.BorderVertical.BorderWidth / this.document.ScaleFactor;

            pdfTable.Borders.ClearAllBorders();

            if (borderHeaderWidth > 0.0)
            {
                pdfTable.Borders.HeaderHorBorder.Set(borderHeaderWidth, options.BorderHeader.BorderColor);
            }
            
            if (borderTopWidth > 0.0)
            {
                pdfTable.Borders.TopBorder.Set(borderTopWidth, options.BorderTop.BorderColor);
            }
            
            if (borderBottomWidth > 0.0)
            {
                pdfTable.Borders.BottomBorder.Set(borderBottomWidth, options.BorderBottom.BorderColor);
            }
            
            if (borderHorizontalWidth > 0.0)
            {
                pdfTable.Borders.CellHorBorder.Set(borderHorizontalWidth, options.BorderHorizontal.BorderColor);
            }
            
            if (borderVerticalWidth > 0.0)
            {
                // vertical border lines
                pdfTable.Borders.HeaderVertBorder[0].Set(borderVerticalWidth, options.BorderVertical.BorderColor);
                pdfTable.Borders.CellVertBorder[0].Set(borderVerticalWidth, options.BorderVertical.BorderColor);
                for (int Index = 1; Index < pdfTable.Columns; Index++)
                {
                    pdfTable.Borders.HeaderVertBorder[Index].Set(borderVerticalWidth, options.BorderVertical.BorderColor);
                    pdfTable.Borders.CellVertBorder[Index].Set(borderVerticalWidth, options.BorderVertical.BorderColor);
                }
                pdfTable.Borders.HeaderVertBorder[pdfTable.Columns].Set(borderVerticalWidth, options.BorderVertical.BorderColor);
                pdfTable.Borders.CellVertBorder[pdfTable.Columns].Set(borderVerticalWidth, options.BorderVertical.BorderColor);
            }


            // default header styles  
            pdfTable.DefaultHeaderStyle.TextBoxTextJustify = TextBoxJustify.Left;
            pdfTable.DefaultHeaderStyle.Alignment = ContentAlignment.BottomLeft;
            pdfTable.DefaultHeaderStyle.BackgroundColor = Color.Transparent;
            //pdfTable.DefaultHeaderStyle.MultiLineText = true;
            pdfTable.DefaultHeaderStyle.TextBoxLineBreakFactor = 0.2;

            // default cell styles  
            pdfTable.DefaultCellStyle.TextBoxTextJustify = TextBoxJustify.Left;
            pdfTable.DefaultCellStyle.Alignment = ContentAlignment.BottomLeft;
            pdfTable.DefaultCellStyle.BackgroundColor = Color.Transparent;
            //pdfTable.DefaultCellStyle.MultiLineText = true;
            pdfTable.DefaultCellStyle.TextBoxLineBreakFactor = 0.2;
            //pdfTable.DefaultCellStyle.MinHeight = 2.0;

            // header
            /*
            pdfTable.DefaultHeaderStyle.Alignment = ContentAlignment.MiddleCenter;
            pdfTable.DefaultHeaderStyle.FontSize = 9.0;
            pdfTable.DefaultHeaderStyle.MultiLineText = true;
            pdfTable.DefaultHeaderStyle.TextBoxTextJustify = TextBoxJustify.Center;
            pdfTable.DefaultHeaderStyle.BackgroundColor = Color.Blue;
            pdfTable.DefaultHeaderStyle.ForegroundColor = Color.LightCyan;
            pdfTable.DefaultHeaderStyle.TextBoxLineBreakFactor = 0.2;
            */

            // header columns 
            for (int index = 0; index < options.ColumnWidths.Count; index++)
            {
                var cell = table.HeaderRow.Cells[index];

                pdfTable.Header[index].Style = new PdfTableStyle()
                {
                    Font = this.getPdfFont(cell.Options.FontOptions),
                    Alignment = Helpers.Convert.ToContentAlignment(cell.Options.TextAlignment),
                    FontSize = cell.Options.FontOptions.FontSize,
                    ForegroundColor = cell.Options.FontOptions.FontColor,
                    BackgroundColor = cell.Options.BackgroundColor,
                    Margin = new PdfRectangle(cell.Options.CellPadding)
                };

                pdfTable.Header[index].Type = CellType.Text;
                pdfTable.Header[index].Value = cell.Text;
            }

            // rows 
            foreach (var row in table.Rows)
            {
                for (int index = 0; index < options.ColumnWidths.Count; index++)
                {
                    var cell = row.Cells[index];

                    pdfTable.Cell[index].Style = new PdfTableStyle()
                    {
                        Font = this.getPdfFont(cell.Options.FontOptions),
                        Alignment = Helpers.Convert.ToContentAlignment(cell.Options.TextAlignment),
                        FontSize = cell.Options.FontOptions.FontSize,
                        ForegroundColor = cell.Options.FontOptions.FontColor,
                        BackgroundColor = cell.Options.BackgroundColor,
                        Margin = new PdfRectangle(cell.Options.CellPadding)
                    };

                    pdfTable.Cell[index].Type = CellType.Text;
                    pdfTable.Cell[index].Value = cell.Text;
                    pdfTable.Cell[index].CellHeight = 10.0 / this.document.ScaleFactor;
                }

                pdfTable.DrawRow();
            }

            pdfTable.Close(); 
        }


        /// <summary>
        /// Add a line of specific length to thepage at the current page position
        /// </summary>
        /// <param name="length">Length of line in mm</param>
        /// <param name="options"></param>
        public void AddLine(double length, LineOptions options = null)
        {
            if (options == null)
            {
                options = new LineOptions();
            }
            var positionStart = new Position(this.pagePosition.X, this.pagePosition.Y);
            var postionEnd = new Position(this.pagePosition.X + length, this.pagePosition.Y);
            var lineWidth = options.LineWidth / this.document.ScaleFactor;

            this.contents.SaveGraphicsState();
            this.contents.SetLineWidth(lineWidth);
            this.contents.SetColorStroking(options.LineColor);
            this.contents.DrawLine(positionStart.X, positionStart.Y, postionEnd.X, postionEnd.Y);
            this.contents.RestoreGraphicsState();
        }

        /// <summary>
        /// Get the PdfFont that best matches the specified TextFontOptions
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        private PdfFont getPdfFont(TextFontOptions options)
        {
            string fontKey = options.FontFamily.Value.Replace(" ", string.Empty).ToUpper();
            System.Drawing.FontStyle drawingFontStyle;
            switch (options.FontStyle)
            {
                case TextFontStyle.Normal:
                    fontKey += "-R";
                    drawingFontStyle = System.Drawing.FontStyle.Regular;
                    if (options.FontWeight == TextFontWeight.Bold)
                    {
                        drawingFontStyle = System.Drawing.FontStyle.Bold;
                        fontKey += "-B";
                    }
                    break;

                case TextFontStyle.Italic:
                    fontKey += "-I";
                    drawingFontStyle = System.Drawing.FontStyle.Italic;
                    if (options.FontWeight == TextFontWeight.Bold)
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
                var pdfFont = PdfFont.CreatePdfFont(this.document, options.FontFamily.Value, drawingFontStyle, true);
                this.fontDictionary.Add(fontKey, pdfFont);
            }

            return this.fontDictionary[fontKey];
        }

        /// <summary>
        /// Check the state of the builder and throw an exception if any issues found 
        /// </summary>
        private void checkBuilderState()
        {
            if (this.document == null)
            {
                throw new System.Exception("PdfBuilder.Open() must be called before adding content");
            }
        }

    }
}
