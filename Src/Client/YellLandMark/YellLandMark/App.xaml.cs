using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Sockets.Plugin;
using Xamarin.Forms;

namespace YellLandMark
{
    public static class Global
    {
        public static CommsInterface DefaultCommsInterface { get; set; }
    }

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (StorageStaticObject.loadingPage==null) { StorageStaticObject.loadingPage = new Loading(); }
            MainPage = StorageStaticObject.loadingPage;
           // MainPage = new InfomationPage();
        }

        protected override void OnStart()
        {
            //var task = Task.Run(async () => { await SocketHelper.connectToService(); });
            Task.Factory.StartNew(async () => { await SocketHelper.connectToService(); });
            //task.Wait();
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
