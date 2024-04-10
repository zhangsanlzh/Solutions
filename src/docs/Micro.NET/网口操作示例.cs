2018/12/20 ���ڲ���ʾ��

using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirMonitorFHY.AirMonitorStation
{
    public class TcpClient
    {
        public delegate void dataReceive(string RecevieData, int ReceiveLength);   //��Ϣ����ί��     ���յ����ݡ�����õ����ݳ���
        public event dataReceive DataReceived;

        string ReceiveData = "";
        int ReceiveLength = 0;
        string IpServer = "";
        string PortServer = "";
        public bool IsConnect = false;
        public TcpClient(string ip, string port)
        {
            IpServer = ip;
            PortServer = port;
        }
        Thread threadClient = null; // �������ڽ��շ������Ϣ�� �̣߳�  
        Socket sockClient = null;
        public Boolean ConnectServer()
        {
            try
            {
                string str = IpServer;
                IPAddress ip = IPAddress.Parse(str);
                IPEndPoint endPoint = new IPEndPoint(ip, int.Parse(PortServer));
                sockClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sockClient.Connect(endPoint);

            }
            catch (SocketException se)
            {
                IsConnect = false;
                //MessageBox.Show(se.Message);
                return IsConnect; 
            }
            IsConnect = true;
            threadClient = new Thread(RecMsg);
            threadClient.IsBackground = true;
            threadClient.Start();
            return true;
        }
        void RecMsg()
        {

            while (true)
            {
                // ����һ��2M�Ļ�������  
                byte[] arrMsgRec = new byte[1024];
                // �����ܵ������ݴ��뵽����  arrMsgRec�У�  
                int length = -1;
                try
                {
                    length = sockClient.Receive(arrMsgRec); // �������ݣ����������ݵĳ��ȣ�  
                }
                catch (SocketException se)
                {
                    IsConnect = false;
                    //MessageBox.Show("�쳣:" + se.Message);
                    return;
                }
                catch (Exception e)
                {
                    MessageBox.Show("�쳣��" + e.Message);
                    return;
                }
                if (arrMsgRec[0] == 0) // ��ʾ���յ�������Ϣ���ݣ�
                {
                    try
                    {
                        ReceiveData = System.Text.Encoding.UTF8.GetString(arrMsgRec, 1, length - 1);// �����յ����ֽ�����ת�����ַ�����
                        ReceiveLength = ReceiveData.Length;
                        Thread th = null;
                        th = new Thread(new ThreadStart(ProcessData));
                        th.Start();
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                        return;
                    }
                }
            }
        }

        // ������Ϣ��  
        public void SendMsg(string strMsg)
        {
            //strMsg = "PH";
            byte[] arrMsg = System.Text.Encoding.UTF8.GetBytes(strMsg);
            byte[] arrSendMsg = new byte[arrMsg.Length + 1];
            arrSendMsg[0] = 0; // ������ʾ���͵�����Ϣ����  
            Buffer.BlockCopy(arrMsg, 0, arrSendMsg, 1, arrMsg.Length);
            try
            {
                sockClient.Send(arrSendMsg); // ������Ϣ��  
            }
            catch (SocketException se)
            {
                Program.logfile.writeValue("�ͻ��˷����쳣��" + se.Message);
            }
            //MessageBox.Show(strMsg);
        }
        void ProcessData()
        {
            if (DataReceived != null)
                DataReceived(ReceiveData, ReceiveLength);
        }
        public void StopConnect(object sender, EventArgs e)
        {
            if (IsConnect)
            {
                try
                {
                    sockClient.Close();
                    threadClient.Abort();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
