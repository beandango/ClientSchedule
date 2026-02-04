using MySql.Data.MySqlClient;
using System.Data;

namespace ClientSchedule.Data;

public static class CustomerRepository
{
    public static Task<DataTable> GetCustomersForGridAsync()
        => Db.QueryAsync(
            "SELECT c.customerId, c.customerName, a.address, a.address2, a.postalCode, a.phone, ci.city, co.country " +
            "FROM customer c " +
            "JOIN address a ON c.addressId = a.addressId " +
            "JOIN city ci ON a.cityId = ci.cityId " +
            "JOIN country co ON ci.countryId = co.countryId " +
            "ORDER BY c.customerId;"
        );

    public static Task<DataTable> GetCountriesAsync()
        => Db.QueryAsync("SELECT countryId, country FROM country ORDER BY country;");

    public static Task<DataTable> GetCitiesByCountryAsync(int countryId)
        => Db.QueryAsync(
            "SELECT cityId, city FROM city WHERE countryId=@cid ORDER BY city;",
            new MySqlParameter("@cid", countryId)
        );

    public static Task<DataTable> GetCustomerForEditAsync(int customerId)
        => Db.QueryAsync(
            "SELECT c.customerName, a.address, a.address2, a.postalCode, a.phone, ci.cityId, co.countryId " +
            "FROM customer c " +
            "JOIN address a ON c.addressId = a.addressId " +
            "JOIN city ci ON a.cityId = ci.cityId " +
            "JOIN country co ON ci.countryId = co.countryId " +
            "WHERE c.customerId=@id;",
            new MySqlParameter("@id", customerId)
        );

    public static async Task AddCustomerAsync(
        string username,
        string customerName,
        string address1,
        string? address2,
        int cityId,
        string? postalCode,
        string phone)
    {
        using var conn = Db.GetOpenConnection();
        using var tx = await conn.BeginTransactionAsync();

        try
        {
            // address
            var addressId = await ExecInsertAddressAsync(conn, tx, username, address1, address2, cityId, postalCode, phone);

            // customer
            using (var cmd = new MySqlCommand(
                "INSERT INTO customer (customerName, addressId, active, createDate, createdBy, lastUpdateBy) " +
                "VALUES (@n, @aid, 1, NOW(), @cb, @lub);", conn, (MySqlTransaction)tx))
            {
                cmd.Parameters.AddWithValue("@n", customerName);
                cmd.Parameters.AddWithValue("@aid", addressId);
                cmd.Parameters.AddWithValue("@cb", username);
                cmd.Parameters.AddWithValue("@lub", username);
                await cmd.ExecuteNonQueryAsync();
            }

            await tx.CommitAsync();
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }
    }

    public static async Task UpdateCustomerAsync(
        string username,
        int customerId,
        string customerName,
        string address1,
        string? address2,
        int cityId,
        string? postalCode,
        string phone)
    {
        using var conn = Db.GetOpenConnection();
        using var tx = await conn.BeginTransactionAsync();

        try
        {
            int addressId;

            using (var cmd = new MySqlCommand(
                "SELECT addressId FROM customer WHERE customerId=@id;",
                conn, (MySqlTransaction)tx))
            {
                cmd.Parameters.AddWithValue("@id", customerId);
                var obj = await cmd.ExecuteScalarAsync();
                if (obj is null) throw new InvalidOperationException("Customer not found.");
                addressId = Convert.ToInt32(obj);
            }

            // address update
            using (var cmd = new MySqlCommand(
                "UPDATE address SET address=@a1, address2=@a2, cityId=@city, postalCode=@pc, phone=@ph, lastUpdateBy=@lub " +
                "WHERE addressId=@aid;",
                conn, (MySqlTransaction)tx))
            {
                cmd.Parameters.AddWithValue("@a1", address1);
                cmd.Parameters.AddWithValue("@a2", string.IsNullOrWhiteSpace(address2) ? DBNull.Value : address2);
                cmd.Parameters.AddWithValue("@city", cityId);
                cmd.Parameters.AddWithValue("@pc", string.IsNullOrWhiteSpace(postalCode) ? DBNull.Value : postalCode);
                cmd.Parameters.AddWithValue("@ph", phone);
                cmd.Parameters.AddWithValue("@lub", username);
                cmd.Parameters.AddWithValue("@aid", addressId);
                await cmd.ExecuteNonQueryAsync();
            }

            // customer update
            using (var cmd = new MySqlCommand(
                "UPDATE customer SET customerName=@n, lastUpdateBy=@lub WHERE customerId=@id;",
                conn, (MySqlTransaction)tx))
            {
                cmd.Parameters.AddWithValue("@n", customerName);
                cmd.Parameters.AddWithValue("@lub", username);
                cmd.Parameters.AddWithValue("@id", customerId);
                await cmd.ExecuteNonQueryAsync();
            }

            await tx.CommitAsync();
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }
    }

    public static async Task DeleteCustomerAsync(int customerId)
    {
        // Keep behavior you already had: delete customer then delete related address
        var dt = await Db.QueryAsync(
            "SELECT addressId FROM customer WHERE customerId=@id;",
            new MySqlParameter("@id", customerId)
        );

        if (dt.Rows.Count == 0)
            return;

        var addressId = Convert.ToInt32(dt.Rows[0]["addressId"]);

        await Db.ExecAsync(
            "DELETE FROM customer WHERE customerId=@id;",
            new MySqlParameter("@id", customerId)
        );

        await Db.ExecAsync(
            "DELETE FROM address WHERE addressId=@aid;",
            new MySqlParameter("@aid", addressId)
        );
    }

    private static async Task<int> ExecInsertAddressAsync(
        MySqlConnection conn,
        MySqlTransaction tx,
        string username,
        string address1,
        string? address2,
        int cityId,
        string? postalCode,
        string phone)
    {
        using (var cmd = new MySqlCommand(
            "INSERT INTO address (address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdateBy) " +
            "VALUES (@a1, @a2, @city, @pc, @ph, NOW(), @cb, @lub);",
            conn, tx))
        {
            cmd.Parameters.AddWithValue("@a1", address1);
            cmd.Parameters.AddWithValue("@a2", string.IsNullOrWhiteSpace(address2) ? DBNull.Value : address2);
            cmd.Parameters.AddWithValue("@city", cityId);
            cmd.Parameters.AddWithValue("@pc", string.IsNullOrWhiteSpace(postalCode) ? DBNull.Value : postalCode);
            cmd.Parameters.AddWithValue("@ph", phone);
            cmd.Parameters.AddWithValue("@cb", username);
            cmd.Parameters.AddWithValue("@lub", username);
            await cmd.ExecuteNonQueryAsync();
        }

        using var idCmd = new MySqlCommand("SELECT LAST_INSERT_ID();", conn, tx);
        var idObj = await idCmd.ExecuteScalarAsync();
        return Convert.ToInt32(idObj);
    }
}
