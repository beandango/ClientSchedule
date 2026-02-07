using MySql.Data.MySqlClient;
using System.Data;

namespace ClientSchedule.Data;

public static class AppointmentRepository
{
    private const string DefaultContact = "";
    private const string DefaultUrl = "";

    // ----------------------------
    // Queries
    // ----------------------------

    public static Task<DataTable> GetAppointmentsForUserAsync(int userId)
        => Db.QueryAsync(
            "SELECT a.appointmentId, c.customerName, a.title, a.type, a.`start`, a.`end` " +
            "FROM appointment a " +
            "JOIN customer c ON a.customerId = c.customerId " +
            "WHERE a.userId = @uid " +
            "ORDER BY a.`start`;",
            new MySqlParameter("@uid", userId)
        );

    public static async Task<DataRow?> GetAppointmentByIdAsync(int appointmentId)
    {
        var dt = await Db.QueryAsync(
            "SELECT appointmentId, customerId, title, type, description, location, contact, url, `start`, `end` " +
            "FROM appointment " +
            "WHERE appointmentId=@id;",
            new MySqlParameter("@id", appointmentId)
        );

        return dt.Rows.Count == 0 ? null : dt.Rows[0];
    }

    public static Task<DataTable> GetAppointmentsForUserBetweenUtcAsync(int userId, DateTime startUtc, DateTime endUtc)
        => Db.QueryAsync(
            "SELECT a.appointmentId, c.customerName, a.title, a.type, a.`start`, a.`end` " +
            "FROM appointment a " +
            "JOIN customer c ON a.customerId = c.customerId " +
            "WHERE a.userId = @uid AND a.`start` >= @startUtc AND a.`start` < @endUtc " +
            "ORDER BY a.`start`;",
            new MySqlParameter("@uid", userId),
            new MySqlParameter("@startUtc", MySqlDbType.DateTime) { Value = startUtc },
            new MySqlParameter("@endUtc", MySqlDbType.DateTime) { Value = endUtc }
        );

    // ----------------------------
    // Mutations
    // ----------------------------

    public static Task InsertAsync(
        int customerId,
        int userId,
        string title,
        string type,
        string? description,
        string? location,
        DateTime startUtc,
        DateTime endUtc,
        string username)
    {
        return Db.ExecAsync(
            "INSERT INTO appointment " +
            "(customerId, userId, title, description, location, contact, type, url, `start`, `end`, createDate, createdBy, lastUpdateBy) " +
            "VALUES (@cid, @uid, @t, @d, @loc, @contact, @type, @url, @s, @e, NOW(), @cb, @lub);",
            new MySqlParameter("@cid", customerId),
            new MySqlParameter("@uid", userId),
            new MySqlParameter("@t", title),
            new MySqlParameter("@d", DbNullIfEmpty(description)),
            new MySqlParameter("@loc", DbNullIfEmpty(location)),
            new MySqlParameter("@contact", DefaultContact),
            new MySqlParameter("@type", type),
            new MySqlParameter("@url", DefaultUrl),
            new MySqlParameter("@s", MySqlDbType.DateTime) { Value = startUtc },
            new MySqlParameter("@e", MySqlDbType.DateTime) { Value = endUtc },
            new MySqlParameter("@cb", username),
            new MySqlParameter("@lub", username)
        );
    }

    public static Task UpdateAsync(
        int appointmentId,
        int customerId,
        string title,
        string type,
        string? description,
        string? location,
        DateTime startUtc,
        DateTime endUtc,
        string username)
    {
        return Db.ExecAsync(
            "UPDATE appointment " +
            "SET customerId=@cid, title=@t, description=@d, location=@loc, contact=@contact, type=@type, `start`=@s, `end`=@e, lastUpdateBy=@lub " +
            "WHERE appointmentId=@id;",
            new MySqlParameter("@cid", customerId),
            new MySqlParameter("@t", title),
            new MySqlParameter("@d", DbNullIfEmpty(description)),
            new MySqlParameter("@loc", DbNullIfEmpty(location)),
            new MySqlParameter("@contact", DefaultContact),
            new MySqlParameter("@type", type),
            new MySqlParameter("@s", MySqlDbType.DateTime) { Value = startUtc },
            new MySqlParameter("@e", MySqlDbType.DateTime) { Value = endUtc },
            new MySqlParameter("@lub", username),
            new MySqlParameter("@id", appointmentId)
        );
    }

    public static Task DeleteAsync(int appointmentId)
        => Db.ExecAsync(
            "DELETE FROM appointment WHERE appointmentId=@id;",
            new MySqlParameter("@id", appointmentId)
        );

    // ----------------------------
    // Rules support
    // ----------------------------

    public static async Task<bool> HasOverlapAsync(
        int userId,
        DateTime newStartUtc,
        DateTime newEndUtc,
        int? excludeAppointmentId)
    {
        var sql =
            "SELECT COUNT(*) FROM appointment " +
            "WHERE userId=@uid " +
            "AND @newStart < `end` AND @newEnd > `start` " +
            (excludeAppointmentId is null ? "" : "AND appointmentId <> @ex ");

        var ps = new List<MySqlParameter>
        {
            new("@uid", userId),
            new("@newStart", MySqlDbType.DateTime) { Value = newStartUtc },
            new("@newEnd", MySqlDbType.DateTime) { Value = newEndUtc },
        };

        if (excludeAppointmentId is not null)
            ps.Add(new MySqlParameter("@ex", excludeAppointmentId.Value));

        var countObj = await Db.ScalarAsync(sql, ps.ToArray());
        return Convert.ToInt32(countObj ?? 0) > 0;
    }

    private static object DbNullIfEmpty(string? s)
        => string.IsNullOrWhiteSpace(s) ? DBNull.Value : s!;
}
