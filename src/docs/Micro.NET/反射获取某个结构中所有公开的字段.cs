2018/12/20 �����ȡĳ���ṹ�����й������ֶ�

foreach(FieldInfo fi in t.GetFields())
{
    if (fi.Name == keyName)
	if (fi.FieldType == typeof(bool))
	    fi.SetValue(null, bool.Parse(key.GetValue(keyName).ToString()));
	else if (fi.FieldType == typeof(int))
	    fi.SetValue(null, int.Parse(key.GetValue(keyName).ToString()));//��ȡ���ͱ�����ֵ
	else if (fi.FieldType == typeof(double))
	    fi.SetValue(null, double.Parse(key.GetValue(keyName).ToString()));
	else
	    fi.SetValue(null, key.GetValue(keyName).ToString());//��ȡ�ַ���������ֵ
}

public struct Setting
{
	public static int PMLimit;
	public static int PMLimit1;
	public static int MMLimit;
	public static string savePath;
	public static bool ShowCode;
	public static string Password;

	public static void RecoverDefault()
	{
		savePath = Application.StartupPath + "\\����ս����������������\\";
		PMLimit = 5000;
		PMLimit1 = 5000;
		MMLimit = 5000;
		ShowCode = false;
		Password = "123456";
	}
}
