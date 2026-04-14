using JobListingsAPI.Data;
using JobListingsAPI.Models;

namespace JobListingsAPI.Services
{
    public class JobService : IJobService
    {
        private readonly AppDbContext _context;
        public JobService(AppDbContext context)
            => _context = context;
        public IEnumerable<JobListing> GetAllActive()
        {
            return _context.jobListings.Where(I=>I.IsActive).ToList();
        }

        public JobListing? GetById(int id)
        {
            return _context.jobListings.FirstOrDefault(J=>J.Id == id);
        }
        public void Create(JobListing Job)
        {
            Job.PostedAt= DateTime.UtcNow;
            Job.IsActive = true;
            _context.jobListings.Add(Job);
            _context.SaveChanges();
        }

        public void Update(int id, JobListing Job)
        {
            var existingKey = _context.jobListings.FirstOrDefault(J => J.Id == id)
                ?? throw new KeyNotFoundException($"Job with ID {id} not found.");
            existingKey.Salary = Job.Salary;
            existingKey.Location= Job.Location;
            existingKey.Title= Job.Title;
            existingKey.Company= Job.Company;

            _context.SaveChanges();
        }

        public void SoftDelete(int id)
        {
            var job = _context.jobListings.FirstOrDefault(J => J.Id == id)
            ?? throw new KeyNotFoundException($"Job with ID {id} not found.");

            job.IsActive = false;
            _context.SaveChanges();
        }

    }
}
