using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsKRLTCPServerApp
{
    public partial class Form1 : Form
    {
        Thread tcpDelegate;
        IPEndPoint localEP;
        Socket KRLSocket;
        Socket server1;
        Socket[] Client;
        int theIndex;
        EndPoint remote;
        Hashtable _sessionTable;
        string[,] theListClient;
        int setPort = 59152;
        string ipaddress = "127.0.0.1";

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Client = new Socket[256];
            theListClient = new string[256,2];
        }
        
        /// <summary>
        /// 启动TCP服务端监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiStart_Click(object sender, EventArgs e)
        {
            OpenTCP();
            tstbIP.Enabled = false;
            tstbPort.Enabled = false;
        }

        /// <summary>
        /// TCP放在后台线程
        /// </summary>
        private void OpenTCP()
        {
            ThreadStart TCPDelegate = new ThreadStart(TCPListen);   //新建一个委托线程
            tcpDelegate = new Thread(TCPDelegate);                  //实例化新线程
            tcpDelegate.Start();
        }

        /// <summary>
        /// 创建TCPServer并监听
        /// </summary>
        public void TCPListen()
        {
            KRLSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);    //初始化Socket实例
            KRLSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);   //允许Socket被绑定在已使用的地址上
            try
            {
                localEP = new IPEndPoint(IPAddress.Parse(ipaddress), setPort);                              //初始化终结点实例
                _sessionTable = new Hashtable(53);
                KRLSocket.Bind(localEP);                //绑定
                KRLSocket.Listen(10);                   //监听
                Print("开始监听");
                KRLSocket.BeginAccept(new AsyncCallback(OnConnectRequest), KRLSocket);  //开始接受异步连接
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 客户端连接
        /// </summary>
        /// <param name="ar"></param>
        public void OnConnectRequest(IAsyncResult ar)
        {
            server1 = (Socket)ar.AsyncState;            //初始化一个Socket，用于其他客户端的连接
            Client[theIndex] = server1.EndAccept(ar);
            DateTimeOffset now = DateTimeOffset.Now;
            Byte[] byteDateLine = new Byte[65534];
            remote = Client[theIndex].RemoteEndPoint;
            _sessionTable.Add(remote, null);   //把连接成功的客户端的Socket实例放入哈希表
            Print($"客户端[{Client[theIndex].RemoteEndPoint}]连接成功！");
            theListClient[theIndex,0] = Client[theIndex].RemoteEndPoint.ToString();
            theListClient[theIndex,1] = "1";
            server1.BeginAccept(new AsyncCallback(OnConnectRequest), server1);  //等待新的客户端连接
            theIndex++;
            int myIndex = theIndex - 1;
            while (true)
            {
                try
                {
                    if (theListClient[myIndex,1] == "0")
                        return;
                    Thread.Sleep(150);
                    int recv = Client[myIndex].Receive(byteDateLine);
                    string stringdata = Encoding.UTF8.GetString(byteDateLine, 0, recv);
                    string ip = Client[myIndex].RemoteEndPoint.ToString();
                    if (stringdata.Any())       //接受到客户端消息
                    {
                        Print($"收到来自[{Client[myIndex].RemoteEndPoint}]消息：" + stringdata);
                    }
                }
                catch (Exception)
                {
                    string ip = Client[myIndex].RemoteEndPoint.ToString();  //从列表中移除通讯失败的客户端
                    _sessionTable.Remove(Client[myIndex].RemoteEndPoint);
                    for (int i = 0; i < 256; i++)
                    {
                        if (Client[myIndex].RemoteEndPoint.ToString() == theListClient[i,0])
                            theListClient[i,1] = "0";
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// 将字符串发送给机械臂
        /// </summary>
        /// <param name="str"></param>
        private void sendToRobot(string str)
        {
            string sendstr;
            str = str.Replace(" ", "");
            string[] fields = str.Split(',','，');
            string header = "<Sensor>", tail = "</Sensor>";//变量xml文件中的根元素，这需要与你在机械臂端配置的 xml 文件中的父元素相同
            sendstr = header;
            //设置xml格式字符串中变量的值，格式：<变量名>变量值</变量名>，注意一定得是上面设置的根元素的子元素才可以，再往下一级就不行了
            foreach (string item in fields)
            {
                string[] arr = item.Split('=');
                sendstr += "<" + arr[0] + ">" + arr[1] + "</" + arr[0] + ">";
            }
            sendstr += tail;
            /*如果还有再往下的 XML 树结构要发送，则需要再添代码补充。比如：
             *sendstr = sendstr + "<Sensor><Status><IsActive>FALSE</IsActive></Status></Sensor>";
             *sendstr = sendstr + "<Sensor><Read><xyzabc X='10.0' Y='20.0' Z='30.0' A='40.0' B='50.0' C='60.0'></xyzabc></Read></Sensor>";
             */
            string strDataLine = sendstr;
            try
            {
                Byte[] sendData = Encoding.UTF8.GetBytes(strDataLine);
                foreach (DictionaryEntry de in _sessionTable)
                {
                    EndPoint temp = (EndPoint)de.Key;
                    {
                        for (int i = 0; i < theIndex; i++)
                        {
                            if (theListClient[i, 1] == "1")
                            {
                                if (temp.ToString() == theListClient[i, 0])
                                {
                                    try
                                    {
                                        if(Client[i].SendTo(sendData, temp) == sendData.Length)
                                        {
                                            Print($"向客户端发送消息：{strDataLine}");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Print(string msg)
        {
            this.Invoke(new Action(() =>
            {
                richTextBox_Receive.AppendText($"[{DateTime.Now}] {msg}\n");  //在日志窗口显示消息
            }));
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            sendToRobot(richTextBox_Send.Text);
            this.Invoke(new Action(() =>
            {
                richTextBox_Send.Clear();
            }));
        }

        private void tstbIP_DoubleClick(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                tstbIP.ReadOnly = false;
                tstbIP.Focus();
            }));
        }

        private void tstbIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)                 //回车键的KeyChar是13
            {
                ipaddress = tstbIP.Text;
                this.Invoke(new Action(() =>
                {
                    tstbIP.ReadOnly = true;
                }));
            }
        }

        private void tstbPort_DoubleClick(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                tstbPort.ReadOnly = false;
                tstbPort.Focus();
            }));
        }

        private void tstbPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if( e.KeyChar == 13)
            {
                setPort = int.Parse(tstbPort.Text);
                this.Invoke(new Action(() =>
                {                    
                    tstbPort.ReadOnly = true;
                }));
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                richTextBox_Receive.Clear();
            }));
        }
    }
}
