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
				.HasKey(o => o.ReviewId);

			builder.Entity<Review>()
				.HasIndex(o => new { o.GameId, o.UserId })
				.IsUnique();

			builder.Entity<Review>()
				.Property(o => o.AddDate)
				.HasDefaultValue(DateTime.Now);

			#endregion

			#region

			builder.Entity<User>()
				.HasKey(o => o.UserId);

			builder.Entity<User>()
				.HasIndex(o => o.UserName)
				.IsUnique();

			builder.Entity<User>()
				.Property(o => o.UserId)
				.ValueGeneratedOnAdd();

			builder.Entity<User>()
				.Property(o => o.UserName);

			#endregion

		}
    }
}