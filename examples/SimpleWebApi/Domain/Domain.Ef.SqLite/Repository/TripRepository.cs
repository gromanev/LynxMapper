using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Data.DataModels;
using Domain.Ef.Repository;
using Microsoft.EntityFrameworkCore;

namespace Domain.Ef.SqLite.Repository
{
    public class TripRepository: ITripRepository
    {
        private readonly MyAppContext _context;

        public TripRepository(MyAppContext context)
        {
            _context = context;
        }

        public IQueryable<Trips> GetAllTrips()
        {
            return _context.Trips
                .Include(x => x.Driver)
                .Include(x => x.Passangers)
                .Include(x => x.Route).ThenInclude(x => x.StartLocation)
                .Include(x => x.Route).ThenInclude(x => x.DestinationLocation);
        }

        public Trips GetTrip(int id)
        {
            return GetAllTrips().FirstOrDefault(x => x.Id == id);
        }
    }
}
