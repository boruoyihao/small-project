using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Handlers;
using System.Web;
using System.Configuration;

namespace MyServer
{
    class RequestAnalyse
    {
        RequestModel requestModel;
        MyServer.Comunication.DGShowMsg dgShowMsg;
        
        public RequestAnalyse(RequestModel requestModel, MyServer.Comunication.DGShowMsg dgShowMsg)
        {
            this.requestModel = requestModel;
            this.dgShowMsg = dgShowMsg;
        }

        //public string GetFolderName() { 
           
            
        //    return 
        //}
        public ResponseModel ProcessWithExtention()
        {
            ResponseModel responseModel = null;//1.获得被请求文件的后缀
            string strFileExtention = System.IO.Path.GetExtension(requestModel.Path);
            
            switch (strFileExtention)
            {
                case ".html":
                case ".css":
                case ".js":
                    responseModel = ProcessStaticPage(requestModel.Path);
                    break;
                case ".jpg":
                case ".gif":
                    responseModel = ProcessImg(requestModel.Path);
                    break;
                case ".aspx":
                case ".ashx":
                case ".jsp":
                case ".php":
                    responseModel = ProcessAyn(requestModel.Path);
                    break;
            }
            return responseModel;
        }


        ResponseModel ProcessStaticPage(string strPath)
        {
            String dataDir = ConfigurationManager.AppSettings["Folder"];
           // dgShowMsg("文件路径"+str);
            //获得当前程序集的文件夹路径
            //string dataDir = AppDomain.CurrentDomain.BaseDirectory;
            // 获得被请求文件的物理路径
            dataDir += "" + System.IO.Path.GetFileName(strPath); 
           // dataDir += "" + "index.html";
            //string strFileName = System.IO.Path.GetFileName(strPath);
            dgShowMsg("路径" +dataDir);
            // 一次性读取文件所有的数据
            string strFileContent = File.ReadAllText(dataDir);
            dgShowMsg("返回内容"+strFileContent);
            // 将请求文件数据 转成字节数组
            byte[] arrFileBody =
            Encoding.UTF8.GetBytes(strFileContent);
            //创建 响应报文实体 对象，并传入 后缀名和 响应报文体数据
            return new ResponseModel(Path.GetExtension(strPath), arrFileBody);
        }

        private ResponseModel ProcessImg(string strPath)
        {
            //获得正在运行程序集的路径
            string webPath1 =
            AppDomain.CurrentDomain.BaseDirectory;
            dgShowMsg("图片地址"+webPath1);
            String webPath = ConfigurationManager.AppSettings["Folder"];
            using (FileStream fs = new FileStream(webPath + strPath, FileMode.Open))
            {
                byte[] arr = new byte[1024 * 1024 * 2];
                int length = fs.Read(arr, 0, arr.Length);
                byte[] arrNew = new byte[length];
                Buffer.BlockCopy(arr, 0, arrNew, 0, length);

                return new ResponseModel(Path.GetExtension(strPath), arrNew);
            }
        }


        private ResponseModel ProcessAyn(string strPath)// ClassList.aspx
        {
            
            //获得去掉后缀名的文件名，然后加上 命名空间名称，组合成一个 类的全名称
            string strFileWithOutExtention =
            Path.GetFileNameWithoutExtension(strPath);// ClassList
            string className = "MyServer." + strFileWithOutExtention;//类的全名称：Web服务器.ClassList
            dgShowMsg("类的名字"+className);
            //获得当前程序集
            Assembly asse = Assembly.GetExecutingAssembly();
            //获得当前程序集里指定名称的 类型 如:ClassList
            Type t = asse.GetType(className);
            //反射创建类的对象 
            dgShowMsg("Type类型");
            object o = Activator.CreateInstance(t); // new ClassList
            dgShowMsg("产生类型"+o.ToString());
          //  IHttpHandler iPage = o as IHttpHandler;
            MyServer.display iPage = o as display; 
            //调用 接口 的 PR 方法，获得 生成的页面html字符串
            string strHtml = iPage.ProcessRequest();
            //string strHtml = "hello";
            //封装到 响应报文实体对象中，并返回
            return new ResponseModel(Path.GetExtension(strPath), Encoding.UTF8.GetBytes(strHtml));
        }
    }
}
