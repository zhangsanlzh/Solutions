2019/3/25 �ֽ�����ת16�����ַ���

   public static string ByteToString(byte[] b)
   {
      string str = "";
      for (int i = 0; i < b.Length; i++)
      {
        str += Convert.ToString(b[i], 16).PadLeft(2, '0').ToUpper() + " ";
      }
      return str;
   }