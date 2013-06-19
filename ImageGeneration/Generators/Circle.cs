using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageGenerator.Core;

namespace ImageGenerator.Generators
{
    public class Circle : IImageGenerator
    {
        public Circle()
        {
        }

        public GeneratedImage Generate(GeneratedImage.ImageSize imageSize)
        {            
            GeneratedImage result = new GeneratedImage(imageSize);

            int size = result.IntSize;
            int radius = size / 4;
            int centreIndex = result.CentreIndex;

            // since we know what size the circle is, we only have to draw the pixels we know are effected
            int min = centreIndex - radius - 1;
            int max = centreIndex + radius + 1;

            for (int x = min; x < max; ++x)
            {
                double xComp = (centreIndex - x) * (centreIndex - x);
                for (int y = min; y < max; ++y)
                {
                    double distanceFromCentre = Math.Sqrt(xComp + (centreIndex - y) * (centreIndex - y));
                    if (distanceFromCentre <= radius)
                    {
                        byte value = (byte)((radius - distanceFromCentre) * byte.MaxValue / radius) ;
                        result.PixelData[x, y] = value;
                    }                    
                }
            }

            return result;
        }
    }
}
