using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace _4_29_EFImages.data2
{
    public class ImageContext : DbContext
    {
        private string _connString;

        public ImageContext(string connString)
        {
            _connString = connString;
        }

        public DbSet<Image> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connString);
        }

    }
}
