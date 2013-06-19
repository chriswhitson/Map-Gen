using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGenerator.Processors
{
    class Binary : IProcessor
    {
        public Core.GeneratedImage Process(Core.GeneratedImage image, byte threshold)
        {
            for (int x = 0; x < image.IntSize; ++x)
            {
                for (int y = 0; y < image.IntSize; ++y)
                {
                    if (image.PixelData[x, y] > threshold)
                    {
                        image.PixelData[x, y] = byte.MaxValue;
                    }
                    else
                    {
                        image.PixelData[x, y] = 0;
                    }

                }
            }

            return image;
        }
    }
}
