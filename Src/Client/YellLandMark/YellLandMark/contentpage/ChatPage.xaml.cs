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
    public partial class ChatPage : ContentPage
    {
        static string targetID;

        static List<UserMessage> userMessageList=new List<UserMessage>();
        static List<UserMessageForRecord> userMessageRecord = new List<UserMessageForRecord>();
        static UserMessageForRecord oneUserMessageRecord = new UserMessageForRecord();
        ChatPageViewModel chatModel;
        public ChatPage(string targetName,string targetid)
        {
            InitializeComponent();
            SocketHelper.IsStopReadMessage = false;
            targetID = targetid;
            titleLable.Text = targetName;
            userMessageList.Clear();
            getRecord();
            Task.Factory.StartNew(()=> SocketHelper.readMessage()) ;
            
            chatModel = new ChatPageViewModel(userMessageRecord, targetName);
            BindingContext = chatModel;
            timer();
        }

        private void getRecord() {
            string uuid = Guid.NewGuid().ToString();
            CommandEntity comm = new CommandEntity()
            {
                commandUuid =uuid ,
                commandType = ICommandForMessage.GetMessageRecord,
                targetString = targetID,
                userId = StorageStaticObject.userNumber,
                
            };
            var task = Task.Run(async () => { await SocketHelper.sendCommandEntity(comm); });
            task.Wait();

            userMessageRecord= SocketHelper.getRecordMessage(uuid);
        }

        private void timer() {
            Device.StartTimer(new TimeSpan(0, 0,1), () => {
                // do something every 1 seconds  

                userMessageRecord.Clear();
                foreach (var c in StorageStaticObject.commandQueue.Values)
                {
                    if (c.userId.Equals(targetID))
                    {
                        userMessageList.Add(c.userMessage);
                    }
                }
                foreach (var message in userMessageList)
                {
                    userMessageRecord.Add(new UserMessageForRecord
                    {
                        message = message.content,
                        messageSendTime = message.sendTime.ToString(),
                    });
                }

                userMessageList.Clear();
                if (StorageStaticObject.LocalMessageQueue.ContainsKey(targetID))
                {
                    foreach (var c in StorageStaticObject.LocalMessageQueue[targetID])
                    {
                        if (c.senderId.Equals(targetID))
                        {
                            userMessageList.Add(c);
                        }
                    }
                    StorageStaticObject.LocalMessageQueue.Remove(targetID);
                    foreach (var message in userMessageList)
                    {
                        userMessageRecord.Add(new UserMessageForRecord
                        {
                            message = message.content,
                            messageSendTime = message.sendTime.ToString(),
                        });
                    }

                    userMessageList.Clear();
                }




                foreach (var r in userMessageRecord)
                {
                    chatModel.Items.Add(new ChatPageViewModel.Item()
                    {
                        Text = r.message,
                        Detail = titleLable.Text + r.messageSendTime
                    });
                }
                userMessageRecord.Clear();

                //DisplayAlert("", DateTime.Now.ToString("mm:ss") + " past the hour", "OK");

                Device.BeginInvokeOnMainThread(() => {
                    // interact with UI elements  
                    

                });


                return true; // runs again, or false to stop  
            });
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            SocketHelper.IsStopReadMessage = true;
            Application.Current.MainPage = StorageStaticObject.pageBlank;
            
        }

        private  void sendButton_Clicked(object sender, EventArgs e)
        {
            oneUserMessageRecord.message = sendEntry.Text;
            oneUserMessageRecord.messageSendTime = DateTime.Now.ToString();

            UserMessage u = new UserMessage() {
                content = sendEntry.Text,
                senderId=StorageStaticObject.userNumber,
                sendTime=DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.SSSZ"),
                resiverId=targetID,
                id=Guid.NewGuid().ToString(),
                contentChecked=false,
                platform=1
                
            };
            CommandEntity comm = new CommandEntity() {
                commandUuid= Guid.NewGuid().ToString(),
                commandType=ICommandForMessage.SendMessage,
                targetString=targetID,
                userId= StorageStaticObject.userNumber,
                userMessage=u
            };

            var task = Task.Run(async () => { await SocketHelper.sendCommandEntity(comm); });
            task.Wait(); 

            chatModel.Items.Add(new ChatPageViewModel.Item()
            {
                Text = oneUserMessageRecord.message,
                Detail = "你" + oneUserMessageRecord.messageSendTime
            });

            sendEntry.Text = "";
        }
    }



    class ChatPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Item> Items { get; set; }

        public ChatPageViewModel(List<UserMessageForRecord> record,string name)
        {
            Items = new ObservableCollection<Item>();
            foreach ( var r in record)
            {
                Items.Add(new Item()
                {
                    Text = r.message,
                    Detail = r.senderId + r.messageSendTime
                });
            }


          

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
            
            public override string ToString() => Text;
        }

        
    }




}
