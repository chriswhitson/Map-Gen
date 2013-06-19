using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ImageGenerator.Core;

namespace ImageGenerator.Processors
{
    class ThreadedDistorter : IProcessor
    {       

        public GeneratedImage Process(GeneratedImage image)
        {
            GeneratedImage directions = new Generators.Noise().Generate(image.EnumSize);
            int offset = image.IntSize / 6;
            directions.Normalize();

            int cellCount = image.MaxIndex / 16;
            Action[] action = new Action[cellCount * cellCount];
            int size = image.MaxIndex / cellCount;
            int counter = 0;

            Barrier barrier = new Barrier(cellCount * cellCount);

            for (int x = 0; x < image.MaxIndex; x += size)
            {
                for (int y = 0; y < image.MaxIndex; y += size)
                {
                    int top = y, bottom = y + size, left = x, right = x + size;
                    action[counter++] = () => CalculateArea(image, directions, top, bottom, left, right, offset);
                }
            }

            Parallel.Invoke(action);
            barrier.Dispose();

            return directions;
        }

        private void CalculateArea(GeneratedImage image, GeneratedImage directions, int top, int bottom, int left, int right, int offset)
        {
            for (int xx = left; xx < right; ++xx)
            {
                for (int yy = top; yy < bottom; ++yy)
                {
                    double radians = directions.PixelData[xx, yy] * 2 * Math.PI;
                    double xOffset = offset * CalculateXComponent(radians);
                    double yOffset = offset * CalculateYComponent(radians);
                    directions.PixelData[xx, yy] = CalculateValue(image, xx + xOffset, yy + yOffset);
                }
            }
        }

        private double CalculateXComponent(double radians)
        {
            return Math.Cos(radians);
        }

        private double CalculateYComponent(double radians)
        {
            return Math.Sin(radians);
        }

        private byte CalculateValue(GeneratedImage image, double x, double y)
        {
            //wrap values since noise
            x = (x + image.MaxIndex) % image.MaxIndex;
            y = (y + image.MaxIndex) % image.MaxIndex;

            return (byte)image.GetInterpolatedValue(x, y);
        }
    }
}
