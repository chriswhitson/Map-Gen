using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGenerator.Processors
{
    public class ImageProcessor
    {
        public Core.GeneratedImage Image { get; private set; }

        private ImageProcessor(Core.GeneratedImage image)
        {
            Image = image;
        }

        public static ImageProcessor NewInstance(Generators.IImageGenerator image, Core.GeneratedImage.ImageSize size)
        {
            return new ImageProcessor(image.Generate(size));
        }

        public void Blend(Core.GeneratedImage imageB, double influence)
        {
            Blender blender = new Blender();
            Image = blender.Process(Image, imageB, influence);
        }

        public void Distort()
        {
            Distorter distorter = new Distorter();
            Image = distorter.Process(Image);
        }

        public void QuickDistort()
        {
            ThreadedDistorter td = new ThreadedDistorter();
            Image = td.Process(Image);
        }

        public void ToBinary(byte threshold)
        {
            Binary b = new Binary();
            Image = b.Process(Image, threshold);
        }
    }
}
