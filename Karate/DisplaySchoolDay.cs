
using System.Collections.Generic;
using System.Linq;
using BrusLib;

namespace Karate {
    public class DisplaySchoolDay {
        public SchoolDay sd { get; private set; }

        public List<DisplayLesson> displayLessons { get; private set; } = new List<DisplayLesson>();
        
        public bool shouldDisplayLessons { get; private set; }
        public string displayDate { get; private set; }

        public DisplaySchoolDay(SchoolDay d) {
            displayDate = d.day.DayOfWeek + "  ";
            displayDate += d.day.ToString("dd.MM");
            sd = d;
            foreach (var aLesson in d.lessons) {
                displayLessons.Add(new DisplayLesson(aLesson));
            }
            shouldDisplayLessons = sd.lessons.Any((w) => w.name != "");
        }
    }
}