using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosterApp.Models
{
    class RosterData
    {
        public string username { get; set; }
        public DateTime day { get; set; }
        public TimeSpan startTime { get; set; }
        public TimeSpan endTime { get; set; }

        public RosterData(string username, DateTime day, TimeSpan startTime, TimeSpan endTime)
        {
            this.username = username;
            this.day = day;
            this.startTime = startTime;
            this.endTime = endTime;
        }

        public override string ToString()
        {
            return username + " " + day.ToString("dd/MM/yyyy") + " " + startTime.ToString("hh:mm:ss tt") + " " + endTime.ToString("hh:mm:ss tt");
        }
    }
}
