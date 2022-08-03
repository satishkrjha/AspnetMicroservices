using Dapper;
using Discount.Grpc.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
      private readonly  IConfiguration configuration;
        NpgsqlConnection connection;
          
        public DiscountRepository(IConfiguration configuration)
        {
            connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSetting:ConnectionString"));
            this.configuration = configuration;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            var affected = await connection.ExecuteAsync("insert into coupon(productname,description,amount)" +
                " values(@productname,@description,@amount)",
                new { productname = coupon.ProductName, description=coupon.Description, amount=coupon.Amount });

            return Convert.ToBoolean(affected);
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            var affected = await connection.ExecuteAsync("delete from coupon where productname=@productname",
                 new { productname = productName });
            return Convert.ToBoolean(affected);
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("select * from coupon where productname=@ProductName",new {ProductName=productName});
            if (coupon == null)
            {
                return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No desc." };
            }
            return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            var affected = await connection.ExecuteAsync("update coupon set productname=@productname,description=@description,amount=@amount " +
                " where Id=@Id)",
                new { productname = coupon.ProductName, description = coupon.Description, amount = coupon.Amount,Id=coupon.Id });

            return Convert.ToBoolean(affected);
        }
    }
}
