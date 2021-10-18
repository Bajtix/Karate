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
    public partial class MessagesTabPage : ContentPage {
        private App application;
        
        public MessagesTabPage() {
            application = Application.Current as App;
            InitializeComponent();

            Task.Run(FetchMessages);
        }
        
        public async Task FetchMessages() {
            LibrusMessageReceiver receiver = await LibrusMessageReceiver.Retrieve(application.librusApi);
            var messages = receiver.messages;
            Device.InvokeOnMainThreadAsync(()=> {
                //BindableLayout.SetItemsSource(MessageDisplay, messages);
                MessageDisplay.ItemsSource = messages;
            });
        }

        private void MessageDisplay_OnItemTapped(object sender, ItemTappedEventArgs e) {
            Navigation.PushModalAsync(new ViewMessagePage((LibrusMessage)e.Item));
        }
    }
}