using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ef.Repository
{
    public interface IUnitOfWork
    {
        ITripRepository TripRepository { get; }
    }
}
