namespace Shop.Application._Utilities
{
    public static class Directories
    {
        public const string ProductImages = "images/products";
        public const string ProductGallery = "images/products/gallery";
        public const string ProductThumbnails = "images/products/thumbnails";

        public const string CategoryImages = "images/categories";
        public const string CategoryIcons = "images/categories/icons";
        public const string CategoryBanners = "images/categories/banners";

        public const string UserAvatars = "images/users/avatars";
        public const string UserDocuments = "documents/users";

        public const string BannerImages = "images/banners";
        public const string SliderImages = "images/sliders";
        public const string AdvertisementImages = "images/advertisements";

        public const string BlogImages = "images/blog";
        public const string BlogThumbnails = "images/blog/thumbnails";
        public const string BlogAuthorAvatars = "images/blog/authors";

        public const string CommentImages = "images/comments";

        public const string BrandLogos = "images/brands";
        public const string BrandBanners = "images/brands/banners";

        public const string RoleIcons = "images/roles";

        public const string SellerLogos = "images/sellers/logos";
        public const string SellerBanners = "images/sellers/banners";
        public const string SellerDocuments = "documents/sellers";

        public const string OrderInvoices = "documents/orders/invoices";
        public const string OrderAttachments = "documents/orders/attachments";

        public const string SiteLogo = "images/site/logo";
        public const string SiteFavicon = "images/site/favicon";
        public const string SiteBackgrounds = "images/site/backgrounds";

        public const string PageImages = "images/pages";
        public const string PageBanners = "images/pages/banners";

        public const string DiscountBanners = "images/discounts";

        public const string ReportFiles = "documents/reports";
        public const string ExportFiles = "documents/exports";

        public const string TempFiles = "temp";
        public const string Uploads = "uploads";

        public static string ProductImagesById(Guid productId)
            => $"{ProductImages}/{productId}";

        public static string CategoryImagesById(Guid categoryId)
            => $"{CategoryImages}/{categoryId}";

        public static string UserAvatarsById(Guid userId)
            => $"{UserAvatars}/{userId}";

        public static string BlogImagesById(Guid blogId)
            => $"{BlogImages}/{blogId}";
    }
}