using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using System.IO;
using Windows.Phone.Speech.Synthesis;
using Coding4Fun.Toolkit.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Devices;
using System.IO.IsolatedStorage;
using CN.SmartMad.Ads.WindowsPhone.WPF;
using System.Text;
using System.Threading;

namespace _5500kaoyan
{
    public partial class Display : PhoneApplicationPage
    {
        private SpeechSynthesizer voice;
        private IsolatedStorageSettings _appSettings=Single.Instance;
        private StringBuilder sb = new StringBuilder();
        public Display()
        {

            InitializeComponent();
            this.voice = new SpeechSynthesizer();
            this.BackKeyPress += new EventHandler<System.ComponentModel.CancelEventArgs>(MainPage_BackKeyPress);
            //Thread t = new Thread(new ThreadStart(processBar));
            //t.Start();

            
        }
        //private void processBar() {
        //    for (int i = 0; i < 1000; i++)
        //    {
        //        processbar1.Value = 0;
        //        processbar1.IsIndeterminate = true;
        //        processbar1.Visibility = System.Windows.Visibility.Visible;
        //    }
        //    processbar1.Visibility = System.Windows.Visibility.Collapsed;
        //}
        void MainPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            VibrateController vc = VibrateController.Default;//振动
            vc.Start(TimeSpan.FromMilliseconds(100));
            //voice.Dispose();
        }
        //private async void TextBlock_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        //{
        //   // MessageBox.Show("double click");
        //    TextBlock t= sender as TextBlock;
        //    string s=t.Text.ToString().Split('.')[1];
        //   // MessageBox.Show(s);

        //    var toast = new ToastPrompt
        //    {
        //        Message = "播放语音"
        //    };
            
        //    toast.Show();
        //    try
        //    {
        //        if (s != "") {
        //            await voice.SpeakTextAsync(s);
        //        }
        //    }
        //    catch {
               
        //    }
        //}



        private async void StackPanel_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
          //  MessageBox.Show("Double Click");
          //  sb.Clear();
            StackPanel stackPanel= sender as StackPanel;
            TextBlock t = stackPanel.Children[0] as TextBlock;
            string s = t.Text.ToString();
            var toast = new ToastPrompt
            {
                Message = "播放语音"
            };

            toast.Show();
            try
            {
                if (s != "")
                {
                    await voice.SpeakTextAsync(s);
                }
            }
            catch
            {

            }
            

        }

        private void StackPanel_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            if (Math.Abs(e.TotalManipulation.Translation.X)>200&&Math.Abs(e.TotalManipulation.Translation.Y)<70) {
                //MessageBox.Show("dd");
                StackPanel s = sender as StackPanel;
                TextBlock t = s.Children[0] as TextBlock;
                TextBlock t1 = s.Children[1] as TextBlock;
                string key = t.Text.ToString();
                string value=t1.Text.ToString();
                //MessageBox.Show(key);
                if (_appSettings.Contains(key))
                {
                    var toast = new ToastPrompt
                    {
                        Message = "已经收藏过"
                    };
                    toast.Show();
                    return;
                }
                else
                {
                    _appSettings.Add(key,value);
                    _appSettings.Save();
                    var toast1 = new ToastPrompt
                    {
                        Message = "收藏到记事本完成"
                    };
                    toast1.Show();
                }
            }
        }

    }
    public class Sentence
    {
        public String English { get; set; }
        public String Chinese { get; set; }
        public Sentence(String English, String Chinese)
        {
            this.English = English;
            this.Chinese = Chinese;
        }
    }

    public class Sentences : ObservableCollection<Sentence>
    {
        public Sentences()
        {
            StreamReader objReader = new StreamReader("txt/" + _5500kaoyan.MainPage.DataID.id + ".txt");
            string sLineChinese = "";
            string sLineEnglish = "";
            while (sLineChinese != null)
            {
                sLineEnglish = objReader.ReadLine();
                sLineChinese = objReader.ReadLine();
                if (sLineChinese != null && !sLineChinese.Equals(""))
                    Add(new Sentence(sLineEnglish, sLineChinese));
            }
            objReader.Close();
        }

    }

}