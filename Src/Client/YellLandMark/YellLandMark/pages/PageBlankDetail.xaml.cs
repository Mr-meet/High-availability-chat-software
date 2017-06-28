using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YellLandMark.pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageBlankDetail : ContentPage
    {
        public PageBlankDetail()
        {
            InitializeComponent();
            /*NavigationPage.SetBackButtonTitle(this, string.Empty);
            NavigationPage.SetHasNavigationBar(this, true);
            NavigationPage.SetHasBackButton(this, true);
            NavigationPage.SetBackButtonTitle(this, "I wanna back");*/
        }
    }
}
