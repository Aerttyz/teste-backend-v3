using System;


namespace TheatricalPlayersRefactoringKata.Calculators
{
    public interface ComedyCalculator
    {
        public int CalculateAmount(Performance performance, Play play){
            int lines = Math.Clamp(play.NumberOfLines, 1000, 4000);
            int baseAmount = lines / 10;
            if (performance.Audience > 20)
            {
                result += 10000 + 500 * (performance.Audience - 20);
            }
            result += 300 * performance.Audience;
            return baseAmount;
        }
        public int CalculateVolumeCredits(Performance performance, Play play){
            int credits =  Math.Max(performance.Audience - 30, 0);

            credits += performance.Audience / 5;
            return credits;
        }
    }
}