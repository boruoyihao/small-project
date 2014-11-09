using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using System.Threading;

namespace _5500kaoyan
{
    public partial class Splash : PhoneApplicationPage
    {
        public Splash()
        {
            InitializeComponent();
            pic.Source = new BitmapImage(new Uri("icon/splash.jpg", UriKind.Relative));
            //Thread t = new Thread(new ThreadStart(Navigate));
            //t.Start()
            //Thread.Sleep(3000);
            //this.Loaded += new RoutedEventHandler(MainPage_loaded);
            
        }
        //private void MainPage_loaded(object sender, RoutedEventArgs e)
        //{
            
        //    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));  
        //    NavigationService.RemoveBackEntry();
        //}

    }
}