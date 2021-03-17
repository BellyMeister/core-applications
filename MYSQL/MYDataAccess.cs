using Interfaces;
using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace MYSQL
{
    public class MYDataAccess : IDataAccess
    {
        public static MySqlConnection OpenConnection()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; Port=3306; Database=CoreApplication; Uid=root; Pwd=");
                conn.Open();
                return conn;
            }
            catch (Exception e)
            {
                return null;
                throw;
            }
        }
        public bool CreateUser(User sender, string name, string email, string password, int role, string phoneNumber = null, string landCode = null)
        {
            if (sender.Role == RoleEnum.Customer) return false;

            MySqlConnection conn = OpenConnection();
            string command = "INSERT INTO Users(Id, FullName, Email, PWord, UserRole, LandCode, PhoneNumber) " +
                $"VALUES ('{Guid.NewGuid()}', '{name}', '{email}', '{password}', '{role}', '{landCode}', '{phoneNumber}')";
            int result = new MySqlCommand(command, conn).ExecuteNonQuery();
            conn.Close();

            if (result != 0) return true;
            return false;
        }

        public List<PhoneRecord> GetPhoneRecords(User sender, User user, DateTime? sinceDate = null)
        {
            List<PhoneRecord> output = new List<PhoneRecord>();
            if (sender.Role == RoleEnum.Customer && sender != user) return new List<PhoneRecord>();
            MySqlConnection conn = OpenConnection();
            string command = $"SELECT * FROM PhoneRecords WHERE CallerID = '{user.Id}' OR RecieverId = '{user.Id}'";

            if (sinceDate != null)
                command += $" AND CallStart >= '{sinceDate}'";

            using (var reader = new MySqlCommand(command, conn).ExecuteReader())
            {
                while (reader.Read())
                {
                    output.Add(new PhoneRecord()
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        CallStart = DateTime.Parse(reader["CallStart"].ToString()),
                        CallEnd = DateTime.Parse(reader["CallEnd"].ToString()),
                        CallerId = Guid.Parse(reader["CallerId"].ToString()),
                        RecieverId = Guid.Parse(reader["RecieverId"].ToString()),
                    });
                }
            }
            conn.Close();
            return output;
        }

        public User Login(string email, string password)
        {
            MySqlConnection conn = OpenConnection();
            string command = $"SELECT * FROM Users WHERE Email = '{email}' AND PWord = '{password}'";
            using (var reader = new MySqlCommand(command, conn).ExecuteReader())
            {
                while (reader.Read())
                {
                    if (!reader.HasRows) return new User();
                    string id = Encoding.UTF8.GetString((byte[])reader["id"]);
                    var user = new User()
                    {
                        Id = Guid.Parse(id),
                        Name = reader["FullName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = "",
                        Role = (RoleEnum)reader["UserRole"],
                        LandCode = reader["LandCode"].ToString(),
                        Number = reader["PhoneNumber"].ToString(),
                        Records = new List<PhoneRecord>()
                    };
                    user.Records = GetPhoneRecords(user, user);
                    return user;
                }
                return null;
            }
        }

        public bool UpdateUserRole(User sender, User userToUpdate, int newRole)
        {
            if (sender.Role != RoleEnum.Manager || (sender.Role == RoleEnum.Employee && userToUpdate.Role == RoleEnum.Employee) || sender.Role == RoleEnum.Customer)
                return false;

            MySqlConnection conn = OpenConnection();
            string command = $"UPDATE Users SET UserRole = '{newRole}' WHERE Id = '{userToUpdate.Id}'";

            if (new MySqlCommand(command, conn).ExecuteNonQuery() != 0)
                return true;
            return false;
        }
    }
}