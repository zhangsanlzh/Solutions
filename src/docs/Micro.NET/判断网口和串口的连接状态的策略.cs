2019/3/1 

	// ÿ��5s����ͨ�����ڻ����ڷ���һ�����ݣ�����ʧ�����Խ������ӣ�
	// ������ӽ����ɹ�����ô�¸�5sʱ���ݽ��ᷢ�ͳɹ�����ô���豸�������������������쳣��
	private void timer5s_Tick(object sender, EventArgs e)
	{
		bool connSuccess = false;

		if (rdoBtnSocket.Checked)
		{
			if (socket.SendMsg(Protocols.PortCommTest))
			{
				picBoxCommunication.BackgroundImage = Properties.Resources.comm0;
				connSuccess = true;
			}
			else
			{
				socket.ConnectServer();
				picBoxCommunication.BackgroundImage = Properties.Resources.comm1;
			}
		}
		else
		{
			if (serial.Send(Protocols.PortCommTest))
			{
				picBoxCommunication.BackgroundImage = Properties.Resources.comm0;
				connSuccess = true;
			}
			else
			{
				serial.Open();
				picBoxCommunication.BackgroundImage = Properties.Resources.comm1;
			}
		}

		if (connSuccess)
			Data_UpdateEvent();
	}
