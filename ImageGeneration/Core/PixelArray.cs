using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageGenerator.Core
{
    public class PixelArray
    {
        private Color[,] data;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public PixelArray(int width, int height)
        {
            if (width <= 0 | height <= 0)
            {
                throw new System.ArgumentException("width and height must be greater than 0, width:" + width
                    + " height:" + height);
            }

            data = new Color[width, height];
            Width = width;
            Height = height;
        }

        public byte this[int x, int y]
        {
            get { return data[x, y].A; }
            internal set { data[x, y] = Color.FromArgb(value, data[x,y]); }
        }
    }
}
