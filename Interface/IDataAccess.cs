using Model;
using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IDataAccess
    {
        User Login(string email, string password);
        bool CreateUser(User sender, User userToAdd);
        bool UpdateUserRole(User sender, User userToUpdate, int newRole);
        List<PhoneRecord> GetPhoneRecords(User sender, User user, DateTime? sinceDate = null);
        bool ValidateNumber(string number);
        string GenerateNumber();
    }
}
