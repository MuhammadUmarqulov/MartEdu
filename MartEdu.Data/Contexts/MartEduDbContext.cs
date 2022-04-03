using MartEdu.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;


namespace MartEdu.Data.Contexts
{
    public class MartEduDbContext : DbContext
    {
        public MartEduDbContext(DbContextOptions<MartEduDbContext> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
    }
}
