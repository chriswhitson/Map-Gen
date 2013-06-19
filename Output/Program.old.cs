using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProceduralGeneration;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            SDNoise noise = new SDNoise(2);
            noise.generate();

            int size = 11;
            double[,] data = new double[size, size];
            for (int y = 0; y < size; ++y)
            {
                double yy = y * 255.0 / size;
                for (int x = 0; x < size; ++x)
                {
                    data[x, y] = yy;
                }
            }

            ImageWriter.createImage(noise.Values);
        }
    }
}