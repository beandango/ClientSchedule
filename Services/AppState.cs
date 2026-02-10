using System.Globalization;
using System.Threading;

namespace ClientSchedule.Services;

public static class AppState
{
    public static event Action? CultureChanged;

    public static CultureInfo CurrentCulture { get; private set; } = CultureInfo.CurrentUICulture;

    public static void SetCulture(string cultureName)
    {
        var c = new CultureInfo(cultureName);

        CultureInfo.DefaultThreadCurrentCulture = c;
        CultureInfo.DefaultThreadCurrentUICulture = c;

        Thread.CurrentThread.CurrentCulture = c;
        Thread.CurrentThread.CurrentUICulture = c;

        Application.CurrentCulture = c;

        CurrentCulture = c;
        CultureChanged?.Invoke();
    }
}
