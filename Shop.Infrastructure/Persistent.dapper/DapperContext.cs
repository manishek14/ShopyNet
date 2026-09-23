using Microsoft.Data.SqlClient;
using System.Data;

namespace Shop.Infrastructure.Persistent.Dapper
{
    public class DapperContext
    {
        private readonly string _connectionString;

        public DapperContext(string connectionString)
        {
            _connectionString = connectionString
                ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

        public static string Users => "[User].[Users]";
        public static string UserRoles => "[User].[UserRoles]";
        public static string Wallets => "[User].[Wallets]";
        public static string WalletTransactions => "[User].[WalletTransactions]";
        public static string UserAddresses => "[User].[UserAddresses]";

        public static string Products => "[Product].[Products]";
        public static string ProductImages => "[Product].[ProductImages]";
        public static string ProductSpecifications => "[Product].[ProductSpecifications]";

        public static string Categories => "[Category].[Categories]";

        public static string Orders => "[Order].[Orders]";
        public static string OrderItems => "[Order].[OrderItems]";

        public static string Comments => "[Comment].[Comments]";

        public static string Roles => "[Role].[Roles]";
        public static string RolePermissions => "[Role].[RolePermissions]";

        public static string Sellers => "[Seller].[Sellers]";
        public static string SellerInventories => "[Seller].[SellerInventories]";
    }
}