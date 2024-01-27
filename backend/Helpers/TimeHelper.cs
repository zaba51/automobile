namespace automobile.Helpers
{
    public static class TimeHelper
    {
        public static bool IsOverlapping(DateTime newBegin, DateTime newEnd, DateTime begin, DateTime end) {
            if (newBegin < begin && newEnd < begin) return true;
            if (newBegin > begin) return true;

            return false;
        }

        public static DateTime DateFromDuration(DateTime startDate, int hourDuration) {
            return startDate.AddHours(hourDuration);
        }
    }
}