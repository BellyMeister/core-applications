using System;

namespace Interfaces
{
    public interface IBLoC
    {
        string Login(string email, string password);
        bool CreateUser(string sender, string userToAdd);
        bool UpdateUserRole(string sender, string userToUpdate, int newRole);
        string GetPhoneRecords(string sender, string user, DateTime? sinceDate = null);
        bool ValidateNumber(string number);
        string GenerateNumber();
    }
}
