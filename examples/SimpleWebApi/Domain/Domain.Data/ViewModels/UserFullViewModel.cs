using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.ViewModels
{
    public class UserFullViewModel: UserViewModel
    {
        /// <summary>
        /// ИД пользователя
        /// </summary>
        public int Id { get; set; }
    }
}
