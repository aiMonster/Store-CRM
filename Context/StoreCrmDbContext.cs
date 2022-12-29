﻿using System;
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
    }
}
