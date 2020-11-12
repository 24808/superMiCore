using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dream.Shop.DataEntity
{
    public class Dream_ShopContext:DbContext
    {
        public Dream_ShopContext(DbContextOptions<Dream_ShopContext> options) : base(options)
        {

        }
        public DbSet<Good_Coupon> Good_Coupons { get; set; }
        public DbSet<Good_Order> Good_Orders { get; set; }
        public DbSet<Good_Promotion> Good_Promotions { get; set; }
        public DbSet<Good_Specification> Good_Specifications { get; set; }
        public DbSet<GoodCategory> GoodCategories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<GoodCoupon> GoodCoupons { get; set; }
        public DbSet<GoodPromotion> GoodPromotions { get; set; }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<GoodSpecification> GoodSpecifications { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLog> OrderLogs { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<ShoppingAddress> ShoppingAddresses { get; set; }
        public DbSet<ShoppingCar> ShoppingCars { get; set; }
        public DbSet<GoodCollect> GoodCollects { get; set; }
        public DbSet<Community> Communities { get; set; }
    }
}
