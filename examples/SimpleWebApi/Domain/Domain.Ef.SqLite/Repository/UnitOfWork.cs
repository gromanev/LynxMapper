using System;
using System.Collections.Generic;
using System.Text;
using Domain.Ef.Repository;

namespace Domain.Ef.SqLite.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private bool _disposed = false;

        private readonly MyAppContext _context;

        private ITripRepository _tripRepository;

        public UnitOfWork(MyAppContext context)
        {
            _context = context ?? throw new ArgumentNullException("Context not implements");
        }

        public ITripRepository TripRepository => _tripRepository ?? (_tripRepository = new TripRepository(_context));

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
