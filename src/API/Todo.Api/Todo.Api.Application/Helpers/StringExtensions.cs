namespace Todo.Api.Application.Helpers
{
    public static class StringExtensions
    {
        public static bool IsValidTimezone(this string timezone)
        {

            if (!string.IsNullOrEmpty(timezone))
            {
                var timeZoneIds = new HashSet<string>(TimeZoneInfo
              .GetSystemTimeZones()
              .Select(tzi => tzi.Id));

                var validTimeZone = timeZoneIds.Contains(timezone);

                return validTimeZone;
            }
            else
            {
                return false;
            }
        }
    }
}
