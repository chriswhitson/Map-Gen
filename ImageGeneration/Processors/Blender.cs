using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGenerator.Processors
{
    class Blender : IProcessor
    {

        public Core.GeneratedImage Process(Core.GeneratedImage a, Core.GeneratedImage b, double balance)
        {
            NormalizeImages(a, b);
            for (int x = 0; x < a.IntSize; ++x)
            {
                for (int y = 0; y < a.IntSize; ++y)
                {
                    a.PixelData[x, y] = (byte)((a.PixelData[x, y] * (1 - balance) + b.PixelData[x, y] * balance) / 2);
                }
            }
            a.Normalize();
            return a;
        }

        private void NormalizeImages(Core.GeneratedImage a, Core.GeneratedImage b)
        {
            a.Normalize();
            b.Normalize();
        }
    }
}
