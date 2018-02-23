using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Data.DataModels
{
    [Table("locations")]
    public class Locations
    {
        public Locations()
        {
            StartRoutes = new HashSet<Routes>();
            DestRoutes = new HashSet<Routes>();
        }

        [Column("id")]
        public int Id { get; set; }

        [Column("location")]
        public string LocationName { get; set; }

        public virtual ICollection<Routes> StartRoutes { get; set; }
        public virtual ICollection<Routes> DestRoutes { get; set; }
    }
}
