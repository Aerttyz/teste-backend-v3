namespace TheatricalPlayersRefactoringKata{

    public interface IPerformanceCalculator
    {
        public double CalculateAmount(Performance performance, Play play);
        public int CalculateVolumeCredits(Performance performance, Play play);
    }
}
