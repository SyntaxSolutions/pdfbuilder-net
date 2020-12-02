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
        [TestMethod]
        public void TestAddTitle()
        {
            var builder = new PdfBuilder();

            builder.Open();

            builder.NewPage();

            builder.PagePositionY -= 5; 

            builder.AddTitle("Fonts");

            builder.AddText("Times New Roman - Normal");
            builder.NewLine();

            builder.AddText("Times New Roman - Bold", TextOptions.Set(FontWeight: TextFontWeight.Bold, FontColor: Color.Red));

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

            builder.AddTitle("Paragraph");
            builder.AddParagraph("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.");
            builder.AddParagraph("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", TextOptions.Set(FontColor: System.Drawing.Color.Red));

            builder.NewPage();
            builder.AddHeading("Images");

            //string imagepath = @"TestImage.jpg";
            string imagepath = @"Sunset.jpg";
            builder.AddImage(imagepath, 50);
            builder.NewLine();

            builder.AddImage(imagepath, 100);
            builder.NewLine();

            builder.AddImage(imagepath, 150);
            builder.NewLine();


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
