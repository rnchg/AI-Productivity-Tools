namespace General.Apt.App.Utility
{
    public static class Progress
    {
        public static int GetProgress(int maximum, int totalFile, int currentFile, int totalStep, int currentStep, double percent)
        {
            var segmentFile = (double)maximum / totalFile;
            var segmentStep = (double)segmentFile / totalStep;
            var progress = (segmentFile * currentFile) + (segmentStep * currentStep) + (segmentStep * percent);
            return (int)progress;
        }
    }
}