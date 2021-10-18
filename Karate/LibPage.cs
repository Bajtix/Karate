using Xamarin.Forms;

namespace Karate {
    public class LibPage : ContentPage {
        protected bool isLoaded = false;

        protected Image Loader;

        private View backup;
        
        public virtual void OnLoad() {
            if(!isLoaded)
                OnFirstLoad();
            isLoaded = true;
        }

        protected void ShowLoader() {
            backup = Content;
            Loader = new Image() {HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("loading.png"), HeightRequest = 200};
            
            Content = Loader;
            
            Loader.RotateTo(3600, 10);
        }

        protected void HideLoader() {
            Content = backup;
            
        }

        public virtual void OnFirstLoad() {
           
        }
    }
}