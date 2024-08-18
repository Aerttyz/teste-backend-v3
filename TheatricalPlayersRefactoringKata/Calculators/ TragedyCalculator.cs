
using System;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    public class TragedyCalculator : IPerformanceCalculator
    {
        public int CalculateAmount(Performance performance, Play play)
        {
            int result = 40000;
            if (performance.Audience > 30)
            {
                result += 1000 * (performance.Audience - 30);
            }
            return result;
        }

        public int CalculateVolumeCredits(Performance performance, Play play)
        {
            return Math.Max(performance.Audience - 30, 0);
        }
    }
}
