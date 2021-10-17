
using System.Linq;
using BrusLib;

namespace Karate {
    public class DisplaySchoolDay {
        public SchoolDay sd { get; private set; }

        public bool displayDay { get; private set; }
        public string dayDisplay { get; private set; }

        public DisplaySchoolDay(SchoolDay d) {
            dayDisplay = d.day.DayOfWeek.ToString() + "  ";
            dayDisplay += d.day.ToString("dd.MM");
            sd = d;
            displayDay = sd.lessons.Any((w) => w.name != "");
        }
    }
}