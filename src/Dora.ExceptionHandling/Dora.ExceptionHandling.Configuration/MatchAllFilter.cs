﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dora.ExceptionHandling.Configuration
{
    public class MatchAllFilter : IExceptionFilter
    {
        public bool Match(ExceptionContext context)
        {
            return true;
        }
    }
}
