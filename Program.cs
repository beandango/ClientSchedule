using System.Globalization;
using ClientSchedule.Forms;
using ClientSchedule.Services;

namespace ClientSchedule;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var osUi = CultureInfo.CurrentUICulture?.Name ?? "en-US";
        AppState.SetCulture(osUi);

        using var login = new LoginForm();
        if (login.ShowDialog() != DialogResult.OK)
            return;

        Application.Run(new MainForm(login.LoggedInUserId, login.LoggedInUsername));
    }
}
