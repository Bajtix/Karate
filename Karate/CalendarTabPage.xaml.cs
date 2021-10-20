using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrusLib;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Karate {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarTabPage : LibPage {

        public App application;
        
        public CalendarTabPage() {
            application = Application.Current as App;
        
            InitializeComponent();
        }

        public override void OnFirstLoad() {
            ShowLoader();
            Task.Run(Fetch);
        }

        public async Task Fetch() {
            LibrusCalendar calendar = await LibrusCalendar.Retrieve(application.librusApi, APIBufferMode.none);
            //Dictionary<DateTime, List<CalendarEvent>> eventLists = new Dictionary<DateTime, List<CalendarEvent>>();
            List<DisplayCalendarDay> calendarDays = new List<DisplayCalendarDay>();
            foreach (var w in calendar.events) {
               calendarDays.Add(new DisplayCalendarDay(w.Value, w.Key));
            }



            await Device.InvokeOnMainThreadAsync(() => {
                BindableLayout.SetItemsSource(WeekDisplay, calendarDays);
                HideLoader();
            });
        }

        private void Button_OnClicked(object sender, EventArgs e) {
            OnFirstLoad();
        }
    }
}