namespace Domain.Data.ViewModels
{
    /// <summary>
    /// Модель отображения информации о человеке
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// Имя человека
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возраст
        /// </summary>
        public int Age { get; set; }
    }
}
