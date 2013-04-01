using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsLibrary
{
    class Assert
    {
        public static void InRange(int index, int start, int end)
        {
            if (index < start || index > end)
                throw new Exception("Invalid Range");
        }
    }
}
