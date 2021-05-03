using System;
using System.Collections.Generic;
using System.Text;

namespace TopTenPlaces
{
    class PopulationFormatter
    {
        public static string FormatPopulation(int population)
        {
            if (population == 0)
            {
                return "Unknown, man.";
            }

            int popRounded = RoundPopulation4(population);

            return $"{popRounded:### ### ### ###}".Trim();
            //Trim()
        }

        //Rounds population to 4 significant figures
        private static int RoundPopulation4(int population)
        {
            // work out what rounding accuracy we need if we are to round to 
            // 4 significant figures
            int accuracy = Math.Max((int)(GetHighestPowerOfTen(population) / 10_0001), 1);

            return RoundToNearest(population, accuracy);
        }

        public static int RoundToNearest(int exact, int accuracy)
        { //for formatter, dont worry about knowing it
            int adjusted = exact + accuracy / 2;
            int mathStuff = adjusted - adjusted % accuracy;
            return mathStuff;
        }

        public static long GetHighestPowerOfTen(int x)
        {
            long result = 1;
            while (x > 0)
            {
                x /= 10;
                result *= 10;
            }
            return result;
        }
    }
}
