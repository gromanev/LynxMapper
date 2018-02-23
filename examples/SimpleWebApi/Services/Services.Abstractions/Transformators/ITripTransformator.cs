using System;
using System.Collections.Generic;
using System.Text;
using Domain.Data.DataModels;
using Domain.Data.ViewModels;
using LynxMapper;

namespace Services.Abstractions.Transformators
{
    public interface ITripTransformator: ILynxTransformator
    {
        TripViewModel ToTripViewModel(Trips trip);
    }
}
