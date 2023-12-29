namespace automobile.Helpers
{
    public static class TimeHelper
    {
        public static bool IsOverlapping(DateTime newBegin, DateTime newEnd, DateTime begin, DateTime end) {
            // if (newBegin < end && newEnd > begin) return false;

            // if (newBegin < begin && newEnd > newBegin) return false;

            // if (newBegin < end && newEnd > end) return false;

            // if (newBegin < begin && newEnd > end) return false;

            // return true;

            if (newBegin < begin && newEnd < begin) return true;
            if (newBegin > begin) return true;

            return false;
        }

        public static DateTime DateFromDuration(DateTime startDate, int hourDuration) {
            // var timespan = new TimeSpan(hourDuration, 0, 0);

            return startDate.AddHours(hourDuration);
        }
    }
}