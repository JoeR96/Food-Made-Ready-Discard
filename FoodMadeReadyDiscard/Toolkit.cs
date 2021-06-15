using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodMadeReadyDiscard
{
    static class Toolkit
    {
        public static bool BetweenRanges(int a, int b, int number)
        {
            return (a <= number && number <= b);
        }
    }
}
