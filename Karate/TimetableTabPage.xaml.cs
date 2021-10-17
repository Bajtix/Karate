using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrusLib;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Karate {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimetableTabPage : ContentPage {

        private App application;
        
        public TimetableTabPage() {
            application = Application.Current as App;
            InitializeComponent();
        }
        
        public async Task FetchTT() {
            LibrusTimetable tt = await LibrusTimetable.Retrieve(application.librusApi);
            List<DisplaySchoolDay> displayList = new List<DisplaySchoolDay>();
            tt.week.ForEach((day => displayList.Add(new DisplaySchoolDay(day))));

            await Device.InvokeOnMainThreadAsync(() => {
                BindableLayout.SetItemsSource(SchoolDayDisplay, displayList);
            });

        }
    }
}