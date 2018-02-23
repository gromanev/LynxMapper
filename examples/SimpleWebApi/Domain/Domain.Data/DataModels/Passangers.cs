using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Data.DataModels
{
    [Table("passangers")]
    public class Passangers
    {
        [Column("passanger_id")]
        public int PassangerId { get; set; }

        [Column("trip_id")]
        public int TripId { get; set; }

        public virtual Users Passanger { get; set; }
        public virtual Trips Trip { get; set; }

    }
}
