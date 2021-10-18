using System;
using BrusLib;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Karate {
    public partial class App : Application {
        public LibrusConnection librusApi;
        public IndexPage IndexPage;
        
        public App() {
            InitializeComponent();
            Current.UserAppTheme = OSAppTheme.Light;

            MainPage = new MainPage(this);
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}