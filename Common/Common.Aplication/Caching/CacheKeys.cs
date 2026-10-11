using System;

namespace Shop.Application.Caching
{
    public static class CacheKeys
    {
        // Products
        public static string ProductById(Guid id) => $"product:{id}";
        public static string ProductBySlug(string slug) => $"product:slug:{slug}";
        public const string ProductsByFilterPattern = "products:filter:*";
        public static string ProductsByFilter(string filterHash) => $"products:filter:{filterHash}";

        // Categories
        public static string CategoryById(Guid id) => $"category:{id}";
        public static string CategoriesByParent(Guid parentId) => $"categories:parent:{parentId}";
        public const string CategoriesList = "categories:list";
        public const string CategoriesAllPattern = "categories:*";

        // Roles
        public static string RoleById(Guid id) => $"role:{id}";
        public const string RolesAllPattern = "role:*";

        // Sellers
        public static string SellerById(Guid id) => $"seller:{id}";
        public static string SellerByUserId(Guid userId) => $"seller:user:{userId}";
        public static string SellerInventories(Guid sellerId) => $"seller:{sellerId}:inventories";
        public const string SellersAllPattern = "seller:*";

        // Users 
        public static string UserById(Guid id) => $"user:{id}";
        public static string UserAddresses(Guid userId) => $"user:{userId}:addresses";
        public static string UserWallets(Guid userId) => $"user:{userId}:wallets";

        // Orders 
        public static string OrderById(Guid id) => $"order:{id}";
        public static string OrdersByUserId(Guid userId) => $"user:{userId}:orders";

        // Comments 
        public static string CommentById(Guid id) => $"comment:{id}";
        public static string CommentsByProductId(Guid productId) => $"product:{productId}:comments";
    }
}
