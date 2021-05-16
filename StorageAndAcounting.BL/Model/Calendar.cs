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

        public int DayOfWeek { get; private set; } = 0;

        public void MoveOneDay()
        {
            DayOfWeek++;
            if (DayOfWeek == 8) DayOfWeek = 1;
            if (DayOfWeek != 6 && DayOfWeek != 7)
            {
                NewDay?.Invoke(this, null);
            }
        }

    }
}
