using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YellLandMark.pages;


namespace YellLandMark
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Loading : ContentPage
    {
        private  UserLoginInfo userLogin = new UserLoginInfo();
        private  CommandEntity commandEntity = new CommandEntity();
        private  string commandUuid=null;
        //public  string ConnString = "Database='YellLandMark';Data Source='139.199.35.140:3306';User Id='root';Password='fj950602';charset='utf8';pooling=true";
        public Loading()
        {
            InitializeComponent();
            
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            //this.activityindicator.IsRunning = true;
            commandUuid = Guid.NewGuid().ToString();
            userLogin.user_num = accountEntry.Text.ToString().Trim();
            userLogin.password = passwordEntry.Text.ToString().Trim();
            commandEntity.commandType = ICommandForMessage.Login;
            commandEntity.userLoginInfo = userLogin;
            commandEntity.commandUuid = commandUuid;
            commandEntity.userId = "未登录用户";
            
            var task = Task.Run(async () => { await SocketHelper.sendCommandEntity(commandEntity); });
            task.Wait();
            if (SocketHelper.loginOpertion(commandUuid))
            {
                StorageStaticObject.userNumber = userLogin.user_num;
                task = Task.Run(async () => { await SocketHelper.sayHello(StorageStaticObject.userNumber); });
                task.Wait();
                if (StorageStaticObject.pageBlank == null) {
                    StorageStaticObject.pageBlank= new PageBlank();
                }
                Application.Current.MainPage = StorageStaticObject.pageBlank;
            }
            else
            {
                DisplayAlert("Warnning","login failed ! place check your account and password","OK");
            }
            //this.activityindicator.IsRunning = false;
        }

        private void Register_Clicked(object sender, EventArgs e)
        {
            //this.activityindicator.IsRunning = true;
            commandUuid = Guid.NewGuid().ToString();
            commandEntity.commandType = ICommandForMessage.LoadDictionary;
            commandEntity.commandUuid = commandUuid;
            commandEntity.userId = "未登录用户";
           
            var task = Task.Run(async () => { await SocketHelper.sendCommandEntity(commandEntity); });
            task.Wait();
            SocketHelper.saveDictionaryList(commandUuid);
            if (StorageStaticObject.dictionaryInfoList.Count > 0) {
                //DisplayAlert("GOOD","we get that!","ok");
            }
            if (StorageStaticObject.registerPage == null) {
                
                StorageStaticObject.registerPage = new Register();
            }
            Application.Current.MainPage = StorageStaticObject.registerPage;
            //Navigation.PushAsync(new Register());
           // this.activityindicator.IsRunning = false;
        }

       

        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="usercontrol"></param>
        //public static void RedirectTo(UserControl usercontrol)
        //{
        //    App app = (App)Application.Current;
        //    app..Children.Clear();
        //    app.rootGrid.Children.Add(usercontrol);
        //}
    }
}
