using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Runtime;


namespace OtiumActio.Infrastructure
{
    public class DbFactory : IDisposable
    {
        private bool _disposed;
        private Func<OtiumActioContext> _instanceFunc;
        private DbContext _dbContext;
        public DbContext DbContext => _dbContext ?? (_dbContext = _instanceFunc.Invoke());
        public DbFactory(Func<OtiumActioContext> dbContextFactory)
        {
            _instanceFunc = dbContextFactory;
        }
        public void Dispose()
        {
            if (!_disposed && _dbContext != null)
            {
                _disposed = true;
                _dbContext.Dispose();
            }
        }
    }
}
