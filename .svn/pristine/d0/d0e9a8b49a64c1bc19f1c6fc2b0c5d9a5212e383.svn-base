using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace APIRPA.bl
{
    public class ImageHelper
    {
        public static Image ResizeImg(Image originalImage)
        {
            Image resizedImage = null;

            if (originalImage == null)
            {

            }
            else
            {
                // ! DPI = PIXELS / INCHES
                // ! 300DPI = sizeH / INCHES

                // ! INCHES = sizeH / 300DPI

                // ! OBTENEMOS LA DIMENSION EN PIXELES
                var sizeH = originalImage.Height;
                var sizeW = originalImage.Width;
                // ! CALCULAMOS LA DIMENSION EN PULGADAS A 300DPI
                var inchH = sizeH / 300;
                var inchW = sizeW / 300;
                using (var xx = new Bitmap(inchW, inchH))
                {
                    using (var graphics = Graphics.FromImage(originalImage))
                    {
                        graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                        graphics.DrawImage(originalImage, 0, 0, inchW, inchH);
                    }
                }
                // ! RESIZE CON DIMENSION APROPIADA
                Bitmap resizedBmp = new Bitmap(originalImage, inchW, inchH);

                resizedImage = resizedBmp;

            }

            return resizedImage;
        }
    }
}