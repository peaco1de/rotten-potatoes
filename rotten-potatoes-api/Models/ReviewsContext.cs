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

		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlServer("Server=localhost;Integrated Security=false;Initial Catalog='RottenPotatoes';User Id='sa';Password='f09d5d14-7a24-4952-902c-a2eca2c2fa66';application name=RottenPotatoes");

		protected override void OnModelCreating(ModelBuilder builder)
        {
			builder.Entity<Review>()
				.HasKey("ReviewId");

			builder.Entity<Review>()
				.Property("ReviewId")
				.ValueGeneratedOnAdd();
		}
	}
}