using System.Data;
using ClientSchedule.Data;
using MySql.Data.MySqlClient;

namespace ClientSchedule.Services;

public static class ReportService
{
    private sealed record ApptRow(int AppointmentId, int UserId, string UserName, int CustomerId, string CustomerName, string Title,
                                  string Type, DateTime StartUtc, DateTime EndUtc);

    public sealed record TypesByMonthRow(string Month, string Type, int Count);
    public sealed record UserScheduleRow(string User, string Customer, string Title, string Type, string StartLocal, string EndLocal);
    public sealed record ByCustomerRow(string Customer, int Count);

    public static async Task<List<TypesByMonthRow>> GetTypesByMonthAsync()
    {
        var appts = await LoadAppointmentsWithUserAndCustomerAsync();

        var result = appts
            .GroupBy(a => new
            {
                Month = a.StartUtc.ToLocalTime().ToString("yyyy-MM"),
                Type = a.Type
            })
            .Select(g => new TypesByMonthRow(g.Key.Month, g.Key.Type, g.Count()))
            .OrderBy(r => r.Month)
            .ThenBy(r => r.Type)
            .ToList();

        return result;
    }

    public static async Task<List<UserScheduleRow>> GetScheduleForEachUserAsync()
    {
        var appts = await LoadAppointmentsWithUserAndCustomerAsync();

        var result = appts
            .OrderBy(a => a.UserName)
            .ThenBy(a => a.StartUtc)
            .Select(a => new UserScheduleRow(
                a.UserName,
                a.CustomerName,
                a.Title,
                a.Type,
                a.StartUtc.ToLocalTime().ToString("yyyy-MM-dd HH:mm"),
                a.EndUtc.ToLocalTime().ToString("yyyy-MM-dd HH:mm")
            ))
            .ToList();

        return result;
    }

    public static async Task<List<ByCustomerRow>> GetAppointmentsPerCustomerAsync()
    {
        var appts = await LoadAppointmentsWithUserAndCustomerAsync();

        var result = appts
            .GroupBy(a => a.CustomerName)
            .Select(g => new ByCustomerRow(g.Key, g.Count()))
            .OrderByDescending(r => r.Count)
            .ThenBy(r => r.Customer)
            .ToList();

        return result;
    }

    private static async Task<List<ApptRow>> LoadAppointmentsWithUserAndCustomerAsync()
    {
        var dt = await Db.QueryAsync(
            "SELECT a.appointmentId, a.userId, u.userName, a.customerId, c.customerName, a.title, a.type, a.`start`, a.`end` " +
            "FROM appointment a " +
            "JOIN user u ON a.userId = u.userId " +
            "JOIN customer c ON a.customerId = c.customerId;"
        );

        var list = new List<ApptRow>(dt.Rows.Count);

        foreach (DataRow r in dt.Rows)
        {
            var startUtc = TimeRules.ReadDbUtc(r["start"]);
            var endUtc = TimeRules.ReadDbUtc(r["end"]);

            list.Add(new ApptRow(
                AppointmentId: Convert.ToInt32(r["appointmentId"]),
                UserId: Convert.ToInt32(r["userId"]),
                UserName: Convert.ToString(r["userName"]) ?? "",
                CustomerId: Convert.ToInt32(r["customerId"]),
                CustomerName: Convert.ToString(r["customerName"]) ?? "",
                Title: Convert.ToString(r["title"]) ?? "",
                Type: Convert.ToString(r["type"]) ?? "",
                StartUtc: startUtc,
                EndUtc: endUtc
            ));
        }

        return list;
    }
}
