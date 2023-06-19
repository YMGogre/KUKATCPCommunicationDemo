using STTech.BytesIO.Tcp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsTestApp
{
    public partial class FormTCPClient : Form
    {

        private TcpClient tcpClient;
        public FormTCPClient()
        {
            InitializeComponent();
            tcpClient = new TcpClient() { Port = 59152 };
            propertyGrid.SelectedObject = tcpClient;
            tcpClient.OnDataReceived += TcpClient_OnDataReceived;
            tcpClient.OnConnectedSuccessfully += TcpClient_OnConnectedSuccessfully;
            tcpClient.OnDisconnected += TcpClient_OnDisconnected;
        }

        private void TcpClient_OnDisconnected(object sender, STTech.BytesIO.Core.DisconnectedEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                tbReceive.AppendText($"[{DateTime.Now}] {"断开连接"}\n");
            }));
        }

        private void TcpClient_OnConnectedSuccessfully(object sender, STTech.BytesIO.Core.ConnectedSuccessfullyEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                tbReceive.AppendText($"[{DateTime.Now}] {"成功建立连接"}\n");
            }));
        }

        private void TcpClient_OnDataReceived(object sender, STTech.BytesIO.Core.DataReceivedEventArgs e)
        {
            //int length = e.Data.Length / 2;
            //short[] arr = new short[length];
            //uint index = 0;
            //string str = "";
            //for (int i = 0; i < length; i++)
            //{
            //    arr[i] = ByteArrToShort(ref index, e.Data);
            //    str = str + arr[i].ToString();
            //}
            //print("收到来自服务端消息：" + str);
            print("收到消息：" + e.Data.EncodeToString());                    //接收到的字节流GBK编码为字符串   
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            tcpClient.Connect();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            tcpClient.Disconnect();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            tcpClient.Send(tbSend.Text.GetBytes());                         //默认UTF-8编码，但TCP调试工具那边是GBK编码，所以使用TCP调试工具调试时，此句代码改为"GetBytes("GBK")"
            this.Invoke(new Action(() =>
            {
                tbReceive.AppendText($"[{DateTime.Now}] {"发送数据："}" + tbSend.Text + "\n");
                tbSend.Clear();
            }));
        }
        private void print(string msg)
        {
            this.Invoke(new Action(() =>
            {
                tbReceive.AppendText($"[{DateTime.Now}] {msg}\n");      //在打印接收到的数据之前先打印下当前时间
            }));
        }

        private int ByteArrToInt(ref uint b, byte[] byteArr)
        {
            int value;
            value = byteArr[b + 3] * 16777216 + byteArr[b + 2] * 65536 + byteArr[b + 1] * 256 + byteArr[b];
            b += 4;
            return value;
        }

        private short ByteArrToShort(ref uint b, byte[] byteArr)
        {
            short value;
            value = (short)(byteArr[b + 1] * 256 + byteArr[b]);
            b += 2;
            return value;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                tbReceive.Clear();
            }));
        }
    }
}
