using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGenerator.Core
{
    public class GeneratedImage : Image
    {
        public enum ImageSize : int
        {
            XSMALL = 129,
            SMALL = 257,
            MEDIUM = 513,
            LARGE = 1025,
            XLARGE = 2049
        }

        public ImageSize EnumSize { get; private set; }
        public int IntSize { get { return (int)EnumSize; } }
        public int MaxIndex { get { return IntSize - 1; } }
        public int CentreIndex { get { return MaxIndex / 2; } }

        // TODO should be removed once all are using bytes
        public bool Normalized { get; set; }

        public GeneratedImage(ImageSize size)
            :base((int)size, (int)size)
        {
            EnumSize = size;
        }

        public double GetInterpolatedValue(double x, double y)
        {
            int xFloor = (int)Math.Floor(x);
            int yFloor = (int)Math.Floor(y);

            double xFraction = x - xFloor;
            double yFraction = y - yFloor;

            double topValue = (PixelData[xFloor, yFloor] * (1 - xFraction)) + (PixelData[xFloor + 1, yFloor] * xFraction);
            double bottomValue = (PixelData[xFloor, yFloor + 1] * (1 - xFraction)) + (PixelData[xFloor + 1, yFloor + 1] * xFraction);

            return (topValue * (1 - yFraction)) + (bottomValue * yFraction);
        }

        // TASK this shouldn't be needed once all values use bytes
        public void Normalize()
        {
       /*     double min = double.MaxValue;
            double max = double.MinValue;

            if (!Normalized)
            {
                for (int x = 0; x < IntSize; ++x)
                {
                    for (int y = 0; y < IntSize; ++y)
                    {
                        if (PixelData[x, y] > max) { max = PixelData[x, y]; }
                        else if (PixelData[x, y] < min) { min = PixelData[x, y]; }
                    }
                }

                double range = max - min;
                for (int x = 0; x < IntSize; ++x)
                {
                    for (int y = 0; y < IntSize; ++y)
                    {
                        PixelData[x, y] = (byte)((PixelData[x, y] - min) / range);
                    }
                }
            }

            Normalized = true;
            */
        }
    }
}
