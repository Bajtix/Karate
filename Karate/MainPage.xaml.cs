using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;
using BrusLib;
using Xamarin.Forms.PlatformConfiguration;

namespace Karate {
    public partial class MainPage : ContentPage {
        private App application;

        private void LoginActionHandler(object s, LibrusAuth.AuthEvent e) {
            Device.BeginInvokeOnMainThread(()=>{
                LoadingMessage.Text = e.message;
                if(e.e != null)
                    ErrorMessage.Text = $"{e.message} : {IdentifyException(e.e)}";
            });
            
        }
        
        
        public MainPage(App application) {
            InitializeComponent();
            if(File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "last.login.key"))) {
                string t = File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "last.login.key"));
                Username.Text = t.Split(';')[0];
                Password.Text = t.Split(';')[1];
            }
            
            Version.Text = Xamarin.Essentials.VersionTracking.CurrentVersion;
            this.application = application;
        }

        private async void StartLogin(object sender, EventArgs e) {
            Loading.IsVisible = true;
            LoginForm.IsVisible = false;
            LoadingMessage.Text = "Wczytywanie serwisu BrusLib";
            LoadingSpinner.RotateTo(3600, 10000).GetAwaiter();
            await Task.Run(async ()=> await Auth(Username.Text,Password.Text));


        }

        private async Task Auth(string u, string p) {
            application.librusApi = await LibrusAuth.Authenticate(u, p, LoginActionHandler);

            if (!application.librusApi.successful) {
                Loading.IsVisible = false;
                LoginForm.IsVisible = true;
                
                return;
            }
            
            await Task.Delay(50);
            
            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "last.login.key"), $"{Username.Text};{Password.Text}");
            
            await Device.InvokeOnMainThreadAsync(()=>Navigation.PushModalAsync(new IndexPage(application)));
            
        }

        private static string IdentifyException(Exception e) {
            if (e == null) return "";
            if (e.GetType() == typeof(WebException)) {
                var w = e as WebException;
                switch ((w.Response as HttpWebResponse).StatusCode) {
                    case HttpStatusCode.Forbidden:
                        return "403: Błędne dane logowania?";
                    case HttpStatusCode.NotFound:
                        return "404: Błędny adres URL";
                }
            }

            return $"{e.Message}";
        }
    }
}