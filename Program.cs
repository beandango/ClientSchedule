using ClientSchedule.Services;
using ClientSchedule.Forms;

namespace ClientSchedule;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        AppState.SetCulture("en-US");

        using var login = new LoginForm();
        if (login.ShowDialog() != DialogResult.OK)
            return;

        Application.Run(new MainForm(login.LoggedInUserId, login.LoggedInUsername));
    }
}
