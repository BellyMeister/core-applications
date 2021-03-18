using System;

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
        public Guid CallerId { get; set; }
        public Guid RecieverId { get; set; }
    }
}
