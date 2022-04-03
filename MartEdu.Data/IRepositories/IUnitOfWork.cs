using System;
using System.Threading.Tasks;

namespace MartEdu.Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository Users { get; }

        Task SaveChangesAsync();
    }
}
