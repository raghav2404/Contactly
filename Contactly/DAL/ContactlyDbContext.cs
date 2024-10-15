using System;
using Contactly.Controllers.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Contactly.DAL
{
	public class ContactlyDbContext:DbContext
	{
		public ContactlyDbContext(DbContextOptions options):base(options)
		{

		}

		public DbSet<Contact> Contacts { get; set; }


	}
	
}

