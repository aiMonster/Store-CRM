using System;
using Microsoft.EntityFrameworkCore;
using StoreCRM.Entities;

namespace StoreCRM.Context
{
    public class StoreCrmDbContext : DbContext
    {
        public StoreCrmDbContext(DbContextOptions<StoreCrmDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Attachment> Attachments => Set<Attachment>();
        public DbSet<StoreTask> Tasks => Set<StoreTask>();
        public DbSet<Stock> Stocks => Set<Stock>();
        public DbSet<Posting> Postings => Set<Posting>();
        public DbSet<PostingProduct> PostingProducts => Set<PostingProduct>();
    }
}
