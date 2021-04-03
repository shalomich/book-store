﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QueryWorker
{
    public interface IInformed
    {
        event Action<string, string> Accepted;
        event Action<string, string> Crashed;
    }
}
