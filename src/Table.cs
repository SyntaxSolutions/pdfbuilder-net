using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSolutions.PdfBuilder
{
    public class Table
    {
        /// <summary>
        /// List of rows for this Table 
        /// </summary>
        public List<TableRow> Rows { get; set; }

        /// <summary>
        /// Header TableRow for this Table
        /// </summary>
        public TableRow HeaderRow { get; set; }

        /// <summary>
        /// Create a new Table
        /// </summary>
        public Table()
        {
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
