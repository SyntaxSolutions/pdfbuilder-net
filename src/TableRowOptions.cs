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
        /// Create a TextOptions with defaults
        /// </summary>
        public TableRowOptions()
        {
        }

        /// <summary>
        /// Return a TableRowOptions with parameters to set specific properties
        /// </summary>
        /// <returns></returns>
        public static TableRowOptions Set(
        )
        {
            var value = new TableRowOptions();

            return value;
        }
    }
}
