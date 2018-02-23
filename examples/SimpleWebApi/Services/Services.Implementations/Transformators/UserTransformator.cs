using System;
using System.Collections.Generic;
using System.Text;
using Domain.Data.DataModels;
using Domain.Data.ViewModels;
using Services.Abstractions.Transformators;

namespace Services.Implementations.Transformators
{
    public class UserTransformator : IUserTransformator
    {
        public UserFullViewModel ToUserFullViewModel(Users user)
        {
            return new UserFullViewModel
            {
                Id = user.Id,
                Name = $"{user.LastName} {user.FirstName} {user.Patronimic}",
                Age = (int) ((DateTime.Now - user.YearOfBirth).TotalDays / 365.2425)
            };
        }

        public UserViewModel ToUserViewModel(Users user)
        {
            try
            {
                return new UserViewModel
                {
                    Name = $"{user.LastName} {user.FirstName} {user.Patronimic}",
                    Age = (int) ((DateTime.Now - user.YearOfBirth).TotalDays / 365.2425)
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}