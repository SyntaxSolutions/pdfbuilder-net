using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace SyntaxSolutions.PdfBuilder.Helper
{
    internal class ImageHelper
    {
        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <param name="backgroundColor">Background color to render the image against to replace any transparent pixels</param>
        /// <returns>The resized image.</returns>
        public static Bitmap resizeImage(Image image, int width, int height, Color backgroundColor)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingQuality = CompositingQuality.Default;
                graphics.CompositingMode = CompositingMode.SourceCopy;

                if (backgroundColor != Color.Transparent)
                {
                    graphics.Clear(backgroundColor);
                    graphics.CompositingMode = CompositingMode.SourceOver;
                }

                graphics.InterpolationMode = InterpolationMode.Default;
                graphics.PixelOffsetMode = PixelOffsetMode.Default;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
