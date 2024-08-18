using System;


namespace TheatricalPlayersRefactoringKata.Calculators
{
    public class ComedyCalculator : IPerformanceCalculator
    {
        public double CalculateAmount(Performance performance, Play play)
        {
            double result = 0;
            int lines = Math.Clamp(play.Lines, 1000, 4000);
            double baseAmount = lines * 10;
            double additionalAmount = 0;
            
            if (performance.Audience > 20)
            {
                additionalAmount = 10000 + 500 * (performance.Audience - 20);
            }
            baseAmount += 300 * performance.Audience;
            result = baseAmount + additionalAmount;
            return result;
        }


        public int CalculateVolumeCredits(Performance performance, Play play)
        {
            int credits = Math.Max(performance.Audience - 30, 0);
            credits += performance.Audience / 5;
            return credits;
        }
    }
}