using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    public class User
    {
        public User ()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }
        public string LandCode { get; set; }
        public string Number { get; set; }
        public List<PhoneRecord> Records { get; set; }
    }

    public enum RoleEnum
    {
        Customer = 1,
        Employee = 2,
        Manager = 3
    }

    class EnumGuid : Attribute
    {
        public Guid Guid;

        public EnumGuid(string guid)
        {
            Guid = new Guid(guid);
        }
    }
}
