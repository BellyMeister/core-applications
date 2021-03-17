using Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MSSQL
{
    public class MSDataAccess : IDataAccess
    {
        public static SqlConnection OpenConnection()
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=CoreApplication;Persist Security Info=True;User ID=sa;Password=<Password1>");
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

            SqlConnection conn = OpenConnection();
            string command = "INSERT INTO Users(FullName, Email, PWord, UserRole, LandCode, PhoneNumber) " +
                $"VALUES ('{name}', '{email}', '{password}', '{role}', '{landCode}', '{phoneNumber}')";
            int result = new SqlCommand(command, conn).ExecuteNonQuery();

            conn.Close();

            if (result != 0) return true;
            return false;
        }

        public List<PhoneRecord> GetPhoneRecords(User sender, User user, DateTime? sinceDate = null)
        {
            List<PhoneRecord> output = new List<PhoneRecord>();
            if (sender.Role == RoleEnum.Customer && sender != user) return new List<PhoneRecord>();
            SqlConnection conn = OpenConnection();
            string command = $"SELECT * FROM PhoneRecords WHERE CallerID = '{user.Id}' OR RecieverId = '{user.Id}'";

            if (sinceDate != null)
                command += $" AND CallStart >= '{sinceDate}'";

            using (var reader = new SqlCommand(command, conn).ExecuteReader())
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
            SqlConnection conn = OpenConnection();
            string command = $"SELECT * FROM Users WHERE Email = '{email}' AND PWord = '{password}'";
            using(var reader = new SqlCommand(command, conn).ExecuteReader())
            {
                while (reader.Read())
                {
                    if (!reader.HasRows) return new User();
                    var id = Guid.Parse(reader["Id"].ToString());
                    var name = reader["FullName"].ToString();
                    var emailvar = reader["Email"].ToString();
                    var role = (RoleEnum)reader["UserRole"];
                    var landcode = reader["LandCode"].ToString();
                    var number = reader["PhoneNumber"].ToString();

                    var user = new User()
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
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

            SqlConnection conn = OpenConnection();
            string command = $"UPDATE Users SET UserRole = '{newRole}' WHERE Id = '{userToUpdate.Id}'";

            if (new SqlCommand(command, conn).ExecuteNonQuery() != 0)
                return true;
            return false;
        }
    }
}
