using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageGenerator;
using ImageLibrary;
using ImageGenerator.Core;
using ImageGenerator.Generators;
using ImageGenerator.Processors;
using System.Diagnostics;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            GeneratedImage.ImageSize size = GeneratedImage.ImageSize.MEDIUM;
           //ImageLibrary.ImageWriter.createImage(new ImageGenerator.Core.IslandMap(size).Land);

            ImageProcessor proc =  ImageProcessor.NewInstance(new Noise(), GeneratedImage.ImageSize.MEDIUM);
            proc.Blend(new Circle().Generate(size), 0.6);
            proc.Distort();
            proc.ToBinary(50); 
            ImageLibrary.ImageWriter.createImage(proc.Image);

             
           /* Stopwatch stopWatch = new Stopwatch();
            for (int i = 0; i < 10; ++i)
            {
                stopWatch.Start();
                proc.Distort();
                stopWatch.Stop();
                Console.WriteLine(i + " : " + stopWatch.ElapsedMilliseconds);
                stopWatch.Reset();
            }

            for (int i = 0; i < 10; ++i)
            {
                stopWatch.Start();
                proc.QuickDistort();
                stopWatch.Stop();
                Console.WriteLine(i + " : " + stopWatch.ElapsedMilliseconds);
                stopWatch.Reset();
            }
            */
        }

    }
}
