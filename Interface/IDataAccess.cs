using Model;
using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IDataAccess
    {
        User Login(string email, string password);
        bool CreateUser(User sender, string name, string email, RoleEnum role, string phoneNumber = null, string landCode = null);
        bool UpdateUserRole(User sender, User userToUpdate, RoleEnum newRole);
        List<PhoneRecord> GetPhoneRecords(User user, DateTime? sinceDate = null);

    }
}
