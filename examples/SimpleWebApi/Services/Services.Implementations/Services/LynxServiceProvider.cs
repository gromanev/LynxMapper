using Services.Abstractions.Services;

namespace Services.Implementations.Services
{
    public class LynxServiceProvider: ILynxServiceProvider
    {
        public LynxServiceProvider(ITripService tripService)
        {
            TripService = tripService;
        }

        public ITripService TripService { get; }
    }
}
