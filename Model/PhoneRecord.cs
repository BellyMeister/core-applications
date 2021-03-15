using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PhoneRecord
    {
        public PhoneRecord()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public DateTime CallStart { get; set; }
        public DateTime CallEnd { get; set; }
        public string CallerLandCode { get; set; }
        public string CallerPhoneNumber { get; set; }
        public string RecieverLandCode { get; set; }
        public string RecieverPhoneNumber { get; set; }
    }
}
