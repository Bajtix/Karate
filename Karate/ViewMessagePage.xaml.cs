using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrusLib;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Karate {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewMessagePage : ContentPage, INotifyPropertyChanged {

        public LibrusMessage msg { get; set; }

        public string Content {
            get => content;
            set {
                OnPropertyChanged(nameof(Content));
                content = value;
            }
        }

        public string Author { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }

        private LibrusConnection connection;
        private string content;

        public ViewMessagePage() {
            InitializeComponent();
        }
        
        public ViewMessagePage(LibrusMessage msg) {
            InitializeComponent();
            connection = (Application.Current as App).librusApi;
            this.msg = msg;
            Load();
            Title = msg.Title;
            Date = msg.ReceiveDate.ToString("dd.MM.yyyy HH:mm");
            Author = msg.Author;
            
            BindingContext = this;
        }

        private async void Load() {
            await msg.ReceiveContent(connection, APIBufferMode.none);

            await Device.InvokeOnMainThreadAsync(() => {
                ContentLabel.Text = msg.Content;
                Content = msg.Content;
            });
        }
    }
}