using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YellLandMark.pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageBlankMaster : ContentPage
    {
        public ListView ListView => ListViewMenuItems;

        public PageBlankMaster()
        {
            InitializeComponent();
            BindingContext = new PageBlankMasterViewModel();
        }



        class PageBlankMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<PageBlankMenuItem> MenuItems { get; }
            public PageBlankMasterViewModel()
            {
                MenuItems = new ObservableCollection<PageBlankMenuItem>(new[]
                {
                    new PageBlankMenuItem(typeof(InfomationPage)) { Id = 0, Title = "Your Infomation" },
                    new PageBlankMenuItem(typeof(MainTabPage)) { Id = 1, Title = "Main Page" },
                    new PageBlankMenuItem (typeof(SettingPage)){ Id = 2, Title = "Setting" },
                    new PageBlankMenuItem (typeof(AboutPage)){ Id = 3, Title = "About" },
                    new PageBlankMenuItem (typeof(HelpPage)){ Id = 4, Title = "Help" },
                });
            }
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        }
    }
}
