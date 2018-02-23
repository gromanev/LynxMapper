using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Data.DataModels;

namespace Domain.Ef.Repository
{
    public interface ITripRepository
    {
        IQueryable<Trips> GetAllTrips();
        Trips GetTrip(int id);
    }
}
