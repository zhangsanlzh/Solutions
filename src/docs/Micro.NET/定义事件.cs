2019/2/20 �����¼�

����ʱ
        public new event EventHandler Click
        {
            add
            {
                lblText.Click += value;
                lblBottom.Click += value;
                lblTop.Click += value;
            }
            remove
            {
                lblText.Click -= value;
                lblBottom.Click -= value;
                lblTop.Click -= value;
            }
        }

����ʱ
	usrCtrlRdoBtn1.Click += UsrCtrlRdoBtn1_Click;
	usrCtrlRdoBtn2.Click += UsrCtrlRdoBtn2_Click;

        private void UsrCtrlRdoBtn1_Click(object sender, EventArgs e)
        {
            usrCtrlRdoBtn2.Checked = false;
        }

        private void UsrCtrlRdoBtn2_Click(object sender, EventArgs e)
        {
            usrCtrlRdoBtn1.Checked = false;
        }
