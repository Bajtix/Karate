using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrusLib;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Karate {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimetableTabPage : LibPage {

        private App application;

        public List<string> Weeks { get; set; }
        
        public TimetableTabPage() {
            application = Application.Current as App;
            InitializeComponent();

            
        }

        public override void OnFirstLoad() {
            ShowLoader();
            GetWeekList();
            Task.Run(()=>FetchTT());
        }

        private void GetWeekList() {
            Weeks = new List<string>();
            int weekCount = 3;
            DateTime start = WeekFirst(DateTime.Now).AddDays(weekCount * -7);
            DateTime end = WeekLast(DateTime.Now).AddDays(weekCount * -7);
            for (int i = 0; i < weekCount*2; i++) {
                Weeks.Add($"{start:yyyy-MM-dd}_{end:yyyy-MM-dd}");
                start = start.AddDays(7);
                end = end.AddDays(7);
            }

            this.BindingContext = this;
        }

        private DateTime WeekFirst(DateTime e) {
            return Util.GetFirstDayOfWeek(e, CultureInfo.InvariantCulture).AddDays(1);
        }
        
        private DateTime WeekLast(DateTime e) {
            return Util.GetFirstDayOfWeek(e, CultureInfo.InvariantCulture).AddDays(7);
        }


        private async Task FetchTT(string week = "") {
            
            LibrusTimetable tt = await LibrusTimetable.Retrieve(application.librusApi, APIBufferMode.none, week);
            List<DisplaySchoolDay> displayList = new List<DisplaySchoolDay>();
            tt.week.ForEach((day => displayList.Add(new DisplaySchoolDay(day))));

            await Device.InvokeOnMainThreadAsync(() => {
                BindableLayout.SetItemsSource(SchoolDayDisplay, displayList);
                HideLoader();
            });

        }


        private void ChangeWeek(object sender, EventArgs e) {
            
            if(WeekPicker.SelectedIndex < 0) return;
            ShowLoader();
            
            string selectedWeek = Weeks[WeekPicker.SelectedIndex];
            Task.Run(async () => {
                    await Task.Delay(100);
                    await FetchTT(selectedWeek);
                }
            );
        }
    }
}