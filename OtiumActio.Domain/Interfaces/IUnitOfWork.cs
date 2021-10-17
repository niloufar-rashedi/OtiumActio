using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OtiumActio.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
