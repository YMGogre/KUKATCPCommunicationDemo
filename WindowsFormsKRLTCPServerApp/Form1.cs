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
        /**
         * Socket 是一个网络通信的套接字（接口），用于建立连接、发送和接收数据。它是应用程序与网络之间的抽象层，为应用程序提供了一种与网络进行通信的方式。
         * EndPoint 是一个抽象类，表示网络上的一个终结点。它包含了 IP 地址和端口号，用于标识网络上的一个唯一地址。
         * 在使用 Socket 进行通信时，需要使用 EndPoint 来指定目标地址。
         * 简单来说，Socket 是用于建立连接和传输数据的接口，而 EndPoint 是用于标识网络上的一个唯一地址。
         * 两者之间的区别在于，Socket 是用于实现通信功能的，而 EndPoint 是用于指定通信目标的。
         * 一个 Socket 对象通常只用于一个应用程序和网络之间的通信。每个 Socket 对象都有一个唯一的本地终结点和远程终结点，用于标识通信的两端。
         * 如果一个应用程序需要与多个远程终结点进行通信，那么它可以创建多个 Socket 对象，每个 Socket 对象都与一个远程终结点相关联。
         * 这样，应用程序就可以使用不同的 Socket 对象与不同的远程终结点进行通信。
         */
        Thread tcpDelegate;             
        Socket[] Client;                    //使用一个 Socket 数组存储客户端 Socket 实例
        int theIndex;
        Hashtable _sessionTable;            //使用一张哈希表存储远端终结点实例
        string[,] theListClient;            //使用一个二维字符串数组存储所有已连接的 Socket 活跃状态
        IPEndPoint localEP;                 //本地终结点实例对象
        int setPort = 59152;                //本地终结点的端口号
        string ipaddress = "127.0.0.1";     //本地终结点的IP地址（使用本地环回地址或者服务器的公网/局域网的IP地址）

        public Form1()
        {
            InitializeComponent();
            //CheckForIllegalCrossThreadCalls = false;      //不检查线程间操作（如果程序抛出了“线程间操作无效”异常，可以取消该行代码注释）
            Client = new Socket[256];
            theListClient = new string[256,2];
            _sessionTable = new Hashtable(53);
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
            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);     //初始化 Socket 实例，该 Socket 只用于绑定本地终结点并接受客户端连接
            listener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);        //允许 Socket 被绑定在已使用的地址上
            try
            {
                localEP = new IPEndPoint(IPAddress.Parse(ipaddress), setPort);                              //初始化本地终结点实例
                listener.Bind(localEP);                //绑定
                listener.Listen(10);                   //监听
                Print("开始监听");
                listener.BeginAccept(new AsyncCallback(OnConnectRequest), listener);                        //开始接受异步连接
                /** 
                 * BeginAccept 方法的第二个参数（object state）是一个用户定义的对象，它可以传递给回调函数，该参数可以用于在回调函数中传递额外的信息。
                 * 比如我们将监听 Socket 对象作为 state 参数传递给了 BeginAccept 方法，在 BeginAccept 方法中，它会使用监听 Socket 对象去初始化一个 IAsyncResult 对象并返回。
                 * IAsyncResult 对象包含了一个 AsyncState 属性，它的值就是传递给 BeginAccept 方法的第二个参数 state。这样，我们就能够在回调函数中使用该属性来获取监听 Socket 对象，以便继续监听客户端连接。
                 * 当然，可以根据需要传递任何类型的对象作为 state 参数。这个参数对于在回调函数中传递额外的信息非常有用。
                 */
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 客户端连接
        /// </summary>
        /// <param name="ar">异步操作的结果</param>
        public void OnConnectRequest(IAsyncResult ar)
        {
            var listener = (Socket)ar.AsyncState;           //在回调函数中获取监听 Socket 对象以便继续监听客户端连接
            Client[theIndex] = listener.EndAccept(ar);      //结束异步接受操作。该方法会返回一个新的 Socket，新的 Socket 对象才是用于与客户端进行通信的
            byte[] byteDateLine = new byte[65536];          //创建一个 64KB 的接收数据缓冲区
            var remote = Client[theIndex].RemoteEndPoint;   //RemoteEndPoint：IP + Port；e.g. 127.0.0.1:55991
            _sessionTable.Add(remote, null);                //把连接成功的客户端的 RemoteEndPoint 实例放入哈希表（作为键存储，所有键的值为null）
            Print($"客户端[{Client[theIndex].RemoteEndPoint}]连接成功！");
            theListClient[theIndex,0] = Client[theIndex].RemoteEndPoint.ToString();     //二维数组第一列放 RemoteEndPoint
            theListClient[theIndex,1] = "1";                                            //二维数组第二列放 RemoteEndPoint 状态："1"表示当前远端终结点为激活状态；"0"表示为失效状态
            listener.BeginAccept(new AsyncCallback(OnConnectRequest), listener);        //继续监听等待新的客户端连接
            theIndex++;                                     //索引+1
            int myIndex = theIndex - 1;                     //获取当前连接的 Socket 索引
            while (true)                                    //进入死循环持续接收来自客户端的消息
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
                        Print($"收到来自[{ip}]消息：" + stringdata);
                    }
                    else
                    {
                        Print($"客户端[{ip}]已断开连接！");
                        break;
                    }
                }
                catch (SocketException e)
                {
                    string ip = Client[myIndex].RemoteEndPoint.ToString();      //从列表中移除通讯失败的客户端
                    _sessionTable.Remove(Client[myIndex].RemoteEndPoint);
                    for (int i = 0; i < 256; i++)
                    {
                        if (Client[myIndex].RemoteEndPoint.ToString() == theListClient[i,0])
                            theListClient[i,1] = "0";
                    }
                    Print($"{e.Message}客户端[{ip}]断开了连接！");
                    break;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    break;
                }
            }
        }

        /// <summary>
        /// 将 XML 格式字符串数据发送给所有连接的 <see cref="Socket"/>
        /// </summary>
        /// <param name="xmlStr">XML 格式字符串</param>
        private void sendToRobot(string xmlStr)
        {
            try
            {
                byte[] sendData = Encoding.UTF8.GetBytes(xmlStr);
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
                                        /**
                                         * Socket.SendTo 方法用于在无连接协议（如 UDP）中发送数据。在无连接协议中，每个数据包都是独立发送的，不需要建立连接。
                                         * 因此，可以使用 Socket.SendTo 方法将数据发送到指定的远程终结点，而不需要预先建立连接。
                                         * 在使用面向连接的协议（如 TCP）时，客户端需要先使用 Socket.Connect 方法建立连接（或者服务端需要监听并接受连接），然后才能使用 Socket.Send 方法发送数据。
                                         * 在这种情况下，每个 Socket 对象都有一个唯一的远程终结点，用于标识通信的目标地址。
                                         */
                                        //if (Client[i].SendTo(sendData, temp) == sendData.Length)
                                        if (Client[i].Send(sendData) == sendData.Length)
                                        {
                                            Print($"向客户端[{temp}]发送消息：{xmlStr}");
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

        /// <summary>
        /// 将数据发送到连接的指定 <see cref="Socket"/>
        /// </summary>
        /// <param name="socket">连接的 <see cref="Socket"/></param>
        /// <param name="sendStr">要发送的字符串</param>
        /// <returns>发送到 <see cref="Socket"/> 的字节数</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="SocketException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public int SendToRobot(Socket socket, string sendStr)
        {
            byte[] sendData = Encoding.UTF8.GetBytes(sendStr);
            return socket.Send(sendData);
        }

        /// <summary>
        /// 将指定格式的字符串转换为 XML 格式字符串
        /// </summary>
        /// <param name="str">指定格式字符串（格式形如 "X = 1"，允许出现空格）</param>
        /// <returns>XML 格式的字符串</returns>
        /// <!-- 本方法对应的 XML 配置文件定义请参阅<https://github.com/YMGogre/KUKATCPCommunicationDemo/blob/master/XmlTransmit.xml> -->
        private string XMLConvert(string str)
        {
            string header = "<Sensor>", tail = "</Sensor>";//变量xml文件中的根元素，这需要与你在机械臂端配置的 xml 文件中的父元素相同
            str = str.Replace(" ", "");
            string[] fields = str.Split(',', '，');
            //设置xml格式字符串中变量的值，格式：<变量名>变量值</变量名>，注意一定得是上面设置的根元素的子元素才可以，再往下一级就不行了
            //开始构建 XML 字符串
            string xmlStr = header;
            foreach (string item in fields)
            {
                string[] arr = item.Split('=');
                xmlStr += "<" + arr[0] + ">" + arr[1] + "</" + arr[0] + ">";
            }
            xmlStr += tail;
            /** 
             * 如果还有再往下的 XML 树结构要发送，则需要再添代码补充。比如：
             * xmlStr = xmlStr + "<Sensor><Status><IsActive>FALSE</IsActive></Status></Sensor>";
             * xmlStr = xmlStr + "<Sensor><Read><xyzabc X='10.0' Y='20.0' Z='30.0' A='40.0' B='50.0' C='60.0'></xyzabc></Read></Sensor>";
             */
            return xmlStr;
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
            sendToRobot(XMLConvert(richTextBox_Send.Text));
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
