2018/12/20 ���ڲ����ο�

using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace AirMonitorFHY.AirMonitorStation
{
    public class SerialPortOperator
    {
        ///��װ���ڲ�����
        SerialPort sp = new SerialPort();
        public delegate void dataReceive(string receivedata, ProtocolReturnLength ReceivedataLength);   //��Ϣ����ί��     ���յ����ݡ�����õ����ݳ���
        public event dataReceive DataReceived;
        /// <summary>
        /// ���յ�������
        /// </summary>
        string m_ReceiveData;
        /// <summary>
        /// ���ݳ���
        /// </summary>
        public ProtocolReturnLength m_ReceivedataLength;
        /// <summary>
        /// ���ں� ������ʵ����
        /// </summary>
        /// <param name="comPort">�˿�</param>
        /// <param name="comRate">������</param>
        public SerialPortOperator(string comPort, string comRate)
        {
            sp.PortName = comPort;
            sp.BaudRate = int.Parse(comRate);
            sp.Parity = Parity.None;
            sp.StopBits = StopBits.One;
            sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
        }
        bool _isopen = false;
        /// <summary>
        /// ���ڿ���״̬
        /// </summary>
        public bool Isopen
        {
            get { return _isopen; }
            set { _isopen = value; }
        }
        /// <summary>
        /// ���ڿ���
        /// </summary>
        public void open()
        {
            if (!sp.IsOpen)
            {
                try
                {
                    sp.Open();
                    Isopen = true;
                }
                catch(Exception ex)
                {
                   
                    Isopen = false;
                    //throw;
                }
            }
        }
        /// <summary>
        /// ���ڹر�
        /// </summary>
        public void close()
        {
            Isopen = false;
            if (sp.IsOpen)
            {
                sp.DiscardInBuffer();
                sp.DiscardOutBuffer();
                sp.Close();

            }
        }
        /// <summary>
        /// �������� ������������
        /// </summary>
        /// <param name="str"></param>
        public void sendHexOnly(string str)
        {
            if (!sp.IsOpen) return;
            sp.DiscardInBuffer();//�ն��������.��֮ǰ���,(���������,�Ͳ�Ҫ��).
            
            try
            {
                byte[] ByteFoo = HexStringToByteArray(str);
                sp.Write(ByteFoo, 0, ByteFoo.Length);
            }
            catch
            {
                //����ʧ��
            }
        }
        /// <summary>
        /// �������� ����������
        /// </summary>
        /// <param name="str"></param>
        public void sendHex(string str, ProtocolReturnLength length)
        {
            if (!sp.IsOpen) return;
            sp.DiscardInBuffer();//�ն��������.��֮ǰ���,(���������,�Ͳ�Ҫ��).
            m_ReceivedataLength = length;
            try
            {
                byte[] ByteFoo = HexStringToByteArray(str);
                sp.Write(ByteFoo, 0, ByteFoo.Length);
            }
            catch
            {
                //����ʧ��
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int bytes = 0;
            Thread.CurrentThread.Join(10);
            bytes = (int)m_ReceivedataLength;
            if (sp.BytesToRead < bytes) return;
            

            byte[] buffer = new byte[bytes];
            try
            {
                if (sp.IsOpen && Isopen)
                {

                    sp.Read(buffer, 0, bytes);

                    if (sp.IsOpen)
                        sp.DiscardInBuffer();
                    m_ReceiveData = ByteArrayToHexString(buffer);
                    m_ReceiveData = m_ReceiveData.Replace(" ", ""); //ת��Ϊ�ַ�������Ϊm_ReceivedataLength*2
                                                                    //m_ReceivedataLength = m_ReceiveData.Length;
                    Thread th = null;
                    th = new Thread(new ThreadStart(ProcessData));
                    th.Start();

                }
            }
            catch (Exception ex)
            { }
        }

        void ProcessData()
        {
            if (DataReceived != null)
            {
                try
                {
                    DataReceived(m_ReceiveData, m_ReceivedataLength);
                }
                catch (Exception  ex)
                { }
             }
        }

        //HEX stringת byte[]
        public byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }
        //byte[]ת HEXstring
        public string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            return sb.ToString().ToUpper();
        }
    }
}

