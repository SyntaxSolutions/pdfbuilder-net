using System.Collections.Generic;
using System.Drawing;

namespace SyntaxSolutions.PdfBuilder
{
    /// <summary>
    /// Table
    /// </summary>
    public class Table
    {
        /// <summary>
        /// TableOptions
        /// </summary>
        public TableOptions Options { get; set; }

        /// <summary>
        /// List of rows for this Table 
        /// </summary>
        public List<TableRow> Rows { get; set; }

        /// <summary>
        /// Header TableRow for this Table
        /// </summary>
        public TableRow HeaderRow { get; set; }

        /// <summary>
        ///  Create a new Table
        /// </summary>
        public Table() : this(null) { }

        /// <summary>
        /// Create a new Table
        /// </summary>
        /// <param name="options"></param>
        public Table(TableOptions options = null)
        {
            if (options == null)
            {
                options = new TableOptions();
            }
            this.Options = options;
            this.Rows = new List<TableRow>();
            this.HeaderRow = new TableRow();
        }

        /// <summary>
        /// Add TableRow which will be used for this Table's column headers 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="options"></param>
        public void AddHeaders(TableRow row, TableRowOptions options = null)
        {
            if (options == null)
            {
                options = new TableRowOptions();
            }
            this.HeaderRow = row;
            this.HeaderRow.Options = options;
        }

        /// <summary>
        /// Add a table row 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="options"></param>
        public void AddRow(TableRow row, TableRowOptions options = null)
        {
            if (options == null)
            {
                options = new TableRowOptions();
            }
            row.Options = options;
            this.Rows.Add(row);
        }
    }
}
