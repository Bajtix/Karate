using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Karate {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToBeImplemented : ContentPage {
        public string Message {
            get => _message;
            set {
                _message = value;
                Msg.Text = _message;
            }
        }

        private string _message;
        
        public ToBeImplemented(string message) {
            InitializeComponent();
            Message = message;
        }
        
        public ToBeImplemented() {
            InitializeComponent();
            Message = "no message set";
        }

        private void GoToAbout(object sender, EventArgs e) {
            //Navigation.PushModalAsync(new Info());
            Navigation.PopModalAsync();
            (Application.Current as App).IndexPage.CurrentPage = (Application.Current as App).IndexPage.Children[4];
        }
        
        private void GoToRoadmap(object sender, EventArgs e) {
            Launcher.OpenAsync("https://github.com/Bajtix/Karate/blob/master/roadmap.md");
        }
    }
}