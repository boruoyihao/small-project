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
using System.IO.IsolatedStorage;
using Coding4Fun.Toolkit.Controls;
using Windows.Phone.Speech.Synthesis;

namespace _5500kaoyan
{
    public partial class note : PhoneApplicationPage
    {
        private SpeechSynthesizer voice;
        private IsolatedStorageSettings _appSettings = Single.Instance;
        public note()
        {
            InitializeComponent();
           // NavigationService.RemoveBackEntry();
            this.voice = new SpeechSynthesizer();
            this.BackKeyPress += new EventHandler<System.ComponentModel.CancelEventArgs>(MainPage_BackKeyPress);
           
        }
        void MainPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
        private void StackPanel_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            if (Math.Abs(e.TotalManipulation.Translation.X) > 200 && Math.Abs(e.TotalManipulation.Translation.Y) < 70) {
                StackPanel s = sender as StackPanel;
                TextBlock t = s.Children[0] as TextBlock;
                string str=t.Text.ToString();
               // MessageBox.Show(str);
                _appSettings.Remove(str);
                _appSettings.Save();
                var toast = new ToastPrompt
                {
                    Message = "已经删除"
                };
                toast.Show();
                
                NavigationService.Navigate(new Uri("/note.xaml?id="+Guid.NewGuid(), UriKind.Relative));
               // s.Opacity=0;
                NavigationService.RemoveBackEntry();
            }
        }

        private async void StackPanel_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            StackPanel stackPanel = sender as StackPanel;
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
    }
    public class SentenceNote
    {
        public String English { get; set; }
        public String Chinese { get; set; }
        public SentenceNote(String English, String Chinese)
        {
            this.English = English;
            this.Chinese = Chinese;
        }
    }

    public class SentencesNote : ObservableCollection<SentenceNote>
    {
        private IsolatedStorageSettings _appSettings = Single.Instance;
        public SentencesNote()
        {
           // _appSettings.Clear();
            if (_appSettings.Count>0)
            {
                //foreach (string key in _appSettings.Keys)
                //{
                //    //if (value.Contains("*"))
                //    //{
                //    //    string[] str = value.Split('*');
                //    //    Add(new SentenceNote(str[0], str[1]));
                        
                //    //}
                    
                //    Add(new SentenceNote(key,));

                //}
                for(int i=0;i<_appSettings.Count;i++){

                    if (!_appSettings.ElementAt(i).Value.ToString().Contains("1.0.0.0"))
                    Add(new SentenceNote(_appSettings.ElementAt(i).Key.ToString(),_appSettings.ElementAt(i).Value.ToString()));
                }
            }
            else
                return;
        }

    }
}