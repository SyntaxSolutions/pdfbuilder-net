using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSolutions.PdfBuilder
{
    /// <summary>
    /// A cell with in a table cell
    /// </summary>
    public class TableCell
    {
        /// <summary>
        /// Get or set TableCellOptions
        /// </summary>
        public TableCellOptions Options { get; set; }

        /// <summary>
        /// Get or set cell text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Create a new TableRow
        /// </summary>
        public TableCell(string text)
        {
            this.Options = new TableCellOptions();
            this.Text = text;
        }
    }
}
