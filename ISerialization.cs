﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder
{
    public interface ISerialization
    {
        string Serialize();

        void Deserialize(string obj);
    }
}