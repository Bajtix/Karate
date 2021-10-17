using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Karate {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Info : ContentPage {
        public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
        public string version { get;}
        public string motd { get;}
        
        public Info() {
            InitializeComponent();
            version = VersionTracking.CurrentVersion;
            BindingContext = this;
            
        }
    }
}