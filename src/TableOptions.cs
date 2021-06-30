using System.Collections.Generic;
using System.Drawing;

namespace SyntaxSolutions.PdfBuilder
{
    /// <summary>
    /// Options for rendering a table 
    /// </summary>
    public class TableOptions
    {
        /// <summary>
        /// Get or set table header border
        /// </summary>
        public TableBorderOptions BorderHeader { get; set; }

        /// <summary>
        /// Get or set table top border
        /// </summary>
        public TableBorderOptions BorderTop { get; set; }

        /// <summary>
        /// Get or set table bottom border
        /// </summary>
        public TableBorderOptions BorderBottom { get; set; }

        /// <summary>
        /// Get or set table horizontal borders
        /// </summary>
        public TableBorderOptions BorderHorizontal { get; set; }

        /// <summary>
        /// Get or set table vertical borders
        /// </summary>
        public TableBorderOptions BorderVertical { get; set; }



        /// <summary>
        /// Get or set table column widths
        /// </summary>
        public List<double> ColumnWidths { get; set; }

        /// <summary>
        /// Create a TextOptions with defaults
        /// </summary>
        public TableOptions()
        {
            this.BorderHeader = new TableBorderOptions();
            this.BorderTop = new TableBorderOptions();
            this.BorderBottom = new TableBorderOptions();
            this.BorderHorizontal = new TableBorderOptions();
            this.BorderVertical = new TableBorderOptions();
            this.ColumnWidths = null;
        }


        /// <summary>
        /// Return a TableOptions with parameters to set specific properties
        /// </summary>
        /// <param name="BorderHeaderWidth"></param>
        /// <param name="BorderHeaderColor"></param>
        /// <param name="BorderTopWidth"></param>
        /// <param name="BorderTopColor"></param>
        /// <param name="BorderBottomWidth"></param>
        /// <param name="BorderBottomColor"></param>
        /// <param name="BorderHorizontalWidth"></param>
        /// <param name="BorderHorizontalColor"></param>
        /// <param name="BorderVerticalWidth"></param>
        /// <param name="BorderVerticalColor"></param>
        /// <param name="ColumnWidths"></param>
        /// <returns></returns>
        public static TableOptions Set(

            double? BorderHeaderWidth = null,
            Color? BorderHeaderColor = null,

            double? BorderTopWidth = null,
            Color? BorderTopColor = null,

            double? BorderBottomWidth = null,
            Color? BorderBottomColor = null,

            double? BorderHorizontalWidth = null,
            Color? BorderHorizontalColor = null,

            double? BorderVerticalWidth = null,
            Color? BorderVerticalColor = null,

            List<double> ColumnWidths = null
        )
        {
            var value = new TableOptions();

            if (BorderHeaderWidth.HasValue)
            {
                value.BorderHeader.BorderWidth = BorderHeaderWidth.Value;
            }

            if (BorderHeaderColor.HasValue)
            {
                value.BorderHeader.BorderColor = BorderHeaderColor.Value;
            }


            if (BorderTopWidth.HasValue)
            {
                value.BorderTop.BorderWidth = BorderTopWidth.Value;
            }

            if (BorderTopColor.HasValue)
            {
                value.BorderTop.BorderColor = BorderTopColor.Value;
            }


            if (BorderBottomWidth.HasValue)
            {
                value.BorderBottom.BorderWidth = BorderBottomWidth.Value;
            }

            if (BorderBottomColor.HasValue)
            {
                value.BorderBottom.BorderColor = BorderBottomColor.Value;
            }


            if (BorderHorizontalWidth.HasValue)
            {
                value.BorderHorizontal.BorderWidth = BorderHorizontalWidth.Value;
            }

            if (BorderHorizontalColor.HasValue)
            {
                value.BorderHorizontal.BorderColor = BorderHorizontalColor.Value;
            }


            if (BorderVerticalWidth.HasValue)
            {
                value.BorderVertical.BorderWidth = BorderVerticalWidth.Value;
            }

            if (BorderVerticalColor.HasValue)
            {
                value.BorderVertical.BorderColor = BorderVerticalColor.Value;
            }


            if (ColumnWidths != null)
            {
                value.ColumnWidths = ColumnWidths;
            }

            return value;
        }
    }
}
