namespace Todo.Api.Application.Helpers
{
    public static class DatetimeExtensions
    {
        public static DateTime DatetimeWithoutSecondsAndMiliseconds(this DateTime date)
        {
            return date
                    .AddSeconds(-date.Second)
                    .AddMilliseconds(-date.Millisecond);
        }
    }
}
