using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSolutions.PdfBuilder
{
    /// <summary>
    /// Represents a single row in a table 
    /// </summary>
    public class TableRow
    {
        /// <summary>
        /// Get or set TableRowOptions
        /// </summary>
        public TableRowOptions Options { get; set; }

        /// <summary>
        /// List of cellls for this TableRow
        /// </summary>
        public List<TableCell> Cells { get; set; }

        /// <summary>
        /// Create a new TableRow
        /// </summary>
        public TableRow()
        {
            this.Options = new TableRowOptions();
            this.Cells = new List<TableCell>();
        }


        /// <summary>
        /// Add a TableCell with value to the TableRow
        /// </summary>
        /// <param name="text"></param>
        /// <param name="options"></param>
        public void AddCell(string text, TableCellOptions options = null)
        {
            if (options == null)
            {
                options = new TableCellOptions();
            }

            var cell = new TableCell(text) 
            { 
                Options = options 
            };
            this.Cells.Add(cell);
        }

        /// <summary>
        /// Add a TableCell to the TableRow
        /// </summary>
        /// <param name="tableCell"></param>
        public void AddCell(TableCell tableCell)
        {
            this.Cells.Add(tableCell);
        }
    }
}
