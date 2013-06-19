using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageGenerator.Core;

namespace ImageGenerator.Processors
{
    class Distorter : IProcessor
    {
        public GeneratedImage Process(GeneratedImage image)
        {
            //
            GeneratedImage directions = new Generators.Noise().Generate(image.EnumSize);
            directions.Normalize();

            int offset = image.IntSize / 8;
            for (int x = 0; x < image.IntSize; ++x)
            {
                for (int y = 0; y < image.IntSize; ++y)
                {
                    double radians = directions.PixelData[x, y] * 2 * Math.PI / byte.MaxValue;
                    //System.Console.WriteLine(radians);
                    double xOffset = offset * CalculateXComponent(radians);
                    double yOffset = offset * CalculateYComponent(radians);
                    directions.PixelData[x, y] = (byte)CalculateValue(image, x + xOffset, y + yOffset);
                }
            }
            return directions;
        }

        private double CalculateXComponent(double radians)
        {
            return Math.Cos(radians);
        }

        private double CalculateYComponent(double radians)
        {
            return Math.Sin(radians);
        }

        private double CalculateValue(GeneratedImage image, double x, double y)
        {
            //wrap values since noise
            x = (x + image.MaxIndex) % image.MaxIndex;
            y = (y + image.MaxIndex) % image.MaxIndex;

            return image.GetInterpolatedValue(x, y);
        }


        
    }
}
