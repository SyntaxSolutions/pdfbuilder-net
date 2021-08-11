using System.Collections.Generic;
using System.Drawing;

namespace SyntaxSolutions.PdfBuilder
{
    /// <summary>
    /// Options for rendering a table row
    /// </summary>
    public class TableRowOptions
    {
        /// <summary>
        /// TableCellOptions
        /// </summary>
        public TableCellOptions TableCellOptions { get; set; }

        /// <summary>
        /// Create a TextOptions with defaults
        /// </summary>
        public TableRowOptions()
        {
            this.TableCellOptions = new TableCellOptions();
        }

        /// <summary>
        /// Return a TableRowOptions with parameters to set specific properties
        /// </summary>
        /// <param name="TableCellOptions"></param>
        /// <returns></returns>
        public static TableRowOptions Set(
            TableCellOptions TableCellOptions = null
        )
        {
            var value = new TableRowOptions();

            if (TableCellOptions != null)
            {
                value.TableCellOptions = TableCellOptions;
            }

            return value;
        }
    }
}
