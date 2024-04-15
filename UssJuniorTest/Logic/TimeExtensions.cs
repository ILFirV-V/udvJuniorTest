using UssJuniorTest.Core.Models.Response;

namespace UssJuniorTest.Logic
{
    public static class TimeExtensions
    {
        public static TimeDto ConvertTimeSpanInTimeDto(this TimeSpan time)
        {
            return new TimeDto(time.Days, time.Hours, time.Minutes);
        }
    }
}
