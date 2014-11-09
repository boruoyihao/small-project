using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using _5500kaoyan.Resources;
using System.Collections.ObjectModel;
using Coding4Fun.Toolkit.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Microsoft.Devices;
using Microsoft.Phone.Tasks;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using Windows.ApplicationModel.Activation;
using System.Threading;

namespace _5500kaoyan
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor

        private BackgroundWorker backroungWorker;
        private Popup myPopup;
        public MainPage()
        {
           
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
            
            InitializeComponent();
            
            this.BackKeyPress += new EventHandler<System.ComponentModel.CancelEventArgs>(MainPage_BackKeyPress);
            //this.Loaded += new RoutedEventHandler(MainPage_loaded);
            //Dispatcher.BeginInvoke(() => {
            //    bar.IsVisible = false;
            //});
            myPopup = new Popup() { IsOpen = true, Child = new Splash() };
            backroungWorker = new BackgroundWorker();
            RunBackgroundWorker();
            this.SetValue(SystemTray.IsVisibleProperty, false);
            this.ApplicationBar.IsVisible = false;
        }
        private void RunBackgroundWorker()
        {

            backroungWorker.DoWork += ((s, args) =>
            {
                Thread.Sleep(3000);
            });
            backroungWorker.RunWorkerCompleted += ((s, args) =>
            {
                this.Dispatcher.BeginInvoke(() =>
                {
                    this.myPopup.IsOpen = false;
                    this.SetValue(SystemTray.IsVisibleProperty, true);
                    this.ApplicationBar.IsVisible = true;
                    //Dispatcher.BeginInvoke(() => {
                    //    bar.IsVisible = true;
                    //});
                });
            });
            backroungWorker.RunWorkerAsync();
           
        }
        void MainPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            VibrateController vc = VibrateController.Default;
            vc.Start(TimeSpan.FromMilliseconds(100));
            if (MessageBox.Show("亲，欢迎下次再来", "提醒", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
            {
                e.Cancel = true;//操作取消
            }

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
               Button bt = sender as Button;//转换下
              // char[]id=bt.Content.ToString().ToCharArray();
              //// MessageBox.Show(id[7].ToString());               //就是当前触发的button的iD
              // if (9 == id.Length)
              //     DataID.id = id[7].ToString();
              // else
              //     DataID.id = id[7].ToString() + id[8].ToString();
               DataID.id = bt.Content.ToString();
            NavigationService.Navigate(new Uri("/Display.xaml", UriKind.Relative));
        }
       public class DataID {
            public static String id; 
        }

       private void note(object sender, EventArgs e)
       {
           NavigationService.Navigate(new Uri("/note.xaml", UriKind.Relative));
       }
       private void score(object sender, EventArgs e)
       {
           MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
           marketplaceReviewTask.Show();
       }
       private void about(object sender, EventArgs e)
       {
           MessageBox.Show("这是第二个版本，根据用户的反馈，美化了界面，调节了字体，增加了语音功能，增加了收藏功能，大家在使用前请阅读使用说明，感谢大家的支持！");
       }
       private void help(object sender, EventArgs e)
       {
           NavigationService.Navigate(new Uri("/help.xaml", UriKind.Relative));
       }
    }
    public class ListSentences : ObservableCollection<ListSentence>
    {
        public ListSentences()
        {
            //for (int i = 1; i <= 16; i++)
            //{
            //    Add(new ListSentence("征服英语疯狂第" + i + "天", getColor(i)));
            //}
            for (char i = 'a'; i <= 'z'; i++)
            {
                Add(new ListSentence(i.ToString(),getColor(i-'a')));
            }
        }
        public String getColor(int i)
        {
            String s = "";
            switch (i % 8)
            {
                case 0: s = "Cyan"; break;
                case 1: s = "Red"; break;
                case 2: s = "Cornsilk"; break;
                case 3: s = "blue"; break;
                case 4: s = "Chocolate"; break;
                case 5: s = "BlueViolet"; break;
                case 6: s = "Brown"; break;
                case 7: s = "DarkGoldenrod"; break;
                case 8: s = "Crimson"; break;
            }
            return s;
        }
    }

    public class ListSentence
    {
        public String title { get; set; }
        public String color { get; set; }
        public ListSentence(String title, String color)
        {
            this.color = color;
            this.title = title;
        }
    }
}