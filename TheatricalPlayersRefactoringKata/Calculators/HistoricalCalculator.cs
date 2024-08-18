using System;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    public class HistoricalCalculator : IPerformanceCalculator
    {
        private readonly TragedyCalculator _tragedyCalculator;
        private readonly ComedyCalculator _comedyCalculator;

        public HistoricalCalculator(TragedyCalculator tragedyCalculator, ComedyCalculator comedyCalculator)
        {
            _tragedyCalculator = tragedyCalculator;
            _comedyCalculator = comedyCalculator;
        }

        public double CalculateAmount(Performance performance, Play play)
        {   
            return _tragedyCalculator.CalculateAmount(performance, play) + _comedyCalculator.CalculateAmount(performance, play);
        }

        public int CalculateVolumeCredits(Performance performance, Play play)
        {
            return _tragedyCalculator.CalculateVolumeCredits(performance, play);
        }
    }
}