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
using YellLandMark.img;

namespace YellLandMark
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfomationPage : ContentPage
    {
        public InfomationPage()
        {
            InitializeComponent();
           
            //BindingContext = new InfomationViewModel();
            loadInformationTopage(new InfomationViewModel().UserInfoItem[0]);
        }
        public InfomationPage(string userNum)
        {
            InitializeComponent();
            //BindingContext = new InfomationViewModel(userNum);
            loadInformationTopage(new InfomationViewModel(userNum).UserInfoItem[0]);
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            //Application.Current.MainPage = StorageStaticObject.pageBlank;
        }

        private void okButton_Clicked(object sender, EventArgs e)
        {

        }
        private void loadInformationTopage(UserInfo userInfo) {
            if (userInfo.icon != null)
            {
                this.circleUserIcon.Source = ImageHelper.ByteArrayToImage(userInfo.icon).Source;
            }
            
            this.userName.Text = userInfo.user_name;
            this.ganderLable.Text = userInfo.gender;
            this.homelandLable.Text=userInfo.homeland;
            this.residenceLable.Text=userInfo.place_of_residence;
            this.emailLable.Text=userInfo.email;
            this.signtureLable.Text=userInfo.signature_of_personality;
            this.homepageLable.Text=userInfo.homepage;
            this.careerLable.Text=userInfo.career;
            this.brithdayLable.Text=userInfo.birthday.ToString();
            
        }
    }


    class InfomationViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<UserInfo> UserInfoItem { get; }
        public InfomationViewModel()
        {
            UserInfoItem = new ObservableCollection<UserInfo>();
            UserInfoItem.Add(new UserInfo() {
                gender="未获取到数据！",
                homeland = "未获取到数据！",
                user_name = "未获取到数据！",
                place_of_residence = "未获取到数据！",
                birthday =DateTime.Now.Date,
                career = "未获取到数据！",
                email = "未获取到数据！",
                homepage = "未获取到数据！",
                signature_of_personality = "未获取到数据！",
            });
            //Inite Item!
        }
        public InfomationViewModel(string num) {
            UserInfoItem = new ObservableCollection<UserInfo>();
            if (StorageStaticObject.newerUserInfomation.ContainsKey(num))
            {
                UserInfo changeUuidString = StorageStaticObject.newerUserInfomation[num];
                foreach (var dic in StorageStaticObject.dictionaryInfoList) {
                    if (dic.uuid.Equals(changeUuidString.career))
                    {
                        changeUuidString.career = dic.value;
                        break;
                    }
                }
                foreach (var dic in StorageStaticObject.dictionaryInfoList)
                {
                    if (dic.uuid.Equals(changeUuidString.homeland))
                    {
                        changeUuidString.homeland = dic.value;
                        break;
                    }
                }
                foreach (var dic in StorageStaticObject.dictionaryInfoList)
                {
                    if (dic.uuid.Equals(changeUuidString.place_of_residence))
                    {
                        changeUuidString.place_of_residence = dic.value;
                        break;
                    }
                }
                foreach (var dic in StorageStaticObject.dictionaryInfoList)
                {
                    if (dic.uuid.Equals(changeUuidString.gender))
                    {
                        changeUuidString.gender = dic.value;
                        break;
                    }
                }
                

                UserInfoItem.Add(changeUuidString);
            }
            else {
                UserInfoItem.Add(new UserInfo()
                {
                    gender = "未获取到数据！",
                    homeland = "未获取到数据！",
                    user_name = "未获取到数据！",
                    place_of_residence = "未获取到数据！",
                    birthday = DateTime.Now.Date,
                    career = "未获取到数据！",
                    email = "未获取到数据！",
                    homepage = "未获取到数据！",
                    signature_of_personality = "未获取到数据！",
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


    }
}
