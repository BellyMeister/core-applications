using Interfaces;
using Model;
using MSSQL;
using MYSQL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Business_Logic
{
    public class BLoC : IBLoC
    {
        IDataAccess da = new MSDataAccess();
        //IDataAccess da = new MYDataAccess();

        public bool CreateUser(string sender, string newUser)
        {
            try
            {
                User userToAdd = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(newUser);
                User userSender = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(sender);
                if (userToAdd.Number == null)
                    userToAdd.Number = da.GenerateNumber();

                var context = new ValidationContext(userToAdd, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();

                if  (userSender.Role == RoleEnum.Customer
                    || !Validator.TryValidateObject(userToAdd, context, validationResults, true)
                    || !da.ValidateNumber(userToAdd.Number))
                {
                    return false;
                }
                return da.CreateUser(userSender, userToAdd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public string GenerateNumber()
        {
            return da.GenerateNumber();
        }

        public string GetPhoneRecords(string sender, string user, DateTime? sinceDate = null)
        {
            User userSender = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(sender);
            User userUser = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(user);

            return Newtonsoft.Json.JsonConvert.SerializeObject(da.GetPhoneRecords(userUser, userUser, sinceDate));
        }

        public string Login(string email, string password)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(da.Login(email, password));
        }

        public bool UpdateUserRole(string sender, string userToUpdate, int newRole)
        {
            User userSender = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(sender);
            User userUserToUpdate = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(userToUpdate);
            return da.UpdateUserRole(userSender, userUserToUpdate, newRole);
        }

        public bool ValidateNumber(string number)
        {
            return da.ValidateNumber(number);
        }
    }
}
