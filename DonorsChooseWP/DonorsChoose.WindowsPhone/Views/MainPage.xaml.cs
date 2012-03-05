using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using DonorsChoose.WindowsPhone.ViewModels;

// Added to test push notifications
using Microsoft.Phone.Notification;
using System.Text;

namespace DonorsChoose.WindowsPhone.Views
{
    public partial class MainPage : PhoneApplicationPage
    {
        private MainViewModel ViewModel
        {
            get { return DataContext as MainViewModel; }
            set
            {
                DataContext = value;
            }
        }


        public MainPage()
        {
            // Holds the push channel that is created or found
            HttpNotificationChannel pushChannel;

            // The name of push channel
            string channelName = "ToastSampleChannel2";


            InitializeComponent();

            ViewModel = ViewModelLocator.Main;

            // Quick test
            //ViewModel.LoadProjectList();

            // Try to find the push channel
            pushChannel = HttpNotificationChannel.Find(channelName);

            // If the channel was no found, then create 
            // a new connecton to the push service
            if (pushChannel == null)
            {
                pushChannel = new HttpNotificationChannel(channelName);

                // Register for all the events before attempting to open the channel
                pushChannel.ChannelUriUpdated +=
                    new EventHandler<NotificationChannelUriEventArgs>(pushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred +=
                    new EventHandler<NotificationChannelErrorEventArgs>(pushChannel_ErrorOccurred);

                // Register for this notification only if you need to receive 
                // the notificiation while your application is running
                pushChannel.ShellToastNotificationReceived +=
                    new EventHandler<NotificationEventArgs>(pushChannel_ShellToastNotificationReceived);

                pushChannel.Open();

                // Bind this new channel for toast events
                pushChannel.BindToShellToast();
            }
            else
            {
                // The channel was already open, so just register for all the events
                pushChannel.ChannelUriUpdated +=
                    new EventHandler<NotificationChannelUriEventArgs>(pushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred +=
                    new EventHandler<NotificationChannelErrorEventArgs>(pushChannel_ErrorOccurred);

                // Register for this notification only if you need to receive 
                // the notificiation while your application is running
                pushChannel.ShellToastNotificationReceived +=
                    new EventHandler<NotificationEventArgs>(pushChannel_ShellToastNotificationReceived);

                // Display the URI for testing purposes. Normally, the URI would be passed
                // back to your web service at this point
                System.Diagnostics.Debug.WriteLine(pushChannel.ChannelUri.ToString());
                MessageBox.Show(String.Format("Channel Uri is {0}", pushChannel.ChannelUri.ToString()));

                WebClient wc = new WebClient();
                string uri = "http://api.metroairship.com/register";
                string postMessage = "device_url=" + pushChannel.ChannelUri.ToString();
                wc.UploadStringCompleted += new UploadStringCompletedEventHandler(wc_UploadStringCompleted);
                wc.UploadStringAsync(new Uri(uri), postMessage);
            }
        }

        void wc_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            MessageBox.Show(e.Result);
        }


        void pushChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
                {
                    // Display the new URI for testing purposes.  Normaly, the URI
                    // would be passed back to your web service at this point.
                    System.Diagnostics.Debug.WriteLine(e.ChannelUri.ToString());
                    MessageBox.Show(string.Format("Channel URI is {0}",
                        e.ChannelUri.ToString()));
                });
        }


        void pushChannel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
            StringBuilder message = new StringBuilder();
            string relativeUri = string.Empty;

            message.AppendFormat("Received Toast {0}:\n", DateTime.Now.ToShortTimeString());

            // Parse out the information that was part of the message.
            foreach (string key in e.Collection.Keys)
            {
                message.AppendFormat("{0}: {1}\n", key, e.Collection[key]);

                if (string.Compare(key, "wp:Param",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.CompareOptions.IgnoreCase) == 0)
                {
                    relativeUri = e.Collection[key];
                }
            }

            // Display a dialog of all the fields in the toast.
            Dispatcher.BeginInvoke(() => MessageBox.Show(message.ToString()));
        }


        void pushChannel_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            // Error handling logic
            Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show(String.Format("A push notification {0} error occurred. {1} ({2}) {3}",
                        e.ErrorType, e.Message, e.ErrorCode, e.ErrorAdditionalData));
                });
        }


        private void buttonNavigate_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/Page1.xaml?NavigatedFrom=MainPage", UriKind.Relative));
        }

    }
}