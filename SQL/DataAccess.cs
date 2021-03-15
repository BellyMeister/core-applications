using Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQL
{
    public class DataAccess : IDataAccess
    {
        public bool CreateUser(User sender, string name, string email, RoleEnum role, string phoneNumber = null, string landCode = null)
        {
            throw new NotImplementedException();
        }

        public List<PhoneRecord> GetPhoneRecords(User sender, User user, DateTime? sinceDate = null)
        {
            throw new NotImplementedException();
        }

        public User Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUserRole(User sender, User userToUpdate, RoleEnum newRole)
        {
            throw new NotImplementedException();
        }
    }
}
