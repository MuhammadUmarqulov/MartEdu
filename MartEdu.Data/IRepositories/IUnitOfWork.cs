using System;
using System.Threading.Tasks;

namespace MartEdu.Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ICourseRepository Courses { get; }

        Task SaveChangesAsync();
    }
}
