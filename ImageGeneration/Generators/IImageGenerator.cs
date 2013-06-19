﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageGenerator.Core;

namespace ImageGenerator.Generators
{
    public interface IImageGenerator
    {
        GeneratedImage Generate(GeneratedImage.ImageSize size);
    }
}
