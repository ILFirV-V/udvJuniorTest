namespace UssJuniorTest.Core.Models.Response
{
    public class TimeDto
    {
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }

        public TimeDto(int days, int hours, int minutes)
        {
            Days = days;
            Hours = hours;
            Minutes = minutes;
        }
    }
}
