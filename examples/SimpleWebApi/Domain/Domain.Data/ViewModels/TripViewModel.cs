using System.Collections.Generic;

namespace Domain.Data.ViewModels
{
    /// <summary>
    /// Модель для отображения информации о поездке
    /// </summary>
    public class TripViewModel
    {
        /// <summary>
        /// Идентификатор поездки
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Водитель
        /// </summary>
        public UserFullViewModel Driver { get; set; }

        /// <summary>
        /// Список пассажиров
        /// </summary>
        public ICollection<UserViewModel> Passangers { get; set; }

        /// <summary>
        /// Место назначения
        /// </summary>
        public string DestinationLocation { get; set; }

        /// <summary>
        /// Откуда выезжаем
        /// </summary>
        public string StartLocation { get; set; }

        /// <summary>
        /// Время отправления
        /// </summary>
        public string StartTime { get; set; }
    }
}
