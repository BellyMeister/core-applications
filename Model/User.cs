﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public RoleEnum Role { get; set; }
        [Required]
        [StringLength(maximumLength: 3, MinimumLength = 2)]
        public string LandCode { get; set; }
        [Required]
        [StringLength(maximumLength: 10, MinimumLength = 8)]
        public string Number { get; set; }
        public List<PhoneRecord> Records { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"Navn: {Name}\n");
            sb.Append($"Email: {Email}\n");
            sb.Append($"Rolle: {Role}\n");
            sb.Append($"Tlf nummer: +{LandCode} {Number}\n");
            foreach (var record in Records)
            {
                sb.Append("----------------------------- \n");
                sb.Append(record.ToString());
            }

            return sb.ToString();
        }
    }


    public enum RoleEnum
    {
        Customer = 1,
        Employee = 2,
        Manager = 3
    }
}
