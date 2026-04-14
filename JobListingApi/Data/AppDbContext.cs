using JobListingsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobListingsAPI.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
        public DbSet<JobListing> jobListings { get; set; }
    }
}
