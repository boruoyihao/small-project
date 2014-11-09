using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyServer
{
    public partial class Form1 : Form
    {
        

        private Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//定义监听套接字
        public Form1()
        {
            InitializeComponent();
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }

        //public  string GetPath() { 

        //        return serverpath.Text.ToString();

        //}

        private void button1_Click(object sender, EventArgs e)
        {
            //string IPstr = IPtext.Text.Trim();
            //string portstr = porttext.Text.Trim();
            if (serverpath.Text.Trim()=="")
            {
                showMessage("请首先设置项目根目录");
                return;
            }
            if (IPtext.Text.Trim() == ""||porttext.Text.Trim()=="") 
            {
                showMessage("没有写IP或端口号，默认的IP为127.0.0.1，端口为8888");
                IPtext.Text = "127.0.0.1";
                porttext.Text = "8888";
            }

            showMessage("服务器启动....");
            //Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//定义监听套接字
            IPAddress address = IPAddress.Parse(IPtext.Text.Trim());//创建IP对象
            IPEndPoint endpoint = new IPEndPoint(address, int.Parse(porttext.Text.Trim()));//创建包含端口的网络节点对象

            listener.Bind(endpoint);
            listener.Listen(10);//设置监听队列
            Thread threadlisten;//定义监听线程
            // showMessage("工作中");
            threadlisten = new Thread(startlisten);
            threadlisten.IsBackground = true;//创建线程时默认false，为false时，主线程退出，仍然执行，直到结束

            threadlisten.Start();

        }

        public void startlisten()
        {
          
            while (true)
            {
               
                Socket sokconn = listener.Accept();
                Comunication comunication = new Comunication(sokconn, showMessage);

            }
        }

        //打印提示信息
        public void showMessage(String strMsg)
        {
            //if(this.invokedRequired)
            //{
            //    DGMessage dg = new DGMessage(showMessage);
            //}
            output.AppendText(strMsg + "\r\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // OpenFileDialog ofd = new OpenFileDialog();
           // ofd.Title = "打开";
            
           //// ofd.Filter="所有文件|*.*";  //这是设置扩展名。
           // if (ofd.ShowDialog() == DialogResult.OK)
           // {
           //     serverpath.Text = ofd.FileName;
           // }

            FolderBrowserDialog folder =
                                new System.Windows.Forms.FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                serverpath.Text = folder.SelectedPath;
            }
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None); 
            cfa.AppSettings.Settings["Folder"].Value = serverpath.Text+"\\"; 
            cfa.Save();

        }



        private void clear_Click(object sender, EventArgs e)
        {
            output.Clear();
        }

        private void export_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder =
                    new System.Windows.Forms.FolderBrowserDialog();
            
            if (folder.ShowDialog() == DialogResult.OK)
            {
                serverpath.Text = folder.SelectedPath;
            }
            writeToFile(folder.SelectedPath);

        }

        private void writeToFile(string path) 
        {
            try
            {

                string filePath = path + "\\" + string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now) + ".txt";
                if (MessageBox.Show("确定导出吗？", "确认信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                    return;
                FileStream aFile = new FileStream(filePath, FileMode.CreateNew);
                StreamWriter sw = new StreamWriter(aFile);
                sw.WriteLine(DateTime.Now+"记录：");
                sw.WriteLine(output.Text);
                sw.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine("An IOException has been thrown!"+ex.GetBaseException());
                MessageBox.Show("发生异常，请重新操作");
                Console.ReadLine();
                return;
            }
        }



    }
}
