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
    public partial class GradesTabPage : LibPage {
        private App application;
        private bool semester = false;
        public GradesTabPage(App application) {
            this.application = application;
            InitializeComponent();
        }
        
        public GradesTabPage() {
            this.application = (App)Application.Current;
            InitializeComponent();
            
            
        }

        public override void OnFirstLoad() {
            Task.Run(() => FetchGrades());
        }

        public async Task FetchGrades(bool autoSem = true) {
            LibrusGrades grades = await LibrusGrades.Retrieve(application.librusApi);
            
            if(autoSem) semester = GuessSemester(grades);

            var notEmptySubjects = grades.subjects.Where((w) => (!semester && w.grades1.Length > 0) || (semester && w.grades2.Length > 0)); // this statement got a bit complex, but basically we just check the selected semester
            Device.BeginInvokeOnMainThread(ShowLoader);
            List<DisplaySubject> subjects = new List<DisplaySubject>();
            foreach (var s in notEmptySubjects) {
                subjects.Add(new DisplaySubject(s.name, semester ? s.grades2 : s.grades1));

                await Task.Delay(100);
                
                Device.BeginInvokeOnMainThread(() => {
                    BindableLayout.SetItemsSource(SubjectsView, subjects.ToArray());
                    
                });
            }
            ChangeSemesterButton.Text = $"Zmień na semestr {(semester ? "1" : "2")}";
            
            Device.BeginInvokeOnMainThread(HideLoader);
        }

        private bool GuessSemester(LibrusGrades grades) {
            var subs = grades.subjects.FirstOrDefault(s => s.grades2.Length > 0);
            return subs != default(Subject);
        }

        private void SemesterChange(object sender, EventArgs e) {
            semester = !semester;
            Task.Run(() => FetchGrades(false));
        }
    }
}