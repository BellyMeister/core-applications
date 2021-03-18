using System;
using System.Collections.Generic;
using Model;
using MSSQL;
using Interfaces;
using MYSQL;
using Business_Logic;
using Newtonsoft;


namespace Presentation
{
    class Program
    {
        static BLoC bloc = new BLoC();
        static void Main(string[] args)
        {
            Random rnd = new Random();
            var localUser = Newtonsoft.Json.JsonConvert.SerializeObject(new User()
            {
                Name = "Bo bodilsen",
                Email = "G@f.asd",
                Password = "",
                Role = RoleEnum.Manager,
                LandCode = "45",
                Number = "12345678",
                Records = new List<PhoneRecord>()
            });
            // CREATE USER
            var userToAdd = Newtonsoft.Json.JsonConvert.SerializeObject(new User()
            {
                Name = "Bent",
                Email = $"hej@med.{rnd.Next(0, 1000)}",
                Password = "Oi",
                Role = RoleEnum.Manager,
                Number = $"{rnd.Next(10000000, 99999999)}",
                LandCode = "45"
            });

            if (bloc.CreateUser(localUser, userToAdd))
                Console.WriteLine("User created");
            else
                Console.WriteLine("User creation failed");

            // LOGIN
            var loggedInUser = bloc.Login("test", "Hej");
            Console.WriteLine(loggedInUser == null ? "Login failed" : "Login Success");
            
            // GET PHONE RECORDS
            List<PhoneRecord> records = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PhoneRecord>>(bloc.GetPhoneRecords(localUser, loggedInUser));

            foreach (var record in records)
            {
                Console.WriteLine(record.Id);
            }

            
            // UPDATE USER ROLE TO RANDOM ROLE
            int rndN = rnd.Next(1, 4);
            if (bloc.UpdateUserRole(localUser, loggedInUser, rndN))
                Console.WriteLine($"Updated user's role to {(RoleEnum)rndN}");
            else
                Console.WriteLine("Update failed");
        }
    }
}
