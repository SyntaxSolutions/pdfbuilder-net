using System.Drawing;
using PdfFileWriter;

namespace SyntaxSolutions.PdfBuilder.Helper
{
    /// <summary>
    /// Collection of conversion static helper methods
    /// </summary>
    internal class Convert
    {
        /// <summary>
        /// Convert the specified TextAlignment to a corresponding ContentAlignment
        /// </summary>
        /// <param name="textAligment"></param>
        /// <returns></returns>
        public static ContentAlignment ToContentAlignment(TextAlignment textAligment)
        {
            var value = ContentAlignment.BottomLeft;
            switch (textAligment)
            {
                case TextAlignment.Center:
                    value = ContentAlignment.BottomCenter;
                    break;

                case TextAlignment.Right:
                    value = ContentAlignment.BottomRight;
                    break;
            }

            return value;
        }

        /// <summary>
        /// Convert the specified TextAlignment to a corresponding TextBoxJustify
        /// </summary>
        /// <param name="textAligment"></param>
        /// <returns></returns>
        public static TextBoxJustify ToTextBoxJustify(TextAlignment textAligment)
        {
            var value = TextBoxJustify.Left;
            switch (textAligment)
            {
                case TextAlignment.Center:
                    value = TextBoxJustify.Center;
                    break;

                case TextAlignment.Right:
                    value = TextBoxJustify.Right;
                    break;

                case TextAlignment.Justify:
                    value = TextBoxJustify.FitToWidth;
                    break;
            }

            return value;
        }
    }
}
