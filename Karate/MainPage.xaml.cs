using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;
using BrusLib;
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration;

namespace Karate {
    public partial class MainPage : ContentPage {
        private App application;

        private void LoginActionHandler(object s, LibrusAuth.AuthEvent e) {
            Device.BeginInvokeOnMainThread(()=>{
                LoadingMessage.Text = e.message;
                if (e.e != null) {
                    ErrorMessage.Text = $"{e.message} : {IdentifyException(e.e)}";
                    DisplayAlert("na jowisza po trzykroć kur*a", e.message + "\n" + IdentifyException(e.e), "rydwan mi się spier**lił");
                }
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
            
            await Permissions.RequestAsync<Permissions.StorageWrite>();
            await Permissions.RequestAsync<Permissions.StorageRead>();
            
            
            await Device.InvokeOnMainThreadAsync(() => {
                Loading.IsVisible = true;
                LoginForm.IsVisible = false;
            });
            
            LoadingMessage.Text = "Wczytywanie serwisu BrusLib";
            //LoadingSpinner.RotateTo(3600, 10000).GetAwaiter();
            try {
                await Task.Run(async () => await Auth(Username.Text, Password.Text));
            }
            catch (Exception exception) {
                await DisplayAlert("na jowisza po trzykroć kurwa", exception.Message, "rydwan mi się spierdolił");
            }

        }

        private async Task Auth(string u, string p) {
            LibrusAuth.delayMultiplier = 0.2f;
            application.librusApi = await LibrusAuth.Authenticate(u, p, LoginActionHandler);

            if (!application.librusApi.successful) {
                await Device.InvokeOnMainThreadAsync(() => {
                    Loading.IsVisible = false;
                    LoginForm.IsVisible = true;
                });
                
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
                if (w.Response == null) return "Brak odpowiedzi serwera";
                switch ((w.Response as HttpWebResponse).StatusCode) {
                    case HttpStatusCode.Forbidden:
                        return "403: Błędne dane logowania";
                    case HttpStatusCode.NotFound:
                        return "404: Błędny adres URL";
                }
            }

            return $"{e.Message}";
        }
    }
}