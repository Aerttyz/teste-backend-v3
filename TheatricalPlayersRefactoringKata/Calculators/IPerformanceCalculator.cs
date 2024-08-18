namespace TheatricalPlayersRefactoringKata;

    public interface IPerformanceCalculator
    {
        public int CalculateAmount(Performance performance, Play play);
        public int CalculateVolumeCredits(Performance performance, Play play);
    }