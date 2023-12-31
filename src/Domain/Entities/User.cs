﻿using Domain.Shared.Interfaces;
using Domain.Vaidator;

namespace Domain.Entities
{
    public class User : BaseEntity, IValidar
    {
        public string UserName { get; private set; }
        public string FullName { get; private set; }
        public string Password { get; private set; }
        public string RefreshToken { get; private set; } = string.Empty;
        public DateTime RefreshTokenExpyTime { get; private set; }

        private User() { }

        public User(string userName, string fullName, string password)
        {
            UserName = userName;
            FullName = fullName;
            Password = password;
        }

        // Method to update the UserName
        public void UpdateUserName(string newUserName)
        {
            // Add validation if necessary
            UserName = newUserName;
        }

        // Method to update the FullName
        public void UpdateFullName(string newFullName)
        {
            // Add validation if necessary
            FullName = newFullName;
        }

        // Method to update the Password
        public void UpdatePassword(string newPassword)
        {
            // Add validation if necessary
            Password = newPassword;
        }

        // Method to update the RefreshToken
        public void UpdateRefreshToken(string newRefreshToken, int daysToExpiry)
        {
            RefreshToken = newRefreshToken;
            RefreshTokenExpyTime = DateTime.UtcNow.AddDays(daysToExpiry);
        }

        public void RevokeToken() => RefreshToken = string.Empty;

        public void Validar()
            => Validate(new UserValidator(), this);
    }
}
