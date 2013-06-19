using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGenerator.Statistics
{
    public class Histogram
    {
        private double[] values;
        public int Length { get; private set; }

        internal Histogram(int length)
        {
            values = new double[length];
            Length = length;
        }

        public double this[int i]
        {
            get { return values[i]; }
            internal set { values[i] = value; }
        }
    }
    
}
