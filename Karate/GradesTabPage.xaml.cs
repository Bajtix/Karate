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
    public partial class GradesTabPage : ContentPage {
        private App application;
        public GradesTabPage(App application) {
            this.application = application;
            InitializeComponent();
        }
        
        public GradesTabPage() {
            this.application = (App)Application.Current;
            InitializeComponent();
        }
        
        public async Task FetchGrades() {
            LibrusGrades grades = await LibrusGrades.Retrieve(application.librusApi);
            var ss = grades.subjects.Where((w) => w.grades1.Length > 0 || w.grades2.Length > 0);

            List<Subject> subjects = new List<Subject>();
            foreach (var s in ss) {
                subjects.Add(s);

                await Task.Delay(100);
                
                Device.BeginInvokeOnMainThread(() => {
                    BindableLayout.SetItemsSource(SubjectsView, subjects.ToArray());
                });
            }
        }
    }
}