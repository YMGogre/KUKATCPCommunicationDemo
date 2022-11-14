# KUKA库卡机械臂通讯测试Demo
* 解决方案名称为《KUKATCPTestApp》，放在WindowsFormsTCPClientApp文件夹下。解决方案下包含三个项目：《WindowsFormsTCPServerApp》、《WindowsFormsTCPClientApp》、《WindowsFormsKRLTCPServerApp》，其中前两个项目为基于 BytesIO TCP 通讯库的测试项目，可以用于测试机械臂通讯Demo；而WindowsFormsKRLTCPServerApp是基于TCP/IP协议簇、使用Socket接口、采用XML结构传输数据的库卡机械臂通讯测试项目。
1. 在项目文件夹下的bin/debug目录下打开应用程序：

![图1](%E6%89%93%E5%BC%80%E5%BA%94%E7%94%A8%E7%A8%8B%E5%BA%8F.png)
![图2](%E6%89%93%E5%BC%80%E5%AE%A2%E6%88%B7%E7%AB%AF%E6%B5%8B%E8%AF%95%E7%A8%8B%E5%BA%8F.png)

2. 配置服务端IP地址与端口号，服务端的IP地址和端口号需要与客户端设置的主机IP地址和端口号一致：

![图3](%E9%85%8D%E7%BD%AEIP%E5%9C%B0%E5%9D%80%E5%92%8C%E7%AB%AF%E5%8F%A3%E5%8F%B7.png)

> 服务器端的IP地址和端口号在未开始监听的情况下可双击修改，修改后按Enter确认（***必须按Enter确认键后才算修改成功***），开始监听后不再能修改

3. WindowsFormsKRLTCPServerApp作为TCP服务端监听连接，开始监听后可在客户端点击连接，连接成功后日志框弹出提示：

![图4](%E6%88%90%E5%8A%9F%E8%BF%9E%E6%8E%A5.png)

4. KRL_TCP服务端按照配置好的XML文件格式输入要发送的信息：

![图5](%E5%87%86%E5%A4%87%E5%8F%91%E9%80%81.png)

5. 点击"发送"，两边日志栏会有相应提示：

![图6](%E5%8F%91%E9%80%81%E5%AE%8C%E6%88%90.png)

6. 以上是通过Demo中自带的客户端测试程序来测试库卡机械臂通讯上位机的。在实际上位机与机械臂通讯测试时可以通过机械臂示教器查看变量的值确定发送是否成功(***机械臂那边别忘了要先在系统文件 $CONFIG.DAT 声明全局变量Nmb***)：

![图7](%E6%9F%A5%E7%9C%8B%E5%8F%98%E9%87%8F%E5%80%BC.jpg)

7. 记住一点：上位机发的始终只是字符串！比如上位机想发整数10，实际上发过去的也只是字符"1"和"0"过去。至于机械臂那边想怎么解析它完全是看 xml 配置文件里是如何配置的。