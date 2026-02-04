using System.Globalization;

namespace ClientSchedule.Services;

public static class TimeRules
{
    // Windows timezone ID for US Eastern (works on Windows 10/11)
    public static readonly TimeZoneInfo Eastern =
        TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

    public static DateTime EasternToUtc(DateTime easternWallClock)
    {
        var easternUnspec = DateTime.SpecifyKind(easternWallClock, DateTimeKind.Unspecified);
        return TimeZoneInfo.ConvertTimeToUtc(easternUnspec, Eastern);
    }

    public static DateTime UtcToEastern(DateTime utc)
    {
        var utcKind = DateTime.SpecifyKind(utc, DateTimeKind.Utc);
        return TimeZoneInfo.ConvertTimeFromUtc(utcKind, Eastern);
    }

    public static bool ValidateEasternBusinessHours(DateTime startEastern, DateTime endEastern, out string errorMessage)
    {
        if (endEastern <= startEastern)
        {
            errorMessage = Resources.Strings.Appt_Validation_EndAfterStart;
            return false;
        }

        if (startEastern.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
        {
            errorMessage = Resources.Strings.Appt_Validation_BusinessHours;
            return false;
        }

        if (startEastern.Date != endEastern.Date)
        {
            errorMessage = Resources.Strings.Appt_Validation_SameDay;
            return false;
        }

        var open = new TimeSpan(9, 0, 0);
        var close = new TimeSpan(17, 0, 0);

        if (startEastern.TimeOfDay < open || endEastern.TimeOfDay > close)
        {
            errorMessage = Resources.Strings.Appt_Validation_BusinessHours;
            return false;
        }

        errorMessage = "";
        return true;
    }

    public static string FormatEasternForGrid(DateTime utc)
        => UtcToEastern(utc).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

    public static DateTime ReadDbUtc(object dbValue)
        => DateTime.SpecifyKind(Convert.ToDateTime(dbValue), DateTimeKind.Utc);
}
