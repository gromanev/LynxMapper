using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Data.DataModels
{
    [Table("routes")]
    public class Routes
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("start_location_id")]
        public int StartLocationId { get; set; }

        [Column("destination_location_id")]
        public int DestinationLocationId { get; set; }

        public virtual Locations StartLocation { get; set; }
        public virtual Locations DestinationLocation { get; set; }
    }
}
