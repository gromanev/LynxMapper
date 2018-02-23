using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Data.DataModels
{
    [Table("trips")]
    public class Trips
    {
        public Trips()
        {
            Passangers = new HashSet<Passangers>();
        }

        [Column("id")]
        public int Id { get; set; }

        [Column("driver_id")]
        public int DriverId { get; set; }

        [Column("route_id")]
        public int RouteId { get; set; }

        [Column("start_time")]
        public DateTime StartTime { get; set; }

        public virtual Routes Route { get; set; }
        public virtual Users Driver { get; set; }

        public virtual ICollection<Passangers> Passangers { get; set; }
    }
}
