
using System;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    public class TragedyCalculator : IPerformanceCalculator
    {
        public double CalculateAmount(Performance performance, Play play)
        {   
            double result = 0;
            int lines = Math.Clamp(play.Lines, 1000, 4000);
            double baseAmount = lines * 10;
            double additionalAmount = 0;

            if (performance.Audience > 30)
            {
                additionalAmount = 1000 * (performance.Audience - 30);
            }
            result = baseAmount + additionalAmount;

            return result;
        }

        public int CalculateVolumeCredits(Performance performance, Play play)
        {
            return Math.Max(performance.Audience - 30, 0);
        }
    }
}
