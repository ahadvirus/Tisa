using MySql.Data.MySqlClient;

namespace Tisa.Store.Web.Ui.Infrastructures.Configurations;

/// <summary>
/// Represent connection string to database
/// </summary>
public record MySqlConnection
{
    public MySqlConnection()
    {
        Server = string.Empty;
        Database = string.Empty;
        UserId = string.Empty;
        Password = string.Empty;
    }

    /// <summary>
    /// Have the address of database
    /// </summary>
    public string Server { get; init; }

    /// <summary>
    /// The port number can connect to database
    /// </summary>
    public uint Port { get; init; }

    /// <summary>
    /// Database name in the SQL Server
    /// </summary>
    public string Database { get; init; }

    /// <summary>
    /// The username is allowed to access the database
    /// </summary>
    public string UserId { get; init; }

    /// <summary>
    /// Password of username to complete accessing the database
    /// </summary>
    public string Password { get; init; }

    /// <summary>
    /// Return the connection string
    /// </summary>
    /// <returns><see cref="string"/> The full address of SQL Server base on information</returns>
    public string String()
    {
        return new MySqlConnectionStringBuilder()
        {
            Server = Server,
            Port = Port,
            Database = Database,
            UserID = UserId,
            Password = Password
        }.ConnectionString;
    }
}