﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain.Data.DataModels;
using Domain.Data.ViewModels;
using LynxMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions.Services;
using Services.Abstractions.Transformators;
using Services.Implementations.Services;
using LynxMapper = LynxMapper.LynxMapper;

namespace WebApp.Controllers
{
    [Produces("application/json")]
    [Route("api/trip/[action]")]
    public class TripController: Controller
    {
        private readonly ITripService _tripService;
        private readonly ILynxMapper _mapper;

        public TripController(ILynxServiceProvider lynxServiceProvider, ILynxMapper mapper)
        {
            _tripService = lynxServiceProvider.TripService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list all trips
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", Type = typeof(ICollection<TripViewModel>))]
        public async Task<IActionResult> GetAllTrips()
        {
            try
            {
                var trips = await _tripService.GetAllTripsAsync();

                var ts = new TimeSpan();
                var result = trips.Select(x => _mapper.Map<TripViewModel, Trips>(x)).ToList();

                return Ok(new
                {
                    totalTime = ts.TotalMilliseconds,
                    //result = result
                });
            }
            catch (Exception)
            {
                return BadRequest("Trips bad request.");
            }
        }
    }
}
