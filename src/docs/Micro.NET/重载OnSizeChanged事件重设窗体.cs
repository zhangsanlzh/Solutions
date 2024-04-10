2018/12/18 ����OnSizeChanged�¼����贰��

        /// <summary>
        /// �����С�ı��¼�
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            //����������δ��ʾ���ǰ��ִ�е��÷������ִ���
            if (!shown) return;
            //����Ŵ����С�ı���
            float newX = (float)this.Width / iniFormSize.Width;
            float newY = (float)this.Height / iniFormSize.Height;
            //���ÿؼ�����
            setControlAndChild(this, newX, newY);
            //���û�����
            base.OnSizeChanged(e);
        }

        /// <summary>
        /// ���ÿؼ���С
        /// </summary>
        /// <param name="ctr">�ؼ�����</param>
        /// <param name="newX">x�Ŵ���С����</param>
        /// <param name="newY">y�Ŵ���С����</param>
        private void setControlAndChild(Control ctr, float newX, float newY)
        {
            //�ݹ�������ÿؼ���С�ĺ��������пؼ����ӿؼ��Ĵ�С����һ��
            foreach (Control childCtr in ctr.Controls)
            {
                setControl(childCtr, newX, newY);
                if (childCtr.Controls.Count > 0)
                    setControlAndChild(childCtr, newX, newY);
            }
        }

        /// <summary>
        /// ���ÿؼ���С
        /// </summary>
        /// <param name="ctr">�ؼ�����</param>
        /// <param name="newX">x�Ŵ���С����</param>
        /// <param name="newY">y�Ŵ���С����</param>
        private void setControl(Control ctr, float newX, float newY)
        {
            //��tag�е�����ͨ�����������ŷֿ�
            string[] para = ctr.Tag.ToString().Split(':');
            //����С��λ�ú��ֺŶ��������Ŵ���С
            int x = (int)(Convert.ToSingle(para[0]) * newX);
            int y = (int)(Convert.ToSingle(para[1]) * newY);
            int width = (int)(Convert.ToSingle(para[2]) * newX);
            int height = (int)(Convert.ToSingle(para[3]) * newY);
            Single fontSize = Convert.ToSingle(para[4]) * newY;
            //���ÿؼ��Ĵ�С��λ�ú��ֺ�
            ctr.Location = new Point(x, y);
            ctr.Size = new Size(width, height);
            ctr.Font = new Font(ctr.Font.Name, fontSize, ctr.Font.Style, ctr.Font.Unit);
        }
