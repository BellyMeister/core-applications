using System;
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
        public Guid CallerId { get; set; }
        public Guid RecieverId { get; set; }
        public string CallerName { get; set; }
        public string RecieverName { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            var callDur = CallEnd.Subtract(CallStart);

            sb.Append($"Opkald start: {CallStart.ToString("dd/MM/yyyy H:mm:ss")}\n");
            sb.Append($"Opkald slut: {CallEnd.ToString("dd/MM/yyyy H:mm:ss")}\n");

            sb.Append($"Opkalder navn: {CallerName}\n");
            sb.Append($"Modtager navn: {RecieverName}\n");

            sb.Append($"Opkald varighed:");
            if (callDur.Days != 0)
            {
                sb.Append($" {callDur.Days} ");
                sb.Append(callDur.Days > 1 ? "dage" : "dag");
            }
            if (callDur.Hours != 0)
            {
                sb.Append($" {callDur.Hours} ");
                sb.Append(callDur.Hours > 1 ? "timer" : "time");
            }
            if (callDur.Minutes != 0)
            {
                sb.Append($" {callDur.Minutes} ");
                sb.Append(callDur.Minutes > 1 ? "minutter" : "minut");
            }
            if (callDur.Seconds != 0)
            {
                sb.Append($" {callDur.Seconds} ");
                sb.Append(callDur.Seconds > 1 ? "sekunder" : "sekund");
            }
            sb.Append("\n");

            sb.Append($"Opkalder ID: {CallerId}\n");
            sb.Append($"Modtager ID: {RecieverId}");
            
            return sb.ToString();
        }
    }
}
