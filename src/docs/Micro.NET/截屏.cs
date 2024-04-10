2018/12/18 ����

	//����
	private void buttonCutScreen_Click(object sender, EventArgs e)
	{
		//�ж�savepath�е�λ���Ƿ���ڣ������������ļ�
		if (Directory.Exists(Setting.savePath) == false)
			Directory.CreateDirectory(Setting.savePath);
		//�����봰���С��ͬ��ͼ���ļ�
		Bitmap image = new Bitmap(this.Size.Width, this.Size.Height);
		//��������
		Graphics imgGraphics = Graphics.FromImage(image);

		//��ȡʱ��
		DateTime dateTime = DateTime.Now;
		//��ʱ����Ϊ�ļ���
		string newFileName = dateTime.ToString("��ͼ(yyyyMMdd-HHʱmm��ss��)");
		//����Ļ����ͼƬ
		imgGraphics.CopyFromScreen(new Point(this.Location.X, this.Location.Y), new Point(0, 0), new Size(this.Size.Width, this.Size.Height));


		if (Setting.savePath != null)
		{
			//����ͼƬ
			image.Save(Setting.savePath + newFileName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

			//��ϵͳ�汾��win7�������ڽ���ʱ����˸���ڣ�������˸
			if (System.Environment.OSVersion.Version.Major >= 6)
			{
				startFlash = true;
				TimerLighting.Enabled = true;
			}
			else
				myLabel1.SendText("��������ʾ�� ��ͼ�ѱ���");
		}

	}

