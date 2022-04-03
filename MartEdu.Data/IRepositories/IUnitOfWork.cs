using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartEdu.Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository Users { get; }

        Task SaveChangesAsync();
    }
}
