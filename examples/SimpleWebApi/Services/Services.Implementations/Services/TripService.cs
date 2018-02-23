using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Data.DataModels;
using Domain.Ef.Repository;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions.Services;

namespace Services.Implementations.Services
{
    public class TripService: ITripService
    {
        private readonly ITripRepository _tripRepository;

        public TripService(IUnitOfWork unitOfWork)
        {
            _tripRepository = unitOfWork.TripRepository;
        }

        public async Task<ICollection<Trips>> GetAllTripsAsync()
        {
            return await _tripRepository.GetAllTrips().ToListAsync();
        }
    }
}
