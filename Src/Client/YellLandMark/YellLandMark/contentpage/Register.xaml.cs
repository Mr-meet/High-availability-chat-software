using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YellLandMark.img;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YellLandMark
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        private CommandEntity commandEntity = new CommandEntity();
        private string commandUuid = null;
        private UserLoginInfo userLogin;
        private UserInfo userInfo;
        private byte[] userImage;
        private byte[] iconImage;
        private int alarm; 
        public Register()
        {
            InitializeComponent();

            foreach (var dic in StorageStaticObject.dictionaryInfoList) {
                if (dic.useage == "place") homelandPicker.Items.Add(dic.value);
                if (dic.useage == "career") careerPicker.Items.Add(dic.value);
                if(dic.useage== "place") placePicker.Items.Add(dic.value);
                if (dic.useage == "gender") genderPicker.Items.Add(dic.value);
            }
            homelandPicker.SelectedIndex = 0;
            careerPicker.SelectedIndex = 0;
            placePicker.SelectedIndex = 0;
            genderPicker.SelectedIndex = 0;
            imageButton.Clicked += async (sender, e) =>
            {
                imageButton.IsEnabled = false;
                Stream stream = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();

                if (stream != null)
                {
                    Image image = new Image
                    {
                        Source = ImageSource.FromStream(() => stream),
                        BackgroundColor = Color.Gray
                    };
                    userImage = ImageHelper.ReadFully(stream);
                    /*
                     //手势点击识别
                    TapGestureRecognizer recognizer = new TapGestureRecognizer();
                    recognizer.Tapped += (sender2, args) =>
                    {
                        //(MainPage as ContentPage).Content = stack;
                        //this.Content = new StackLayout();
                        //imageButton.IsEnabled = true;
                    };
                    image.GestureRecognizers.Add(recognizer);
                    this.Content = image;*/
                }
                else
                {
                    imageButton.IsEnabled = true;
                }
            };

            iconButton.Clicked += async (sender, e) =>
            {
                iconButton.IsEnabled = false;
                Stream stream = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();

                if (stream != null)
                {
                    Image image = new Image
                    {
                        Source = ImageSource.FromStream(() => stream),
                        BackgroundColor = Color.Gray
                    };
                    iconImage = ImageHelper.ReadFully( stream);
                }
                else
                {
                    iconButton.IsEnabled = true;
                }
            };

        }

        private void submitButton_Clicked(object sender, EventArgs e)
        {
            //Navigation.PopAsync();
            getUserInfoFromPage();
            if (this.alarm > 0)
            {
                DisplayAlert("warnning", "place check your in put!", "ok");
            }
            else
            {
                commandUuid = Guid.NewGuid().ToString();

                commandEntity.commandType = ICommandForMessage.Registe;
                commandEntity.userInfo = userInfo;
                commandEntity.userLoginInfo = userLogin;
                commandEntity.commandUuid = commandUuid;
                commandEntity.userId = "未登录用户";

                var task = Task.Run(async () => { await SocketHelper.sendCommandEntity(commandEntity); });
                task.Wait();
                UserLoginInfo myInfo=SocketHelper.registeBack(commandUuid);
                if (myInfo.user_num != null)
                {
                    DisplayAlert("Good!", "remember your YellLandMark account is:"+myInfo.user_num, "I already get it!");
                    if (StorageStaticObject.loadingPage == null) { StorageStaticObject.loadingPage = new Loading(); }
                    Application.Current.MainPage = StorageStaticObject.loadingPage;
                }
                else {
                    DisplayAlert("warnning", "sorry you registe filed!", "ok");
                }
                
            }
            
        }

        private void cancelButton_Clicked(object sender, EventArgs e)
        {
            if (StorageStaticObject.loadingPage == null) { StorageStaticObject.loadingPage = new Loading(); }
            Application.Current.MainPage = StorageStaticObject.loadingPage;
        }

        private void liscenseSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (liscenseSwitch.IsToggled) submitButton.IsEnabled = true;
            else submitButton.IsEnabled = false;
        }

        private void getUserInfoFromPage() {
            if (this.userInfo == null) this.userInfo = new UserInfo();
            if (this.userLogin == null) this.userLogin = new UserLoginInfo();
            this.alarm = 0;
            //下方是校验
            if (nickName.Text.Trim().Length > 20 || nickName.Text.Trim().Length < 1) this.alarm++;
            if (password.Text.Length < 6 || password.Text.Length > 12) this.alarm++;
            if (!password.Text.Equals(checkPassword.Text)) this.alarm++;
            string emailRule = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            Regex regex = new Regex(emailRule);
            if (!regex.IsMatch(email.Text)) this.alarm++;
            if (homepage.Text!=null&&homepage.Text.Length > 120) this.alarm++;
            if (signature.Text != null && signature.Text.Length > 255) this.alarm++;


            userInfo.user_name = nickName.Text.Trim();
            userInfo.email = email.Text;
            userInfo.birthday = birthday.Date;
            userInfo.homeland = findUuidByValue((string)homelandPicker.SelectedItem);
            userInfo.career = findUuidByValue((string)careerPicker.SelectedItem);
            userInfo.place_of_residence = findUuidByValue((string)placePicker.SelectedItem);
            userInfo.gender = findUuidByValue((string)genderPicker.SelectedItem);
            userInfo.icon = iconImage;
            userInfo.image = userImage;
            userInfo.homepage = homepage.Text;
            userInfo.signature_of_personality = signature.Text;
            userLogin.password = password.Text;

        }

        private string findUuidByValue(string value) {
            foreach (var dic in StorageStaticObject.dictionaryInfoList) {
                if (dic.value.Equals(value)) return dic.uuid;
            }
            return null;
        }
       

    }
}
