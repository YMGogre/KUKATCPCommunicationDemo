using STTech.BytesIO.Core;
using STTech.BytesIO.Tcp;
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
using TcpClient = STTech.BytesIO.Tcp.TcpClient;

namespace WindowsFormsTCPServerApp
{
    public partial class FormTCPServer : Form
    {
        private TcpServer server;
        public FormTCPServer()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            server = new TcpServer();
            server.Port = int.Parse(tstb_Port.Text);

            server.Started += Server_Started;
            server.Closed += Server_Closed;
            server.ClientConnected += Server_ClientConnected;
            server.ClientDisconnected += Server_ClientDisconnected;
        }

        private void Server_ClientDisconnected(object sender, STTech.BytesIO.Tcp.Entity.ClientDisconnectedEventArgs e)
        {
            print($"客户端[{e.Client.Host}:{e.Client.Port}]断开连接");
        }

        private void Server_ClientConnected(object sender, STTech.BytesIO.Tcp.Entity.ClientConnectedEventArgs e)
        {
            print($"客户端[{e.Client.Host}:{e.Client.Port}]连接成功");
            e.Client.OnDataReceived += Client_OnDataReceived;
            //e.Client.UseHeartbeatTimeout(3000);                         //心跳超时检查，可设置限定时间内不通信则断开连接的毫秒数
        }

        private void Client_OnDataReceived(object sender, STTech.BytesIO.Core.DataReceivedEventArgs e)
        {
            TcpClient tcpClient = (TcpClient)sender;
            print($"来自客户端[{tcpClient.RemoteEndPoint}]的消息：{e.Data.EncodeToString("GBK")}");
        }

        private void Server_Closed(object sender, EventArgs e)
        {
            print("停止监听");
        }

        private void Server_Started(object sender, EventArgs e)
        {
            print("开始监听");
        }

        private void tsmi_Start_Click(object sender, EventArgs e)
        {
            server.StartAsync();   
        }

        private void tsmi_Close_Click(object sender, EventArgs e)
        {
            server.StopAsync();
        }

        private void print(string msg)
        {
            tbLog.AppendText($"[{DateTime.Now}] {msg}\r\n");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            foreach(TcpClient client in server.Clients)
            {
                client.SendAsync(tbSend.Text.GetBytes("GBK"));
            }
            print($"向所有接入客户端发送：{tbSend.Text}");
            tbSend.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => {
                tbLog.Clear();
            }));
        }
    }
}
