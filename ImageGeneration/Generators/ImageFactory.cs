using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageGenerator.Core;

namespace ImageGenerator.Generators
{
    public class ImageFactory
    {
        GeneratedImage.ImageSize size;

        private ImageFactory(GeneratedImage.ImageSize size)
        {
            this.size = size;
        }

        public ImageFactory createImageFactory(GeneratedImage.ImageSize size)
        {
            return new ImageFactory(size);
        }

        public GeneratedImage GenerateNoise()
        {
            return new Noise().Generate(size);
        }
    }
}
