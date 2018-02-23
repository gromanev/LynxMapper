using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Data.DataModels;

namespace Services.Abstractions.Services
{
    public interface ITripService
    {
        Task<ICollection<Trips>> GetAllTripsAsync();
    }
}
