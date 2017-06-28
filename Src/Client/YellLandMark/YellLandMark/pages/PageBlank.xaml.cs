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
    public partial class PageBlank : MasterDetailPage
    {
        public PageBlank()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as PageBlankMenuItem;
            if (item == null)
                return;

            if (item.Id == 0)
            {
                Page infoMationPage = new InfomationPage(StorageStaticObject.userNumber);
                Detail = new NavigationPage(infoMationPage);
                MasterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
            else {
                var page = (Page)Activator.CreateInstance(item.TargetType);
                page.Title = item.Title;
                Detail = new NavigationPage(page);
                MasterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
            

        }

    }

}
