using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace techtasticwelcome.Helpers
{
    public static class Guard
    {
        public static void GuardStringValue(string value, string name)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("name");
        }
    }
}
