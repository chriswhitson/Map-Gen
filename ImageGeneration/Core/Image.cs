using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageGenerator.Core;

namespace ImageGenerator.Core
{
    public class Image
    {
        public PixelArray PixelData { get; protected set; }

        public Image(int width, int height)
        {
            PixelData = new PixelArray(width, height);
        }
    }
}
