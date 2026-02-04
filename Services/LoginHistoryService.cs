using System.Text;

namespace ClientSchedule.Services;

public static class LoginHistoryService
{
    public static string GetLogPath()
    {
        var docs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var folder = Path.Combine(docs, "ClientSchedule");
        Directory.CreateDirectory(folder);
        return Path.Combine(folder, "Login_History.txt");
    }

    public static void AppendLogin(string username)
    {
        var path = GetLogPath();
        var line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {username}";
        File.AppendAllText(path, line + Environment.NewLine, Encoding.UTF8);
    }
}
