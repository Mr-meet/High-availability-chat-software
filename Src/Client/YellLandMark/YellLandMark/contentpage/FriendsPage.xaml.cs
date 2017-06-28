using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YellLandMark
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendsPage : ContentPage
    {
        public FriendsPage()
        {
            InitializeComponent();
            BindingContext = new FriendsPageViewModel();
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            string tarID = null;
            foreach (var f in StorageStaticObject.localFriendsList) {
                if (f.remark.Equals(e.SelectedItem.ToString())) {
                    tarID = f.id;
                    break;
                }
            }
             //await DisplayAlert("Selected", e.SelectedItem.ToString()+ tarID, "OK");
            Application.Current.MainPage = new ChatPage(e.SelectedItem.ToString(), tarID);
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

         void addFriendButton_Clicked(object sender, EventArgs e)
        {
            string uuid = Guid.NewGuid().ToString();
            CommandEntity comm = new CommandEntity() {
                targetString= headEntry.Text,
                commandType=ICommandForMessage.AddFriend,
                userId=StorageStaticObject.userNumber,
                commandUuid=uuid,
            };

            var task = Task.Run(async () => { await SocketHelper.sendCommandEntity(comm); });
            task.Wait();

            if (SocketHelper.opertionIsSuccess(uuid))
            {
                DisplayAlert("Good", "Add friend success", "OK");
            }
            else {
                DisplayAlert("Warnning", "Add friend field", "OK");
            }


        }

         void findFriendsButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Selected", "66666666", "OK");
        }

         void addGroupButton_Clicked(object sender, EventArgs e) {
            DisplayAlert("Selected", "66666666", "OK");
        }

        //private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{

        //}
    }



    class FriendsPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<Grouping<string, Item>> ItemsGrouped { get; set; }

        public FriendsPageViewModel()
        {
            Items = new ObservableCollection<Item>();
            foreach (Friend friend in StorageStaticObject.localFriendsList) {
                Items.Add(new Item() {
                    Text = friend.remark,
                    groupName = StorageStaticObject.localGroupMap[friend.group].groupName,
                    Detail=friend.id
                });
            }
            

            var sortedGroup = from item in Items
                         orderby item.groupName
                         group item by item.groupName into itemGroup
                         select new Grouping<string, Item>(itemGroup.Key, itemGroup);

            ItemsGrouped = new ObservableCollection<Grouping<string, Item>>(sortedGroup);

            RefreshDataCommand = new Command(
                async () => await RefreshData());
        }

        public ICommand RefreshDataCommand { get; }

        async Task RefreshData()
        {
            IsBusy = true;
            //Load Data Here
            await Task.Delay(2000);

            IsBusy = false;
        }

        bool busy;
        public bool IsBusy
        {
            get { return busy; }
            set
            {
                busy = value;
                OnPropertyChanged();
                ((Command)RefreshDataCommand).ChangeCanExecute();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public class Item
        {
            public Image UserIcon { get; set; }
            public string Text { get; set; }
            public string Detail { get; set; }
            public string groupName { get; set; }
            public override string ToString() => Text;
        }

        public class Grouping<K, T> : ObservableCollection<T>
        {
            public K Key { get; private set; }

            public Grouping(K key, IEnumerable<T> items)
            {
                Key = key;
                foreach (var item in items)
                    this.Items.Add(item);
            }
        }
    }
}
