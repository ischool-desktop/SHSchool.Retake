﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SHSchool.Retake
{
    public static class RetakeEvents
    {
        public static void RaiseAssnChanged()
        {
            if (RetakeChanged != null)
                RetakeChanged(null, EventArgs.Empty);
        }

        public static event EventHandler RetakeChanged;
    }
}
