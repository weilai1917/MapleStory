using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EasyMaple
{
    public class ConnectServer
    {
        //监听套接字
        public Socket listenSocket;
        //客户端连接(异步)
        public Conn[] conns;
        //最大连接数
        public int maxCount = 100;

        //获取连接池索引
        public int NewIndex()
        {
            if (conns == null)
                return -1;

            for (int i = 0; i < conns.Length; i++)
            {
                if (conns[i] == null)
                {
                    conns[i] = new Conn();
                    return i;
                }
                else if (conns[i].isUse == false)
                {
                    return i;
                }
            }
            return -1;
        }

        //开启服务器
        public void Start(IPEndPoint iPEndPoint)
        {
            //连接对象池
            conns = new Conn[maxCount];
            for (int i = 0; i < maxCount; i++)
            {
                conns[i] = new Conn();
            }
            //socket
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //bind

            listenSocket.Bind(iPEndPoint);

            //listen
            listenSocket.Listen(maxCount);

            //Accept
            listenSocket.BeginAccept(AcceptCb, null);
        }

        //Accept回调函数
        private void AcceptCb(IAsyncResult asyncResult)
        {
            try
            {
                Socket socket = listenSocket.EndAccept(asyncResult);
                int index = NewIndex();

                if (index == -1)
                {
                    Console.WriteLine("Conn连接池已满!");
                }
                else
                {
                    Conn conn = conns[index];
                    conn.Init(socket);
                    string addr = conn.GetAddress();
                    Console.WriteLine("客户端 [" + addr + "] 连接");
                    Console.WriteLine("已连接 " + index + " 个客户端");
                    conn.socket.BeginReceive(conn.readBuff, conn.buffCount, conn.BuffRemain(), SocketFlags.None, ReceiveCb, conn);
                    listenSocket.BeginAccept(AcceptCb, null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("AcceptCb 失败：" + ex.Message);
            }
        }

        //Receive回调函数
        private void ReceiveCb(IAsyncResult asyncResult)
        {
            //获取接收对象
            Conn conn = (Conn)asyncResult.AsyncState;
            try
            {
                int count = conn.socket.EndReceive(asyncResult);
                //关闭信号
                if (count <= 0)
                {
                    Console.WriteLine("收到 [" + conn.GetAddress() + "] 断开连接");
                    conn.Close();

                    return;
                }

                //数据处理
                string str = conn.socket.RemoteEndPoint.ToString() + ":" + Encoding.UTF8.GetString(conn.readBuff, 0, count);
                Console.WriteLine("[" + conn.GetAddress() + "] : " + str);

                byte[] sendMsg = Encoding.UTF8.GetBytes(str);
                //将所有的消息广播
                for (int i = 0; i < maxCount; i++)
                {
                    if (conns[i] == null || !conns[i].isUse)
                        continue;
                    conns[i].socket.Send(sendMsg);

                }

                //继续接收
                conn.socket.BeginReceive(conn.readBuff, conn.buffCount, conn.BuffRemain(), SocketFlags.None, ReceiveCb, conn);

            }
            catch (Exception ex)
            {
                Console.WriteLine("[" + conn.GetAddress() + "] 断开连接");
                conn.Close();
            }
        }
    }

    public class Conn
    {
        //缓存区大小
        public const int BUFFER_SIZE = 1024;
        //socket
        public Socket socket;
        //是否被使用
        public bool isUse = false;
        //Buff
        public byte[] readBuff;
        public int buffCount = 0;

        //构造函数
        public Conn()
        {
            readBuff = new byte[BUFFER_SIZE];
        }

        //初始化
        public void Init(Socket socket)
        {
            this.socket = socket;
            isUse = true;
            buffCount = 0;
        }

        //缓存区剩余的字节
        public int BuffRemain()
        {
            return BUFFER_SIZE - buffCount;
        }

        //获取客户端的地址
        public string GetAddress()
        {
            if (!isUse)
                return "获取地址失败";

            return socket.RemoteEndPoint.ToString();
        }

        //关闭
        public void Close()
        {
            if (isUse)
                return;

            Console.WriteLine(GetAddress() + " 断开连接");
            socket.Close();
            isUse = false;
        }
    }
}
