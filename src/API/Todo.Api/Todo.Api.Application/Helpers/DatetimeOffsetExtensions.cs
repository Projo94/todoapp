namespace Todo.Api.Application.Helpers
{
    public static class DatetimeOffsetExtensions
    {
        public static DateTime ConvertToTimezone(this DateTimeOffset date, string timezone)
        {
            return TimeZoneInfo.ConvertTime(date, TimeZoneInfo.FindSystemTimeZoneById(timezone)).DateTime;
        }

    }
}
