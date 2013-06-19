using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ImageGenerator.Core;

namespace ImageLibrary
{
    public class ImageWriter
    {
        public static void createImage(ImageGenerator.Core.Image image)
        {
            Bitmap bitmap = new Bitmap(image.PixelData.Width, image.PixelData.Height, PixelFormat.Format24bppRgb);

            Rectangle area = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData data = bitmap.LockBits(area, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            IntPtr ptr = data.Scan0;
            int byteCount = data.Stride * bitmap.Height;
            byte[] imageData = new byte[byteCount];
            int counter = 0;
            int strideBuffer = getStrideBufferSize(bitmap.Width * 3);

            double max = Double.MinValue;
            double min = Double.MaxValue;

            for (int i = 0; i < image.PixelData.Width; ++i)
            {
                for (int j = 0; j < image.PixelData.Height; ++j)
                {
                    if (image.PixelData[i, j] > max) { max = image.PixelData[i, j]; }
                    if (image.PixelData[i, j] < min) { min = image.PixelData[i, j]; }
                }
            }

            double range = max - min;
            double scale = 255.0 / range;
            
            for (int x = 0; x < image.PixelData.Width; x++)
            {
                for (int y = 0; y < image.PixelData.Height; y++)
                {
                    byte v = (byte)((image.PixelData[y, x] - min) * scale);
                    imageData[counter++] = v;
                    imageData[counter++] = v;
                    imageData[counter++] = v;
                }
                counter += strideBuffer;
            }

            Marshal.Copy(imageData, 0, ptr, byteCount);

            bitmap.UnlockBits(data);


            bitmap.Save(@"C://Users/Chris/Desktop/foo.png", ImageFormat.Png);
        }

        private static int getStrideBufferSize(int targetWidth)
        {
            return 4 - targetWidth % 4;
        }

       
    }
}