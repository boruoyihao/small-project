using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace MyServer
{
    class Comunication
    {
        public delegate void DGShowMsg(string msg);
        Thread thrMsg;
        Socket sokMsg;
        DGShowMsg dgShowMsg;
        bool isReceive = true;
        //通过构造函数，自动开启通信线程
        public Comunication(Socket sok, DGShowMsg dgShowMsg)
        {
            this.sokMsg = sok;
            this.dgShowMsg = dgShowMsg;
            //创建线程
            thrMsg = new Thread(StartReceive);
            thrMsg.IsBackground = true;
            thrMsg.Start();
        }

        public void StartReceive()
        {
            byte[] msgArr = new byte[1024 * 1024];
            try
            {
                while (isReceive)
                {
                    int i = 0;
                    string data = null;
                    if ((i = sokMsg.Receive(msgArr)) != 0)
                        data = System.Text.Encoding.UTF8.GetString(msgArr, 0, i);

                    RequestModel requestModel = new RequestModel(data);
                    dgShowMsg(data);
                    RequestAnalyse analyse = new RequestAnalyse(requestModel, dgShowMsg);
                    ResponseModel responseModel = analyse.ProcessWithExtention();
                    sokMsg.Send(responseModel.GetResponseHeader());//发送响应报文头
                    sokMsg.Send(Encoding.UTF8.GetBytes("\n\n"));
                    sokMsg.Send(responseModel.GetContentBody());//发送响应包文体
                    //dgShowMsg(responseModel.GetResponseHeader().ToString());
                    //dgShowMsg(responseModel.GetContentBody().ToString());
                    dgShowMsg("***********************发送响应报文完毕！***********************");
                    isReceive = false;
                }

                sokMsg.Shutdown(SocketShutdown.Both);
                sokMsg.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("抛出异常" + e.GetBaseException());
            }
        }


    }
}
