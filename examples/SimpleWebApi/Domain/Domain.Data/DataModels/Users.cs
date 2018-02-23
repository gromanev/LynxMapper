using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Data.DataModels
{
    [Table("users")]
    public class Users
    {
        public Users()
        {
            Passangers = new HashSet<Passangers>();
            DriverTrips = new HashSet<Trips>();
        }

        [Column("id")]
        public int Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("patronimic")]
        public string Patronimic { get; set; }

        [Column("year_of_birth")]
        public DateTime YearOfBirth { get; set; }

        [Column("is_driver")]
        public bool? IsDriver { get; set; }

        public virtual ICollection<Passangers> Passangers { get; set; }
        public virtual ICollection<Trips> DriverTrips { get; set; }
    }
}
