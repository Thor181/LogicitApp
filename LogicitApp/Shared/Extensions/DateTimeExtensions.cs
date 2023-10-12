namespace LogicitApp.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToDateTimeString(this DateTime d)
        {
            return $"{d:G}";
        }
    }
}
