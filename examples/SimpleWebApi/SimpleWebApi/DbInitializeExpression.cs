using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bogus;
using Domain.Data.DataModels;
using Domain.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleWebApi
{
    public static class DbInitializeExpression
    {
        public static void DbInitialize(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var context = serviceProvider.GetService<MyAppContext>();

            if (File.Exists("trips.db"))
            {
                return;
            }
            else
            {
                context.Database.Migrate();
            }

            TestData(context);
        }

        private static void TestData(MyAppContext context)
        {
            #region Locations

            var fakeLocations = new Faker<Locations>("ru")
                .RuleFor(x => x.LocationName, f => f.Address.City());

            context.Locations.AddRange(fakeLocations.Generate(100));
            context.SaveChanges();

            #endregion

            #region Users

            var fakeUsers = new Faker<Users>("ru")
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.Patronimic, f => f.Name.Suffix())
                .RuleFor(x => x.IsDriver,
                    f => f.PickRandom(new List<bool> {false, false, true, false, false, false, false}))
                .RuleFor(x => x.YearOfBirth,
                    f => f.Date.Between(DateTime.Now.AddYears(-70), DateTime.Now.AddYears(-20)));

            context.Users.AddRange(fakeUsers.Generate(10000));
            context.SaveChanges();

            #endregion

            #region Routes

            var locations = context.Locations.ToList();

            var fakeRoutes = new Faker<Routes>("ru")
                .RuleFor(x => x.DestinationLocationId, f => f.PickRandom(locations.Select(s => s.Id)))
                .RuleFor(x => x.StartLocationId, f => f.PickRandom(locations.Select(s => s.Id)));

            context.Routes.AddRange(fakeRoutes.Generate(2000));
            context.SaveChanges();

            #endregion

            #region Trips

            var drivers = context.Users.Where(x => x.IsDriver == true).ToList();
            var routes = context.Routes.ToList();

            var fakeTrips = new Faker<Trips>("ru")
                .RuleFor(x => x.DriverId, f => f.PickRandom(drivers.Select(s => s.Id)))
                .RuleFor(x => x.RouteId, f => f.PickRandom(routes.Select(s => s.Id)))
                .RuleFor(x => x.StartTime, f => f.Date.Between(DateTime.Now.AddDays(1), DateTime.Now.AddDays(60)));

            context.Trips.AddRange(fakeTrips.Generate(1000));
            context.SaveChanges();

            #endregion

            #region Passangers

            var trips = context.Trips.ToList();
            var passangerUsers = context.Users.Where(x => x.IsDriver != true).ToList();

            var ran = new Random();

            foreach (var trip in trips)
            {
                var inDbUsers = new List<Passangers>();
                var pasUsers = passangerUsers.Take(ran.Next(15, 25)).ToList();

                foreach (var pasUser in pasUsers)
                {
                    passangerUsers.Remove(pasUser);

                    var passanger = new Passangers
                    {
                        TripId = trip.Id,
                        PassangerId = pasUser.Id
                    };

                    inDbUsers.Add(passanger);
                }

                context.Passangers.AddRange(inDbUsers);
            }

            context.SaveChanges();

            #endregion
        }
    }
}