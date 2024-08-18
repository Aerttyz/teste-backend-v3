
using System;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    public class TragedyCalculator : IPerformanceCalculator
    {
        public int CalculateAmount(Performance performance, Play play)
        {
            int lines = Math.Clamp(play.NumberOfLines, 1000, 4000);
            int baseAmount = lines / 10;
            if (performance.Audience > 30)
            {
                result += 1000 * (performance.Audience - 30);
            }
            return baseAmount;
        }

        public int CalculateVolumeCredits(Performance performance, Play play)
        {
            return Math.Max(performance.Audience - 30, 0);
        }
    }
}
