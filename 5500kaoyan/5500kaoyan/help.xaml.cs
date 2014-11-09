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

namespace _5500kaoyan
{
    public partial class help : PhoneApplicationPage
    {
        public help()
        {
            InitializeComponent();
            help1.Text = "1.本软件使用之前必须把设置-语音-语音语言改为English（United States）如下图所示\n2.本软件可以把英文单词加入收藏夹，以方便闲暇记忆\n3.双击句子可以播放声音，收藏夹中在句子上划线可以取消收藏，普通页面中在句子上划线可以加入收藏夹";
            pic.Source = new BitmapImage(new Uri("icon/voice.jpg",UriKind.Relative));
        }
    }
}