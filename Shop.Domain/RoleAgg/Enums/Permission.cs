namespace Shop.Domain.RoleAgg
{
    public partial class Role
    {
        public enum Permission
        {
            UserView = 1, 
            UserCreate = 2,   
            UserEdit = 3, 
            UserDelete = 4, 
            RoleView = 5,
            RoleCreate = 6,
            RoleEdit = 7,  
            RoleDelete = 8, 
            ProductView = 9,  
            ProductCreate = 10,   
            ProductEdit = 11,  
            ProductDelete = 12, 
            CategoryManagement = 13, 
            OrderView = 14, 
            OrderEdit = 15, 
            OrderDelete = 16, 
            OrderStatusManagement = 17,
            WalletView = 18,     
            WalletCharge = 19,   
            TransactionView = 20,  
            DiscountManagement = 21, 
            BannerManagement = 22,  
            SliderManagement = 23,
            BlogManagement = 24,      
            CommentManagement = 25,  
            SalesReport = 26,    
            UserReport = 27,       
            ProductReport = 28,   
            SiteSettings = 29,    
            PaymentSettings = 30,    
            ShippingSettings = 31,  
            SuperAdmin = 32  
        }
    }
}