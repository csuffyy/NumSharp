﻿using NumSharp.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NumSharp.Core
{
    public partial class NumPy
    {
        public matrix asmatrix(NDArray nd)
        {
            return nd.AsMatrix();
        }
    }
}
