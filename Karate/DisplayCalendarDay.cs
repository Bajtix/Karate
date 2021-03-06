using System;
using System.Collections.Generic;
using System.Windows.Input;
using BrusLib;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Karate {
    public class DisplayCalendarDay {
        private ContentPage creator;
        public DisplayCalendarDay(List<CalendarEvent> events, DateTime day, ContentPage creator) {
            Events = events;
            DayIndex = day.Day;
            this.creator = creator;
            Day = day;
            
            int dayOfWeek = GetDayNumFromDoW(new DateTime(day.Year, day.Month, 1).DayOfWeek);
            
            
            int i = DayIndex + dayOfWeek - 1;
            int x = i % 7;
            int y = (i - x) / 7;

            WeekGridX = x;
            WeekGridY = y;
        }

        private int GetDayNumFromDoW(DayOfWeek dayOfWeek) {
            switch (dayOfWeek) { //TODO: można to zrobić za pomocą prostej matmy. załamujesz mnie, baja.
                case DayOfWeek.Monday:
                    return 0;
                case DayOfWeek.Tuesday:
                    return 1;
                case DayOfWeek.Wednesday:
                    return 2;
                case DayOfWeek.Thursday:
                    return 3;
                case DayOfWeek.Friday:
                    return 4;
                case DayOfWeek.Saturday:
                    return 5;
                case DayOfWeek.Sunday:
                    return 6;
            }

            return 0;
        }

        public List<CalendarEvent> Events { get; }
        public int DayIndex { get; }
        public DateTime Day { get; }
        public int WeekGridX { get; }
        public int WeekGridY { get; }
        
        public ICommand TapCommand => new Command(() => {
            Device.BeginInvokeOnMainThread(() => {
                var page = new ViewCalendarPage(Day, Events);
                creator.Navigation.PushModalAsync(page);
            });
        });
    }
}