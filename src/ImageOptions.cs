﻿using System.Drawing;

namespace SyntaxSolutions.PdfBuilder
{
    /// <summary>
    /// Options for rendering an image
    /// </summary>
    public class ImageOptions
    {
        /// <summary>
        /// Resolution
        /// </summary>
        public double Resolution { get; set; }

        /// <summary>
        /// Quality
        /// </summary>
        public int Quality { get; set; }

        /// <summary>
        /// PositionX
        /// </summary>
        public double PositionX { get; set; }

        /// <summary>
        /// PositionY
        /// </summary>
        public double PositionY { get; set; }

        /// <summary>
        /// Create a TextOptions with defaults
        /// </summary>
        public ImageOptions()
        {
            this.Resolution = 0; // maximum
            this.Quality = 100; // 100%
            this.PositionX = 0;
            this.PositionY = 0;
        }
        
        /// <summary>
        /// Return a ImageOptions with parameters to set specific properties
        /// </summary>
        /// <param name="Resolution"></param>
        /// <param name="Quality"></param>
        /// <param name="PositionX"></param>
        /// <param name="PositionY"></param>
        /// <returns></returns>
        public static ImageOptions Set (
            double? Resolution = null,
            int? Quality = null,
            double? PositionX = null,
            double? PositionY = null
        )
        {
            var value = new ImageOptions();

            if (Resolution.HasValue)
            {
                value.Resolution = Resolution.Value;
            }

            if (Quality.HasValue)
            {
                value.Quality = Quality.Value;
            }

            if (PositionX.HasValue)
            {
                value.PositionX = PositionX.Value;
            }

            if (PositionY.HasValue)
            {
                value.PositionY = PositionY.Value;
            }

            return value;
        }
    }
}
