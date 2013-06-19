using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGenerator.Core
{
    public class IslandMap
    {
        public GeneratedImage Land { get; private set; }

        public IslandMap(GeneratedImage.ImageSize size)
        {
            InitialiseLand(size);
        }

        private void InitialiseLand(GeneratedImage.ImageSize size)
        {
            Processors.ImageProcessor processor = Processors.ImageProcessor.NewInstance(new Generators.Noise(), size);
            processor.Blend(new Generators.Circle().Generate(size), 0.6);
            processor.Distort();
            Land = processor.Image;
        }
    }

}
