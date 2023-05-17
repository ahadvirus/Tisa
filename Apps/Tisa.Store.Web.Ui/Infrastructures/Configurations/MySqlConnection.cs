using MySql.Data.MySqlClient;

namespace Tisa.Store.Web.Ui.Infrastructures.Configurations;

public record MySqlConnection
{
    public string Server { get; init; }
    public uint Port { get; init; }
    public string Database { get; init; }
    public string UserId { get; init; }
    public string Password { get; init; }

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