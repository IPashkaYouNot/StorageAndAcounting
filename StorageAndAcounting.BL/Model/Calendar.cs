using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAndAcounting.BL.Model
{
    public class Calendar
    {
        public event EventHandler NewDay;

        public int DayOfWeek { get; private set; } = 1;

        public void MoveOneDay()
        {
            DayOfWeek++;
            if (DayOfWeek == 8) DayOfWeek = 1;
            if (DayOfWeek != 6 && DayOfWeek != 7)
            {
                NewDay?.Invoke(this, null);
            }   
        }

        public string GetDayOfWeek()
        {
            if (DayOfWeek == 1) return "Monday";
            if (DayOfWeek == 2) return "Tuesday";
            if (DayOfWeek == 3) return "Wednesday";
            if (DayOfWeek == 4) return "Thursday";
            if (DayOfWeek == 5) return "Friday";
            if (DayOfWeek == 6) return "Saturday";
            return "Sunday";
        }

    }
}
