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
    public partial class ViewCalendarPage : LibPage {

        public List<CalendarEvent> Events { get; }
        public string Day { get; }

        private App application;
        
        public ViewCalendarPage() {
            InitializeComponent();
        }
        
        public ViewCalendarPage(DateTime day, List<CalendarEvent> events) {
            InitializeComponent();
            this.Day = day.ToString("dddd, dd.MM");
            this.Events = events;
            Date.Text = Day;
            ShowLoader();
            application = Application.Current as App;
            
            
            Task.Run(Load);
        }

        private async Task Load() {
            
            foreach (var i in Events) {
                await i.FetchInfo(application.librusApi);
            }



            await Device.InvokeOnMainThreadAsync(() => {
                Items.ItemsSource = Events;
                HideLoader();
            });
        }
    }
}