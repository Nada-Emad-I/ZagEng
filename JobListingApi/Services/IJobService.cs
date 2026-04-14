using JobListingsAPI.Models;

namespace JobListingsAPI.Services
{
    public interface IJobService
    {
        IEnumerable<JobListing> GetAllActive();
        JobListing? GetById(int id);
        void Create(JobListing Job);
        void Update(int id ,JobListing Job);
        void SoftDelete(int id);

    }
}
