using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSolutions.PdfBuilder
{
    /// <summary>
    /// An (X,Y) coordinate position
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Get X coordinate
        /// </summary>
        public double X { get; internal set; }

        /// <summary>
        /// Get Y coordinate
        /// </summary>
        public double Y { get; internal set; }

        /// <summary>
        /// An (X,Y) coordinate position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Position(double x, double y) 
        {
            this.X = x;
            this.Y = y;
        }

    }
}
