﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Domain.Models.Products;
using System.Threading.Tasks;

namespace Application.Storages
{
    public partial class Storage
    {
        public DbSet<Product> Products { get; set; }

        public async ValueTask<Product> InsertProductAsync(Product product)
        {
            EntityEntry<Product> entityEntry = await this.Products.AddAsync(product);
            await this.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}