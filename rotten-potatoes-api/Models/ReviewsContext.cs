using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace rotten_potatoes_api.Models 
{
	public class ReviewsContext : DbContext
	{
		public DbSet<Review> Reviews { get;	set; }
		public DbSet<User> Users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlServer("Server=localhost;Integrated Security=false;Initial Catalog='RottenPotatoes';User Id='sa';Password='f09d5d14-7a24-4952-902c-a2eca2c2fa66';application name=RottenPotatoes");

		protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Review

            builder.Entity<Review>()
				.HasKey(new[] { "GameId", "UserId" });

			builder.Entity<Review>()
				.Property("AddDate")
				.HasDefaultValue(DateTime.Now);

			#endregion

			#region

			builder.Entity<User>()
				.HasKey("Id");

			builder.Entity<User>()
				.HasIndex(o => o.UserName)
				.IsUnique();

			builder.Entity<User>()
				.Property("Id")
				.ValueGeneratedOnAdd();

			builder.Entity<User>()
				.Property("UserName");

			builder.Entity<User>()
				.HasMany(o => o.Reviews)
				.WithOne(o => o.User);

			#endregion

		}
    }
}