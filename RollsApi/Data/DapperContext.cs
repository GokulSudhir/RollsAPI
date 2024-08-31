namespace RollsApi.Data
{
  public class DapperContext
  {
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
      _configuration = configuration;
      _connectionString = _configuration.GetValue<string>("DapperConn:PgSql");
    }

    public IDbConnection CreateConnection()
        => new NpgsqlConnection(_connectionString);
  }
}
