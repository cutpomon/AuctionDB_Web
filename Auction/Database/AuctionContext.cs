using EFCoreFirstDB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreFirstDB
{
    public class AuctionContext : DbContext
    {
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemProperty> ItemProperties { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Seller> Sellers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AuctionDB;
                Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
