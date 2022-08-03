using Npgsql;

namespace Discount.Grpc.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrationDatabase<TContext>(this IHost host, int? retry = 0)
        {
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var logger = service.GetRequiredService<ILogger<TContext>>();
                var configuration = service.GetRequiredService<IConfiguration>();

                using (var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSetting:ConnectionString")))
                {
                    try
                    {
                        logger.LogInformation("Migration postrewql database started");

                        string script = "DROP TABLE IF EXISTS coupon";

                        connection.Open();
                        var commnd = new NpgsqlCommand(script, connection);
                        commnd.ExecuteNonQuery();

                         script = @"CREATE TABLE IF NOT EXISTS coupon
                        (
                            id serial  primary key,
                            productname character varying(24) ,
                            description text,
                            amount integer
                        )";

                        commnd = new NpgsqlCommand(script, connection);
                        commnd.ExecuteNonQuery();

                         script = @"INSERT INTO coupon(productname, description, amount)
	                    VALUES ('APPle Iphone', 'Smart phone', 2000);
                        INSERT INTO coupon(productname, description, amount)
	                    VALUES ('Samsung Galaxy', 'Smart phone', 1000)";
                       

                        commnd = new NpgsqlCommand(script, connection);
                        commnd.ExecuteNonQuery();

                        logger.LogInformation("Migration postrewql database completed");
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "error occured while migrating db");
                        throw;
                    }
                    finally { connection.Close(); }
                }

            }
            return host;

        }
    }
}
