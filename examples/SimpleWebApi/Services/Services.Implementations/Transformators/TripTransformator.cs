using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Data.DataModels;
using Domain.Data.ViewModels;
using Services.Abstractions.Transformators;

namespace Services.Implementations.Transformators
{
    public class TripTransformator: ITripTransformator
    {
        private readonly IUserTransformator _userTransformator;

        public TripTransformator(IUserTransformator userTransformator)
        {
            _userTransformator = userTransformator;
        }

        public TripViewModel ToTripViewModel(Trips trip)
        {
            return new TripViewModel
            {
                Id = trip.Id,
                StartTime = trip.StartTime.ToLongDateString(),
                Driver = _userTransformator.ToUserFullViewModel(trip.Driver),
                DestinationLocation = trip.Route?.DestinationLocation?.LocationName ?? "Нет информации о пункте прибытия",
                StartLocation = trip.Route?.StartLocation?.LocationName ?? "Нет информации о пункте посадки",
                // Passangers = trip.Passangers?.Select(x => _userTransformator.ToUserViewModel(x?.Passanger)).ToList() ?? null
            };
        }
    }
}
