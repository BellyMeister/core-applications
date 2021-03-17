using System;
using System.Collections.Generic;
using Model;
using MSSQL;
using Interfaces;
using MYSQL;

namespace Presentation
{
    class Program
    {
        static MSDataAccess da = new MSDataAccess();  // MS SQL
        //static MYDataAccess da = new MYDataAccess();    // MY SQL
        static void Main(string[] args)
        {
            Random rnd = new Random();
            User localUser = new User()
            {
                Name = "Bo bodilsen",
                Email = "G@f.asd",
                Password = "",
                Role = RoleEnum.Manager,
                LandCode = "45",
                Number = "12345678",
                Records = new List<PhoneRecord>()
            };

            if (da.CreateUser(localUser, "bent", $"hej@med.{rnd.Next(0,1000)}", "", (int)RoleEnum.Manager, rnd.Next(11111111, 99999999).ToString(), "45"))
                Console.WriteLine("User created");
            else
                Console.WriteLine("User creation failed");
            //f95b8af3-77ec-4afa-91c3-c9e83cb88be0
            //59543c14-5167-4588-8854-a086713f7587
            User loggedInUser = da.Login("test", "Hej");
            Console.WriteLine(loggedInUser == null ? "Login failed" : "Login Success");

            foreach (var record in da.GetPhoneRecords(localUser, loggedInUser))
            {
                Console.WriteLine(record.Id);
            }


            // Customer - 8bf36e85-6458-495a-9bee-b1e3a96d8a0f
            // Manager  - 28c7e2e9-9b7f-455f-bf94-e2f334b7f1b9
            // Employee - 9612a7ac-fa5a-43c2-bee8-f3badab30eca
            int rndN = rnd.Next(1, 4);
            if (da.UpdateUserRole(localUser, loggedInUser, rndN))
                Console.WriteLine($"Updated {loggedInUser.Name}'s role to {(RoleEnum)rndN}");
            else
                Console.WriteLine("Update failed");

        }
    }
}
