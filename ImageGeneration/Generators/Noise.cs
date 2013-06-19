using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageGenerator.Core;

namespace ImageGenerator.Generators
{
    public class Noise : IImageGenerator
    {
        private Random random;

        public Noise()
        {
            random = new Random();
        }

        public GeneratedImage Generate(GeneratedImage.ImageSize imageSize)
        {
            GeneratedImage result = new GeneratedImage(imageSize);
            int size = result.IntSize;
            int lastIndex = result.MaxIndex;
            byte value = (byte)random.Next(byte.MaxValue);

            result.PixelData[0, 0] = value;
            result.PixelData[lastIndex, 0] = value;
            result.PixelData[0, lastIndex] = value;
            result.PixelData[lastIndex, lastIndex] = value;

            double offset = byte.MaxValue;
            for (int sideLength = lastIndex; sideLength >= 2; sideLength /= 2, offset /= 2)
            {
                int halfLength = sideLength / 2;

                for (int x = halfLength; x < lastIndex; x += sideLength)
                {
                    for (int y = halfLength; y < lastIndex; y += sideLength)
                    {
                        Square(result, x, y, halfLength, offset);
                    }
                }

                for (int x = 0; x < lastIndex; x += halfLength)
                {
                    for (int y = (x + halfLength) % sideLength; y < lastIndex; y += sideLength)
                    {
                        Diamond(result, x, y, halfLength, offset);
                    }
                }
            }
            result.Normalize();
            return result;
        }

        private void Square(GeneratedImage img, int x, int y, int halfSize, double offset)
        {
            int t = y - halfSize;
            int b = y + halfSize;
            int l = x - halfSize;
            int r = x + halfSize;

            byte value = (byte)((img.PixelData[l, t] + img.PixelData[r, t] + img.PixelData[l, b] + img.PixelData[r, b]) / 4.0);
            value += (byte)(random.NextDouble() * 2 * offset - offset);
            img.PixelData[x, y] = value;
        }

        private void Diamond(GeneratedImage img, int x, int y, int halfSize, double offset)
        {
            int t = (y - halfSize + img.MaxIndex) % img.MaxIndex;
            int b = (y + halfSize) % img.MaxIndex;
            int l = (x - halfSize + img.MaxIndex) % img.MaxIndex;
            int r = (x + halfSize) % img.MaxIndex;

            byte value = (byte)((img.PixelData[x, t] + img.PixelData[x, b] + img.PixelData[l, y] + img.PixelData[r, y]) / 4.0);
            value += (byte)(random.NextDouble() * 2 * offset - offset);
            img.PixelData[x, y] = value;

            if (x == 0) { img.PixelData[img.MaxIndex, y] = value; }
            if (y == 0) { img.PixelData[x, img.MaxIndex] = value; }
        }
    }
}
