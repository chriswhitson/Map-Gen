using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageGenerator.Core;
using ImageGenerator.Statistics;

namespace ImageGenerator.Statistics
{
    public class ImageStatistics
    {
        public double Average { get; protected set; }
        public double Min { get; protected set; }
        public double Max { get; protected set; }
        public Histogram ColumnHistogram { get; protected set; }
        public Histogram RowHistogram { get; protected set; }

        private ImageStatistics() { }

        public static ImageStatistics GenerateStatistics(Image image)
        {
            ImageStatistics stats = new ImageStatistics();
            stats.CalculateStats(image);
            return stats;
        }
        
        private void CalculateStats(Image image)
        {
            Average = 0;
            Min = double.MaxValue;
            Max = double.MinValue;
            ColumnHistogram = new Histogram(image.PixelData.Width);
            RowHistogram = new Histogram(image.PixelData.Height);

            for (int x = 0; x < image.PixelData.Width; ++x)
            {
                for (int y = 0; y < image.PixelData.Height; ++y)
                {
                    double v = image.PixelData[x, y];
                    Average += v;
                    if (v < Min) { Min = v; }
                    else if (v > Max) { Max = v; }
                    ColumnHistogram[y] += v;
                    RowHistogram[x] += y;
                }
            }

            Average /= (image.PixelData.Height + image.PixelData.Width);
        }
    }


}
