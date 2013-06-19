using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageGenerator.Core;


namespace ImageGenerator.Generators
{
    public class Islands : IImageGenerator
    {
        Random rnd = new Random();

        public Islands()
        {
        }

        public GeneratedImage Generate(GeneratedImage.ImageSize imageSize)
        {
            GeneratedImage result = new GeneratedImage(imageSize);

            IList<Island> islands = CreateIslands(result);
            WriteIslands(result, islands);
            return result;
        }

        private IList<Island> CreateIslands(GeneratedImage image)
        {
            IList<Island> result = new List<Island>();
            int half = (image.MaxIndex - 1) / 2;
            int islandCount = rnd.Next(3, 9);
            for (int i = 0; i < islandCount; ++i)
            {
                result.Add(CreateIsland(image, image.CentreIndex, image.CentreIndex));
            }
                        
            return result;
        }

        private Island CreateIsland(GeneratedImage img, int x, int y)
        {
            int fraction = img.MaxIndex / 5;
            int radiusFraction = fraction / 10;

            // off set by twice radius to try and get a good distribution of islands
            int xDir = rnd.Next(-fraction, fraction);
            int yDir = rnd.Next(-fraction, fraction);
            int radius = rnd.Next(radiusFraction, radiusFraction + radiusFraction);
            Console.WriteLine(xDir + " " + yDir + " " + 5);

            return new Island(x + xDir, y + yDir, radiusFraction);
        }

        private void WriteIslands(Image img, IList<Island> islands)
        {
            double level1 = 1;
            double level2 = 0.5;
            for (int x = 0; x < img.PixelData.Width; ++x)
            {
                for (int y = 0; y < img.PixelData.Height; ++y)
                {
                    double value = 0.0;
                    foreach (Island i in islands)
                    {
                        value += i.Value(x, y);
                    }

                    if (value >= level1)
                    {
                        img.PixelData[x, y] = 1;
                    }
                    else if (value >= level2)
                    {
                        img.PixelData[x, y] = (byte)((value - level2) / (level1 - level2));
                    }
                    else
                    {
                        img.PixelData[x, y] = 0;
                    }
                }
            }
        }
        
        struct Island
        {
            public int X;
            public int Y;
            public double Radius;

            public Island(int x, int y, double radius)
            {
                X = x;
                Y = y;
                Radius = radius;
            }

            public double Value(int x, int y)
            {
                double value = Radius / Math.Sqrt((x - X) * (x - X) + (y - Y) * (y - Y));
                return value;
            }
        }
    }
}
