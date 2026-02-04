using ClientSchedule.Data;
using MySql.Data.MySqlClient;

namespace ClientSchedule.Services;

public static class UpcomingAlertService
{
    public static async Task ShowUpcomingAppointmentAlertAsync(int userId)
    {
        try
        {
            var nowUtc = DateTime.UtcNow;
            var soonUtc = nowUtc.AddMinutes(15);

            var dt = await Db.QueryAsync(
                "SELECT c.customerName AS customerName, a.title AS apptTitle, a.type AS apptType, a.`start` AS apptStart " +
                "FROM appointment a " +
                "JOIN customer c ON a.customerId = c.customerId " +
                "WHERE a.userId=@uid AND a.`start` >= @now AND a.`start` <= @soon " +
                "ORDER BY a.`start` " +
                "LIMIT 1;",
                new MySql.Data.MySqlClient.MySqlParameter("@uid", userId),
                new MySql.Data.MySqlClient.MySqlParameter("@now", nowUtc),
                new MySql.Data.MySqlClient.MySqlParameter("@soon", soonUtc)
            );


            if (dt.Rows.Count == 0)
                return;

            var r = dt.Rows[0];

            var customer = Convert.ToString(r["customerName"]) ?? "";
            var title = Convert.ToString(r["apptTitle"]) ?? "";     // <-- use alias
            var type = Convert.ToString(r["apptType"]) ?? "";       // <-- use alias

            var startUtc = TimeRules.ReadDbUtc(r["apptStart"]);
            var startLocal = startUtc.ToLocalTime().ToString("yyyy-MM-dd HH:mm");

            if (string.IsNullOrWhiteSpace(title))
                title = "(no title)";

            MessageBox.Show(
                string.Format(Resources.Strings.Alert_UpcomingApptBody, customer, title, type, startLocal),
                Resources.Strings.Alert_UpcomingApptTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        catch
        {
            // ignore alert failures so app still loads
        }
    }
}
