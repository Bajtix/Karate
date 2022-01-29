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
    public partial class MessagesTabPage : LibPage {
        private App application;
        
        public MessagesTabPage() {
            application = Application.Current as App;
            InitializeComponent();

            
        }

        public override void OnFirstLoad() {
            ShowLoader();
            Task.Run(FetchMessages);
        }

        public async Task FetchMessages() {
            LibrusMessageReceiver receiver = await LibrusMessageReceiver.Retrieve(application.librusApi);
            var messages = receiver.messages;
            Device.InvokeOnMainThreadAsync(()=> {
                //BindableLayout.SetItemsSource(MessageDisplay, messages);
                MessageDisplay.ItemsSource = messages;
                HideLoader();
            });
        }

        private void MessageDisplay_OnItemTapped(object sender, ItemTappedEventArgs e) {
            Navigation.PushModalAsync(new ViewMessagePage((LibrusMessage)e.Item));
        }

        private void CreateMessage(object sender, EventArgs e) {
            Navigation.PushModalAsync(new ToBeImplemented("Pisanie wiadomości nadejdzie w przyszłości"));
        }

        private void ChangeMessageBox(object sender, FocusEventArgs e) {
            Navigation.PushModalAsync(new ToBeImplemented("Funkcje zmiany źródła wiadomości już wkrótce!"));
        }
    }
}