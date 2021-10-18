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
    public partial class IndexPage : TabbedPage {
        private App application;
       
        public List<Subject> SubjectListSource { get; set;  }
        
        
        public IndexPage(App application) {
            this.application = application;
            application.IndexPage = this;
            
            InitializeComponent();
            
            application.MainPage = this;
        }
        


        protected override bool OnBackButtonPressed() {
            return true;
        }

        private void IndexPage_OnCurrentPageChanged(object sender, EventArgs e) {
            if (CurrentPage is LibPage) {
                (CurrentPage as LibPage).OnLoad();
            }
        }
    }
}