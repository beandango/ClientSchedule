using System.Globalization;

namespace ClientSchedule.Services;

public static class TimeRules
{
    public static readonly TimeZoneInfo Eastern =
        TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

    public static TimeZoneInfo UserTz => TimeZoneInfo.Local;

    // ----------------------------
    // Core conversions
    // ----------------------------

    public static DateTime EasternToUtc(DateTime easternWallClock)
    {
        var easternUnspec = DateTime.SpecifyKind(easternWallClock, DateTimeKind.Unspecified);
        return TimeZoneInfo.ConvertTimeToUtc(easternUnspec, Eastern);
    }

    public static DateTime UtcToEastern(DateTime utc)
    {
        var utcKind = DateTime.SpecifyKind(utc, DateTimeKind.Utc);
        var eastern = TimeZoneInfo.ConvertTimeFromUtc(utcKind, Eastern);
        return DateTime.SpecifyKind(eastern, DateTimeKind.Unspecified);
    }

    public static DateTime UtcToUserLocal(DateTime utc)
    {
        var utcKind = DateTime.SpecifyKind(utc, DateTimeKind.Utc);
        return TimeZoneInfo.ConvertTimeFromUtc(utcKind, UserTz);
    }

    // ----------------------------
    // Display helpers
    // ----------------------------

    public static string FormatUserLocalForGrid(DateTime utc)
        => UtcToUserLocal(utc).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

    public static string FormatEasternForGrid(DateTime utc)
        => UtcToEastern(utc).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

    // ----------------------------
    // DB read helper
    // ----------------------------

    public static DateTime ReadDbUtc(object dbValue)
        => DateTime.SpecifyKind(Convert.ToDateTime(dbValue), DateTimeKind.Utc);

    // ----------------------------
    // Business rules (Eastern)
    // ----------------------------

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

        var open = new TimeSpan(6, 0, 0);
        var close = new TimeSpan(17, 0, 0);

        if (startEastern.TimeOfDay < open || endEastern.TimeOfDay > close)
        {
            errorMessage = Resources.Strings.Appt_Validation_BusinessHours;
            return false;
        }

        errorMessage = "";
        return true;
    }
}
