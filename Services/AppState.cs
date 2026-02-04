using System.Globalization;

namespace ClientSchedule.Services;

public static class AppState
{
    public static event Action? CultureChanged;

    public static CultureInfo CurrentCulture { get; private set; } = CultureInfo.CurrentUICulture;

    public static void SetCulture(string cultureName)
    {
        var c = new CultureInfo(cultureName);

        CultureInfo.CurrentCulture = c;
        CultureInfo.CurrentUICulture = c;

        Application.CurrentCulture = c;

        CurrentCulture = c;
        CultureChanged?.Invoke();
    }
}
