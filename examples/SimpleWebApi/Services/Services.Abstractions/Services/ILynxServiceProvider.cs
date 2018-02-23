namespace Services.Abstractions.Services
{
    public interface ILynxServiceProvider
    {
        ITripService TripService { get; }
    }
}
