﻿using System.Collections.Generic;
using System;
using System.Drawing;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyntaxSolutions.PdfBuilder;

namespace PdfBuilder_tests
{
    [TestClass]
    public class UnitTests
    {
        private void PageHeader(PdfBuilder builder, PageHeaderEventArgs e)
        {
            builder.AddText("...PAGE HEADER...");
            builder.NewLine();
        }

        private void PageFooter(PdfBuilder builder, PageFooterEventArgs e)
        {
            var postionY = builder.PagePositionY; 
            builder.PagePositionY = 10; 
            builder.AddText(String.Format("...PAGE FOOTER... | Page: {0}", builder.PageCount.ToString()));
            builder.PagePositionY = postionY;
        }

        [TestMethod]
        public void TestAddTitle()
        {
            var options = new DocumentOptions()
            {
                PageSize = PageSize.A4,
                PageOrientation = PageOrientation.Portrait
            };
            var builder = new PdfBuilder(options);

            builder.PageHeader += new PageHeaderEventHandler(this.PageHeader);
            builder.PageFooter += new PageFooterEventHandler(this.PageFooter);

            builder.Open();

            //
            // Fonts
            //
            builder.NewPage();
            builder.NewLine();
            builder.AddTitle("Fonts");
            builder.NewLine();

            builder.AddHeading("Styles");
            builder.AddText("Times New Roman - Normal");
            builder.NewLine();

            builder.AddText("Times New Roman - Bold", TextOptions.Set(FontWeight: TextFontWeight.Bold));

            builder.NewLine();

            builder.AddText("Times New Roman - Italic", TextOptions.Set(FontStyle: TextFontStyle.Italic));
            builder.NewLine();

            builder.AddText("Times New Roman - Italic Bold", TextOptions.Set(FontStyle: TextFontStyle.Italic, FontWeight: TextFontWeight.Bold));
            builder.NewLine();

            builder.AddText("Arial - Normal", TextOptions.Set(FontFamily: TextFontFamily.Arial));
            builder.NewLine();

            builder.AddText("Arial - Bold", TextOptions.Set(FontFamily: TextFontFamily.Arial, FontWeight: TextFontWeight.Bold));
            builder.NewLine();

            builder.AddText("Arial - Italic", TextOptions.Set(FontFamily: TextFontFamily.Arial, FontStyle: TextFontStyle.Italic));
            builder.NewLine();

            builder.AddText("Arial - Italic Bold", TextOptions.Set(FontFamily: TextFontFamily.Arial, FontStyle: TextFontStyle.Italic, FontWeight: TextFontWeight.Bold));
            builder.NewLine();
            builder.NewLine();

            builder.AddHeading("Colors");
            builder.AddText("Red", TextOptions.Set(FontColor: Color.Red));
            builder.NewLine();
            builder.AddText("Green", TextOptions.Set(FontColor: Color.Green));
            builder.NewLine();
            builder.AddText("Blue", TextOptions.Set(FontColor: Color.Blue));
            builder.NewLine();


            //
            // Paragraphs
            //

            builder.NewPage();
            builder.NewLine();
            builder.AddTitle("Paragraphs");
            builder.NewLine();

            builder.AddHeading("Left Aligned");
            builder.AddParagraph("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.");
            builder.NewLine();

            builder.AddHeading("Center Aligned");
            builder.AddParagraph("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", ParagraphOptions.Set(TextAlignment: TextAlignment.Center));
            builder.NewLine();

            builder.AddHeading("Right Aligned");
            builder.AddParagraph("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", ParagraphOptions.Set(TextAlignment: TextAlignment.Right));
            builder.NewLine();

            builder.AddHeading("Justified Aligned");
            builder.AddParagraph("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", ParagraphOptions.Set(TextAlignment: TextAlignment.Justify));
            builder.NewLine();


            //
            // Images
            //

            builder.NewPage();
            builder.NewLine();
            builder.AddTitle("Images");
            builder.NewLine();

            string imagepath = @"Sunset.jpg";
            builder.AddImage(imagepath, 50);
            builder.NewLine();

            builder.AddImage(imagepath, 100);
            builder.NewLine();

            builder.AddImage(imagepath, 150);
            builder.NewLine();


            //
            // Tables
            //

            builder.NewPage();
            builder.NewLine();
            builder.AddTitle("Tables");
            builder.NewLine();

            var table = new Table();

            var headerCellOptions = TableCellOptions.Set(
                TextAlignment: TextAlignment.Center,
                FontSize: 14.0,
                FontWeight: TextFontWeight.Bold,
                FontFamily: TextFontFamily.Arial,
                BackgroundColor: Color.LightGray
            );

            var headers = new TableRow();
            headers.AddCell("Header 1", headerCellOptions);
            headers.AddCell("Header 2", headerCellOptions);
            headers.AddCell("Header 3", headerCellOptions);
            table.AddHeaders(headers);

            var defaultCellOptions = TableCellOptions.Set(
                FontColor: Color.DarkGray
            );

            var row1 = new TableRow();
            row1.AddCell("Row 1", defaultCellOptions);
            row1.AddCell("Item 1", TableCellOptions.Set(FontColor: Color.Red));
            row1.AddCell("123.00", TableCellOptions.Set(TextAlignment: TextAlignment.Right, BackgroundColor: Color.LightGreen));
            table.AddRow(row1);

            var row2 = new TableRow();
            row2.AddCell("Row 2", defaultCellOptions);
            row2.AddCell("Item 2", TableCellOptions.Set(FontColor: Color.Red));
            row2.AddCell("123.00", TableCellOptions.Set(TextAlignment: TextAlignment.Right, BackgroundColor: Color.LightGreen));
            table.AddRow(row2);

            var row3 = new TableRow();
            row2.AddCell("Row 3", defaultCellOptions);
            row2.AddCell("Item 3", TableCellOptions.Set(FontColor: Color.Red));
            row2.AddCell("123.00", TableCellOptions.Set(TextAlignment: TextAlignment.Right, BackgroundColor: Color.LightGreen));
            table.AddRow(row2);

            var tableOptions = TableOptions.Set(
                BorderHeaderWidth: 1.0,
                BorderTopWidth: 0.0,
                BorderBottomWidth: 1.0,
                BorderHorizontalWidth: 0.8,
                BorderVerticalWidth: 0.6, 
                BorderVerticalColor: Color.DarkGray,
                ColumnWidths: new List<double>(new double[] { 1.0, 2.0, 1.0 })
            );
            builder.AddTable(table, tableOptions);


            // save file 
            var guid = System.Guid.NewGuid();
            string filepath = String.Format("TestPdfBuilder_{0}.pdf", guid.ToString("N"));

            using (var fileStream = new FileStream(filepath, FileMode.Create, FileAccess.Write))
            {
                byte[] data = builder.GetBytes();
                fileStream.Write(data, 0, data.Length);
            }

            builder.Close();

            Assert.IsTrue(1 == 1);
        }
    }
}
