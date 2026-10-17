namespace StockScreener
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        //Rev 00.01.01 -SS- Adding notification test
        /// <summary>
        /// Test Button Clicked - test notification on Android 13+ (API 33+)
        /// </summary>
        private async void OnTestButtonClicked(object sender, EventArgs e)
        {
            await Task.Delay(5000);
            //Checks android
#if ANDROID
            var status = await Permissions.RequestAsync<Permissions.PostNotifications>(); //make user grant notification permissions
            if (status != PermissionStatus.Granted)
                return;

            var context = Platform.AppContext;
                
            if (OperatingSystem.IsAndroidVersionAtLeast(26))
            {
                var channel = new Android.App.NotificationChannel("main", "General", Android.App.NotificationImportance.Default);
                var manager = (Android.App.NotificationManager)context.GetSystemService(Android.Content.Context.NotificationService)!;
                manager.CreateNotificationChannel(channel);

                var builder = new AndroidX.Core.App.NotificationCompat.Builder(context, "main");
                builder.SetContentTitle("Test Notification");
                builder.SetContentText("This is a test notification.");
                builder.SetSmallIcon(Resource.Mipmap.appicon);
                builder.SetPriority(AndroidX.Core.App.NotificationCompat.PriorityHigh); //High Priority forces banner notifications
                var notification = builder.Build();

                AndroidX.Core.App.NotificationManagerCompat.From(context)!.Notify(1, notification);
            }
#endif

        }
    }
}
