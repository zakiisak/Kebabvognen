using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kebabvognen
{
    public class OpeningHours
    {
        //1 is monday, 7 is sunday
        private byte day;
        private TimeSpan from;
        private TimeSpan to;

        public byte Day { get => day; }
        public TimeSpan From { get => from; }
        public TimeSpan To { get => to; }

        public OpeningHours(byte day, TimeSpan from, TimeSpan to)
        {
            this.day = day;
            this.from = from;
            this.to = to;
        }
        
        public string GetDayString()
        {
            switch(day)
            {
                case 1: return "Mandag";
                case 2: return "Tirsdag";
                case 3: return "Onsdag";
                case 4: return "Torsdag";
                case 5: return "Fredag";
                case 6: return "Lørdag";
                case 7: return "Søndag";
            }
            return "Ukendt dag";
        }

    }
}
