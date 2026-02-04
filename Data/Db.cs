using System.Data;
using MySql.Data.MySqlClient;

namespace ClientSchedule.Data;

public static class Db
{
    public static MySqlConnection GetOpenConnection()
    {
        var conn = new MySqlConnection(DbConfig.ConnectionString);
        conn.Open();
        return conn;
    }

    public static async Task<DataTable> QueryAsync(string sql, params MySqlParameter[] ps)
    {
        using var conn = GetOpenConnection();
        using var cmd = new MySqlCommand(sql, conn);
        if (ps is { Length: > 0 }) cmd.Parameters.AddRange(ps);

        using var reader = await cmd.ExecuteReaderAsync();
        var dt = new DataTable();
        dt.Load(reader);
        return dt;
    }

    public static async Task<int> ExecuteAsync(string sql, params MySqlParameter[] ps)
    {
        using var conn = GetOpenConnection();
        using var cmd = new MySqlCommand(sql, conn);
        if (ps is { Length: > 0 }) cmd.Parameters.AddRange(ps);
        return await cmd.ExecuteNonQueryAsync();
    }

    public static async Task<object?> ScalarAsync(string sql, params MySql.Data.MySqlClient.MySqlParameter[] ps)
    {
        using var conn = GetOpenConnection();
        using var cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
        if (ps is { Length: > 0 }) cmd.Parameters.AddRange(ps);
        return await cmd.ExecuteScalarAsync();
    }


    public static async Task<int> ExecAsync(string sql, params MySqlParameter[] ps)
    {
        using var conn = GetOpenConnection();
        using var cmd = new MySqlCommand(sql, conn);
        if (ps is { Length: > 0 }) cmd.Parameters.AddRange(ps);
        return await cmd.ExecuteNonQueryAsync();
    }

}
