using MartEdu.Domain.Entities.Authors;
using MartEdu.Domain.Entities.Courses;
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
        public virtual DbSet<Course> Courses { get; set; }  
        public virtual DbSet<Author> Authors { get; set; }
    }
}
